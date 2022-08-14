using Canoe;
using Cysharp.Threading.Tasks;
using Solana.Unity.Rpc.Core.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class WalletSub_Transfer : MonoBehaviour
{
    public InputField TargetAddress;
    public InputField Amount;
    public Image TokenImage;
    public Sprite SOLSprite, AARTSprite;
    public Text Balance;
    public bool isTransferSOL = true;

    private double transferAmount = 0;

   private Wallet_Homepage wallet_Homepage;
    private void OnEnable()
    {
        TargetAddress.text = "";
        TargetAddress.text = "";

        wallet_Homepage = GetComponentInParent<Wallet_Homepage>();
    }
    public void SelectSOL()
    {
        isTransferSOL = true;
        TokenImage.sprite = SOLSprite;
        Balance.text = wallet_Homepage.SOLValue.text;
    }
    public void SelectToken()
    {
        isTransferSOL = false;
        TokenImage.sprite = AARTSprite;
        Balance.text = wallet_Homepage.AARTValue.text;
    }
    public void SetAllBtn()
    {
        if (isTransferSOL)
        {
            Amount.text=(Convert.ToDouble( wallet_Homepage.SOLValue.text)-0.001).ToString();
        }
        else
        {
            Amount.text = wallet_Homepage.AARTValue.text;
        }
    }

    public void TransferBtn()
    {
        if (string.IsNullOrEmpty(TargetAddress.text))
        {
            WalletController.Instance.ShowNotice("address can't be empty");
            return;
        }

        try
        {
            transferAmount = Convert.ToDouble(Amount.text);
            if (transferAmount == 0)
            {
                WalletController.Instance.ShowNotice("amount can't be zero");
                return;
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex);
            throw;
        }

        if (isTransferSOL)
        {
            //SOL
            transferAmount *= 1000000000;
        }
        else
        {
            //AART
            transferAmount *= 1000000;
        }
        if (isTransferSOL)
        {
          

            Func<Task> SOLTransferTask = async () =>
            {
                    RequestResult<string> transferResult = await CanoeDeFi.Instance.TransferSol(TargetAddress.text, (ulong)transferAmount);
                    if (transferResult.Reason == "OK" || transferResult.Reason == "ok")
                    {
                        WalletController.Instance.ShowNotice("The request is successful!");
                    }
                    else
                    {
                        WalletController.Instance.ShowNotice("The request is failed!");
                    }

            };
            SOLTransferTask();
        }
        //transfer aart
        else
        {
            Func<Task> AARTTransferTask = async () =>
            {
                if (WalletController.Instance.CurrentAARTTokenAccount == null)
                {
                    var SPLResult = await CanoeDeFi.Instance.GetOwnedTokenAccounts();
                    foreach (var item in SPLResult)
                    {

                        if (item.Account.Data.Parsed.Info.Mint == WalletController.Instance.AARTMINT)
                        {
                            WalletController.Instance.CurrentAARTTokenAccount = item;
                        }
                    }
                }
                Debug.Log("Prepare to transfer");
                RequestResult<string> transferResult = await CanoeDeFi.Instance.TransferToken(WalletController.Instance.CurrentAARTTokenAccount.PublicKey, TargetAddress.text, WalletController.Instance.CurrentWallet.GetAccount(0), WalletController.Instance.AARTMINT, 6, (ulong)transferAmount);

                Debug.Log("transfer done :" +transferResult.Reason);
                if (transferResult.Reason == "OK" || transferResult.Reason == "ok")
                {
                    WalletController.Instance.ShowNotice("The request is successful!");
                    WalletController.Instance.RefreshBanlace();
                }
                else
                {
                    WalletController.Instance.ShowNotice("The request is failed!");
                }

            };
            AARTTransferTask();
            
        }
    }

    // IEnumerator IE_UniTransfer()
    // {
    //     bool await_finished = false;
    //     Func<UniTaskVoid> UniTransfer = async () =>
    //     {
    //         RequestResult<string> transferResult = await CanoeDeFi.Instance.TransferSol(TargetAddress.text, (ulong)transferAmount);
    //         if (transferResult.Reason == "OK" || transferResult.Reason == "ok")
    //         {
    //             WalletController.Instance.ShowNotice("The request is successful!");
    //         }
    //         else
    //         {
    //             WalletController.Instance.ShowNotice("The request is failed!");
    //         }
    //         await_finished = true;
    //
    //     };
    //     UniTransfer();
    //     yield return new WaitUntil(() => await_finished); //异步行为完成
    //     
    //     //todo...
    //     
    // }
}
