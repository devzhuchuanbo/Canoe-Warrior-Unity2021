using Canoe;
using Cysharp.Threading.Tasks;
using Solana.Unity.Programs;
using Solana.Unity.Rpc;
using Solana.Unity.Rpc.Builders;
using Solana.Unity.Rpc.Core.Http;
using Solana.Unity.Rpc.Messages;
using Solana.Unity.Rpc.Models;
using Solana.Unity.Wallet;
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
    public Text TokenName;
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
        TokenName.text = "SOL";
    }
    public void SelectToken()
    {
        isTransferSOL = false;
        TokenImage.sprite = AARTSprite;
        Balance.text = wallet_Homepage.AARTValue.text;
        TokenName.text = "AART";
    }
    public void SetAllBtn()
    {
        if (isTransferSOL)
        {
            Amount.text = (Convert.ToDouble(wallet_Homepage.SOLValue.text) - 0.001).ToString();
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
                //Debug.Log("Prepare to transfer");
                Debug.Log("Prepare to transfer");
                RequestResult<string> transferResult;
                try
                {
                    transferResult = await TransferToken(WalletController.Instance.CurrentAARTTokenAccount.PublicKey, TargetAddress.text, CanoeDeFi.Instance.CurrentWallet.GetAccount(0), WalletController.Instance.AARTMINT, (ulong)transferAmount);
                    Debug.Log("transfer done :" + transferResult.Reason);
                    if (transferResult.Reason == "OK" || transferResult.Reason == "ok")
                    {
                        WalletController.Instance.ShowNotice("The request is successful!");
                        WalletController.Instance.RefreshBanlace();
                    }
                    else
                    {
                        Debug.Log("transfer resson:" + transferResult.Reason);
                        WalletController.Instance.ShowNotice("The request is failed!");
                    }
                }

                catch (Exception ex)
                {
                    Debug.LogError(ex.Message);
                }

            };
            AARTTransferTask();


        }

    }
    public async Task<RequestResult<string>> TransferToken(string sourceTokenAccount, string toWalletAccount, Account sourceAccountOwner, string tokenMint, ulong amount = 1)
    {
        Debug.Log("TransferToken222 Invoke");
        PublicKey associatedTokenAccountOwner = new PublicKey(toWalletAccount);
        Debug.Log("associatedTokenAccountOwner: " + associatedTokenAccountOwner);
        PublicKey mint = new PublicKey(tokenMint);
        Debug.Log("mint: " + tokenMint);
        Account ownerAccount = CanoeDeFi.Instance.CurrentWallet.GetAccount(0);
        Debug.Log("ownerAccount: " + ownerAccount);
        PublicKey associatedTokenAccount = AssociatedTokenAccountProgram.DeriveAssociatedTokenAccount(associatedTokenAccountOwner, new PublicKey(tokenMint));
        Debug.Log("associatedTokenAccount: " + associatedTokenAccount);

        RequestResult<ResponseValue<BlockHash>> blockHash = await ClientFactory.GetClient(CanoeDeFi.Instance.Env).GetRecentBlockHashAsync();
        Debug.Log("blockHash: " + blockHash);
        RequestResult<ulong> rentExemptionAmmount = await ClientFactory.GetClient(CanoeDeFi.Instance.Env).GetMinimumBalanceForRentExemptionAsync(TokenProgram.TokenAccountDataSize);
        //TokenAccount[] lortAccounts = await GetOwnedTokenAccounts(toWalletAccount, tokenMint, "");
        Debug.Log("rentExemptionAmmount: " + rentExemptionAmmount);
        TokenAccount[] lortAccounts = await GetOwnedTokenAccounts(toWalletAccount, tokenMint, TokenProgram.ProgramIdKey);
        Debug.Log("lortAccounts: " + lortAccounts);
        byte[] transaction;
        //try to make sure is the account already have a token account
        var info = await GetAccountData(associatedTokenAccount);
        Debug.Log("info: " + info);
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
                    9,//token小数点精度
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
