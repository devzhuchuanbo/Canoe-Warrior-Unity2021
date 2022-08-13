using Canoe;
using Solana.Unity.Wallet;
using Solana.Unity.Wallet.Bip39;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanoeDeFi))]
public class WalletController : MonoBehaviour
{
    public static WalletController Instance;
    //Notice: this is a demo for show, you shuld use pwd form user's input
    public readonly string PASSWORD = "demopassword";

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

    #region Public Data Members

    public Mnemonic Mnemonic;
    public Wallet CurrentWallet;
    [HideInInspector]
    public double SOLBalance;
    [HideInInspector]
    public double AARTBalance;

    #endregion

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        //UniClipboard.SetText("text you want to clip");
       CanoeDeFi.Instance.HasWallet();   
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
            Panel_Homepage.SetActive(true);
        }
    }
    public void ShowWalletHomePage()
    {
        Panel_Homepage.SetActive(true);
    }
    public void GenerateNewWallet(Mnemonic mnemonic)
    {
        CanoeDeFi.Instance.GenerateNewWallet();
    }
    public void ShowNotice(string msg)
    {
        Panel_Notice.SetActive(true);
        Panel_Notice.GetComponent<Wallet_Notice>().ShowNotice(msg);
    }
}
