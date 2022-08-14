using Canoe;
using Solana.Unity.Rpc.Core.Http;
using Solana.Unity.Rpc.Models;
using Solana.Unity.Wallet;
using Solana.Unity.Wallet.Bip39;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(CanoeDeFi))]
public class WalletController : MonoBehaviour
{
    public static WalletController Instance;
    //Notice: this is a demo for show, you shuld use pwd form user's input
    public readonly string PASSWORD = "demopassword";
    public readonly string AARTCANOEADDRESS = "Canoe7wkcZcdF6qKqWghq8fD2UkD4rhg1QxkS3H1g6NZ";
    public readonly string AARTMINT = "F3nefJBcejYbtdREjui1T9DPh5dBgpkKq7u2GAAMXs5B";

    #region Public GameObject Members

    public GameObject Panel_NewUser;
    public GameObject Panel_ImportWallet;
    public GameObject Panel_CreateWallet;
    public GameObject Panel_CreateWalletConfirm;
    public GameObject Panel_Homepage;
    //public GameObject Panel_Recieve;
    //public GameObject Panel_Transfer;
    //public GameObject Panel_Swap;
    //public GameObject Panel_TransferConfirm;
    public GameObject Panel_Notice;

    #endregion
    #region Public Component Members

    public Wallet_Homepage HomePage;

    #endregion
    #region Public Data Members

    public Mnemonic Mnemonic;
    public Wallet CurrentWallet;
    public TokenAccount CurrentAARTTokenAccount;
    [HideInInspector]
    public double SOLBalance;
    [HideInInspector]
    public double AARTBalance;

    #endregion

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
        HomePage = Panel_Homepage.GetComponent<Wallet_Homepage>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //UniClipboard.SetText("text you want to clip");
        WalletStart();
    }

    // when wallet need to be shown
    void WalletStart()
    {
        if (!CanoeDeFi.Instance.HasWallet())
        {
            Panel_NewUser.SetActive(true);
        }
        else
        {
            //login with pwd
            bool loginResult = CanoeDeFi.Instance.LoginWithPwd(PASSWORD);
            if (loginResult)
            {
                Panel_Homepage.SetActive(true);
            }
            else
            {
                ShowNotice("Network error");
            }
        }
    }
    public void ShowWalletHomePage()
    {
        Panel_Homepage.SetActive(true);
    }
    public void RefreshBanlace()
    {
        if (Panel_Homepage.activeInHierarchy)
        {
            HomePage.RefreshBalance();
        }
        
    }
    public void GenerateNewWallet(Mnemonic mnemonic)
    {
        CanoeDeFi.Instance.GenerateNewWallet();
    }

    public async Task<RequestResult<string>> Reborn()
    {
        if (CurrentAARTTokenAccount == null)
        {
            var SPLResult = await CanoeDeFi.Instance.GetOwnedTokenAccounts();
            foreach (var item in SPLResult)
            {

                if (item.Account.Data.Parsed.Info.Mint == WalletController.Instance.AARTMINT)
                {
                    CurrentAARTTokenAccount = item;
                }
            }
        }
        RequestResult<string> result = await CanoeDeFi.Instance.TransferToken(CurrentAARTTokenAccount.PublicKey, AARTCANOEADDRESS, CurrentWallet.GetAccount(0), AARTMINT, 6, 29);
        return result;
        // return null;
    }
    public void ShowNotice(string msg)
    {
        Panel_Notice.SetActive(true);
        Panel_Notice.GetComponent<Wallet_Notice>().ShowNotice(msg);
    }

    public void OnDead()
    {
        
    }
}
