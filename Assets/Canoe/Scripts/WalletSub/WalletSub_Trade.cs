using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Canoe;
using LitJson;
using UnityEngine.Networking;
using System;
using Solana.Unity.Rpc.Core.Http;
using System.Threading.Tasks;
using System.Transactions;
using Solana.Unity.Rpc.Messages;
using Solana.Unity.Rpc;
using Solana.Unity.Rpc.Models;
using Solana.Unity.Rpc.Builders;
using Solana.Unity.Wallet;

public class WalletSub_Trade : MonoBehaviour
{
    public Image FromImg, ToImg;
    public Sprite SOLSprite, AARTSprite;

    public Text FromName, ToName;
    public Text FromBalance, ToBalance;

    public InputField InputValue;
    public InputField OutputValue;


    private JsonData jupyteRoute = new JsonData();
    private string routeUrl = $"https://quote-api.jup.ag/v1/quote?inputMint={0}&outputMint={1}&amount={2}&slippage={3}&feeBps={4}";
    private string jupiterPostUrl = "https://quote-api.jup.ag/v1/swap";
    private string jupiterMsgBase64;

    string feeAccount = "Canoe7wkcZcdF6qKqWghq8fD2UkD4rhg1QxkS3H1g6NZ";

    private RequestResult<string> jupiterSwapResult;
    private Action<RequestResult<string>> jupiterSwapCallback;

    private float slippage = 0.5f;
    public Text Slippage;
    public void OnEnable()
    {
        InputValue.onEndEdit.RemoveAllListeners();
        InputValue.onEndEdit.AddListener(OnEndInput);

        FromImg.sprite = SOLSprite;
        ToImg.sprite = AARTSprite;
        FromName.text = "SOL";
        ToName.text = "AART";
        FromBalance.text = WalletController.Instance.SOLBalance.ToString();
        ToBalance.text = WalletController.Instance.AARTBalance.ToString();
        slippage = 0.5f;
        Slippage.text = slippage.ToString() + "%";
    }
    public void OnEndInput(string str)
    {
        Debug.Log("OnEndInput:" + str);
        if (string.IsNullOrEmpty(str) || str == "0")
        {
            return;
        }
        double inputDouble = Convert.ToDouble(str);

        bool isFromSOL = FromName.text == "SOL" ? true : false;
        var inMint = isFromSOL ? WalletController.Instance.SOLMINT : WalletController.Instance.AARTMINT;
        var outMint = isFromSOL ? WalletController.Instance.AARTMINT : WalletController.Instance.SOLMINT;
        ulong amount = isFromSOL ? (ulong)(inputDouble * 1000000000) : (ulong)(inputDouble * 1000000);

        StartCoroutine(
      CanoeDeFi.Instance.RequestJupiterOutputAmount(inMint, outMint, amount, slippage, 4, WalletController.Instance.AARTCANOEADDRESS, (s) =>
      {
          Debug.Log("out:" + s);
          bool isFromSOL = FromName.text == "SOL" ? true : false;
          double outvalue = Convert.ToDouble(s);
          OutputValue.text = isFromSOL ? (outvalue / 1000000).ToString() : (outvalue / 1000000000).ToString();
      }));
    }

    public void ExchanceBtn()
    {
        var tmp = FromImg.sprite;
        FromImg.sprite = ToImg.sprite;
        ToImg.sprite = tmp;

        var name = FromName.text;
        FromName.text = ToName.text;
        ToName.text = name;

        var balance = FromBalance.text;
        FromBalance.text = ToBalance.text;
        ToBalance.text = balance;

        InputValue.text = "0";
        OutputValue.text = "0";
    }
    public void AllBtn()
    {
        InputValue.text = FromBalance.text;
        InputValue.onEndEdit.Invoke(InputValue.text);
    }
    public void TradeBtn()
    {
        //WalletController.Instance.ShowNotice("Comming Soon ...");
        double inputDouble = Convert.ToDouble(InputValue.text);
        bool isFromSOL = FromName.text == "SOL" ? true : false;
        var inMint = isFromSOL ? WalletController.Instance.SOLMINT : WalletController.Instance.AARTMINT;
        var outMint = isFromSOL ? WalletController.Instance.AARTMINT : WalletController.Instance.SOLMINT;
        ulong amount = isFromSOL ? (ulong)(inputDouble * 1000000000) : (ulong)(inputDouble * 1000000);

        CanoeDeFi.Instance.JupiterSwapRequest(inMint, outMint, amount, slippage, 4, WalletController.Instance.AARTCANOEADDRESS, (tf) =>
        //CanoeDeFi.Instance.JupiterSwapRequest(inMint, outMint, amount, slippage, 4, "", (s) =>
        {
            WalletController.Instance.CloseNotice();
            if (tf)
            {
             WalletController.Instance.ShowNotice("The request is successful!");
            }
            else
            {
                WalletController.Instance.ShowNotice("The request is failed!");
            }
        });
        WalletController.Instance.ShowNotice("Processing, please wait...");
    }
    public void ChangeSlippage(float _slippage)
    {
        slippage = _slippage;
        Slippage.text = slippage.ToString() + "%";
    }


    private IEnumerator GetJupiterTx(string routeUrlWithPams)
    {
        //get jupiter route
        UnityWebRequest getRequest = UnityWebRequest.Get(routeUrlWithPams);
        yield return getRequest.SendWebRequest();
        if (getRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(" Failed to communicate with the server");
            yield return null;
        }
        string data = getRequest.downloadHandler.text;
        Debug.Log(data);
        JsonData jData = JsonMapper.ToObject(data);
        //choose the first
        jupyteRoute["route"] = jData["data"][0];

        //get jupiter transaction

        jupyteRoute["userPublicKey"] = CanoeDeFi.Instance.CurrentWallet.Account.PublicKey.ToString();
        jupyteRoute["fee"] = feeAccount;
        Debug.Log($"data:{(string)jupyteRoute.ToJson()}");
        byte[] postBytes = System.Text.Encoding.Default.GetBytes((string)jupyteRoute.ToJson());

        Debug.Log($"route: {(string)jupyteRoute.ToJson()}");
        Debug.Log($"userPublicKey:{CanoeDeFi.Instance.CurrentWallet.Account.PublicKey}");
        UnityWebRequest postRequest = new UnityWebRequest(jupiterPostUrl, "POST");
        postRequest.SetRequestHeader("Content-Type", "application/json");
        postRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(postBytes);
        postRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

        yield return postRequest.SendWebRequest();

        if (postRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log($"UnityWebRequest.Result.ProtocolError:{postRequest.result}");
        }
        else if (postRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log($"UnityWebRequest.Result.ConnectionError:{postRequest.result}");
        }
        else
        {
            string receiveContent = postRequest.downloadHandler.text;
            Debug.Log(receiveContent);
        }

        Debug.Log($"Status Code: {postRequest.responseCode}");
        if (postRequest.responseCode == 200)
        {
            string text = postRequest.downloadHandler.text;
        }
        JsonData resJdata = JsonMapper.ToObject(postRequest.downloadHandler.text);
        Debug.Log($"base64Data:{resJdata["swapTransaction"]}");

        jupiterMsgBase64 = resJdata["swapTransaction"].ToJson().Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "").Replace("\"", "");

        var task = Task.Run(SendJupiterTransaction);
        yield return new WaitUntil(() => task.IsCompleted);

    }

    private async Task SendJupiterTransaction()
    {
        Solana.Unity.Rpc.Models.Transaction decodedInstructions = Solana.Unity.Rpc.Models.Transaction.Deserialize(jupiterMsgBase64);

        RequestResult<ResponseValue<BlockHash>> blockHash = await ClientFactory.GetClient(Cluster.MainNet).GetRecentBlockHashAsync();

        var tb = new TransactionBuilder().SetRecentBlockHash(blockHash.Result.Value.Blockhash).
       SetFeePayer(CanoeDeFi.Instance.CurrentWallet.Account);
        for (int i = 0; i < decodedInstructions.Instructions.Count; i++)
        {
            tb.AddInstruction(decodedInstructions.Instructions[i]);
        }
        byte[] txBytes = tb.
       Build(new List<Account> { CanoeDeFi.Instance.CurrentWallet.Account });

        var result = await ClientFactory.GetClient(Cluster.MainNet).SendTransactionAsync(txBytes);
        jupiterSwapResult = result;
        jupiterSwapCallback?.Invoke(result);
        Debug.Log($"done: {result.Reason}");
    }
}
