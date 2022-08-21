using Canoe;
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
        SelectSOL();
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
                RequestResult<string> transferResult;
                try
                {
                    transferResult = await WalletController.Instance.TransferToken(WalletController.Instance.CurrentAARTTokenAccount.PublicKey, TargetAddress.text, CanoeDeFi.Instance.CurrentWallet.GetAccount(0), WalletController.Instance.AARTMINT, (ulong)transferAmount);
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

}
