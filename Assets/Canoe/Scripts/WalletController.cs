using Canoe;
using Solana.Unity.Programs;
using Solana.Unity.Rpc;
using Solana.Unity.Rpc.Builders;
using Solana.Unity.Rpc.Core.Http;
using Solana.Unity.Rpc.Messages;
using Solana.Unity.Rpc.Models;
using Solana.Unity.Wallet;
using Solana.Unity.Wallet.Bip39;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanoeDeFi))]
public class WalletController : MonoBehaviour
{
    public static WalletController Instance;
    //Notice: this is a demo for show, you shuld use pwd form user's input
    public readonly string PASSWORD = "demopassword";
    public readonly string AARTCANOEADDRESS = "Canoe7wkcZcdF6qKqWghq8fD2UkD4rhg1QxkS3H1g6NZ";

    public readonly string SOLMINT = "So11111111111111111111111111111111111111112";
    //public readonly string AARTMINT = "F3nefJBcejYbtdREjui1T9DPh5dBgpkKq7u2GAAMXs5B";
    public readonly string AARTMINT = "EPjFWdd5AufqSSqeM2qN1xzybapC8G4wEGGkZwyTDt1v";

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
    public GameObject Panel_OnDead;
    public GameObject Panel_NoticeDig;
    public GameObject Panel_Notice;

    #endregion
    #region Public Component Members

    public Wallet_Homepage HomePage;

    #endregion
    #region Public Data Members

    public Mnemonic Mnemonic;
    private Wallet currentWallet;
    public Wallet CurrentWallet
    {
        get
        {
            if (currentWallet == null)
            {
                currentWallet = CanoeDeFi.Instance.CurrentWallet;
            }
            return currentWallet;
        }
        set => currentWallet = value;
    }
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
        bool loginResult = CanoeDeFi.Instance.LoginWithPwd(PASSWORD);
        if (loginResult)
        {
            RefreshBanlace();
        }
    }

    // when wallet need to be shown
    public void WalletStart()
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

        if (CurrentAARTTokenAccount == null)
        {
            Func<Task> getTokenAccount = async () =>
            {
                await GetTokenAccount();
            };
            getTokenAccount();
        }


    }
    public void GenerateNewWallet(Mnemonic mnemonic)
    {
        CanoeDeFi.Instance.GenerateNewWallet();
    }
    public bool IsHasEnoughAART()
    {
        if (AARTBalance < 29)
        {

        }
        return AARTBalance >= 29;
    }
    public async Task GetTokenAccount()
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
    }
    //public void 
    public async Task<RequestResult<string>> Reborn()
    {
        if (CurrentAARTTokenAccount == null)
        {
            await GetTokenAccount();
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

    private Action onRebornCallback = null;



    public void OnDead(Action onRebornCallback)
    {
        this.onRebornCallback = onRebornCallback;
        // todo ...
        Panel_OnDead.SetActive(true);
    }

    public void OnReborn()
    {
        this.onRebornCallback?.Invoke();
        this.onRebornCallback = null;
    }



    public async Task<RequestResult<string>> TransferToken(string sourceTokenAccount, string toWalletAccount, Account sourceAccountOwner, string tokenMint, ulong amount = 1)
    {
        PublicKey associatedTokenAccountOwner = new PublicKey(toWalletAccount);
        Debug.Log("associatedTokenAccountOwner: " + associatedTokenAccountOwner);
        PublicKey mint = new PublicKey(tokenMint);
        Debug.Log("mint: " + tokenMint);
        Account ownerAccount = CanoeDeFi.Instance.CurrentWallet.GetAccount(0);
        Debug.Log("ownerAccount: " + ownerAccount);
        PublicKey associatedTokenAccount = AssociatedTokenAccountProgram.DeriveAssociatedTokenAccount(associatedTokenAccountOwner, new PublicKey(tokenMint));
        Debug.Log("associatedTokenAccount: " + associatedTokenAccount);

        RequestResult<ResponseValue<BlockHash>> blockHash = await ClientFactory.GetClient(CanoeDeFi.Instance.Env).GetRecentBlockHashAsync();

        RequestResult<ulong> rentExemptionAmmount = await ClientFactory.GetClient(CanoeDeFi.Instance.Env).GetMinimumBalanceForRentExemptionAsync(TokenProgram.TokenAccountDataSize);
        //TokenAccount[] lortAccounts = await GetOwnedTokenAccounts(toWalletAccount, tokenMint, "");
        Debug.Log("rentExemptionAmmount: " + rentExemptionAmmount);
        TokenAccount[] lortAccounts = await GetOwnedTokenAccounts(toWalletAccount, tokenMint, TokenProgram.ProgramIdKey);
        Debug.Log("lortAccounts: " + lortAccounts);
        byte[] transaction;
        //try to make sure is the account already have a token account
        var info = await GetAccountData(associatedTokenAccount);

        //already have a token account
        if (info != null)
        {
            Debug.Log("info!=null ");
            PublicKey initialAccount =
    AssociatedTokenAccountProgram.DeriveAssociatedTokenAccount(ownerAccount, mint);

            Debug.Log($"initialAccount: {initialAccount}");
            transaction = new TransactionBuilder().
                SetRecentBlockHash(blockHash.Result.Value.Blockhash).
                SetFeePayer(ownerAccount).
                AddInstruction(TokenProgram.TransferChecked(
                    initialAccount,
                    associatedTokenAccount,
                    amount,
                    6,
                   ownerAccount, mint
                    )).
                Build(new List<Account> { ownerAccount });
        }
        else
        {
            Debug.Log($"AssociatedTokenAccountOwner: {associatedTokenAccountOwner}");
            Debug.Log($"AssociatedTokenAccount: {associatedTokenAccount}");

            PublicKey initialAccount =
    AssociatedTokenAccountProgram.DeriveAssociatedTokenAccount(ownerAccount, mint);

            Debug.Log($"initialAccount: {initialAccount}");
            transaction = new TransactionBuilder().
                SetRecentBlockHash(blockHash.Result.Value.Blockhash).
                SetFeePayer(ownerAccount).
                AddInstruction(AssociatedTokenAccountProgram.CreateAssociatedTokenAccount(
                    ownerAccount,
                    associatedTokenAccountOwner,
                    mint)).
                AddInstruction(TokenProgram.TransferChecked(
                    initialAccount,
                    associatedTokenAccount,
                    amount,
                    6,
                   ownerAccount, mint
                    )).// the ownerAccount was set as the mint authority
                Build(new List<Account> { ownerAccount });
        }

        return await ClientFactory.GetClient(CanoeDeFi.Instance.Env).SendTransactionAsync(transaction);
    }
    private async Task<TokenAccount[]> GetOwnedTokenAccounts(string walletPubKey, string tokenMintPubKey, string tokenProgramPublicKey)
    {
        RequestResult<ResponseValue<List<TokenAccount>>> result = await ClientFactory.GetClient(CanoeDeFi.Instance.Env).GetTokenAccountsByOwnerAsync(walletPubKey, tokenMintPubKey, tokenProgramPublicKey);
        if (result.Result != null && result.Result.Value != null)
        {
            return result.Result.Value.ToArray();
        }
        return null;
    }
    private async Task<TokenAccount[]> GetOwnedTokenAccounts(Account account)
    {
        try
        {
            RequestResult<ResponseValue<List<TokenAccount>>> result = await ClientFactory.GetClient(CanoeDeFi.Instance.Env).GetTokenAccountsByOwnerAsync(account.PublicKey, null, TokenProgram.ProgramIdKey);
            if (result.Result != null && result.Result.Value != null)
            {
                return result.Result.Value.ToArray();
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
        return null;
    }

    private async Task<AccountInfo> GetAccountData(PublicKey account)
    {
        RequestResult<ResponseValue<AccountInfo>> result = await ClientFactory.GetClient(CanoeDeFi.Instance.Env).GetAccountInfoAsync(account);
        if (result.Result != null && result.Result.Value != null)
        {
            return result.Result.Value;
        }
        return null;
    }

}
