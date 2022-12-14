using Canoe;
using Solana.Unity.Rpc.Core.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Wallet_GetMoreLives : MonoBehaviour
{
    // Start is called before the first frame update
    public Text InsufficientNotice;
    public void OnEnable()
    {
        InsufficientNotice.text = "";
        if (WalletController.Instance.CurrentAARTTokenAccount!=null)
        {
            double tokenBalance = WalletController.Instance.CurrentAARTTokenAccount.Account.Data.Parsed.Info.TokenAmount.AmountDouble;
            if (WalletController.Instance.CurrentAARTTokenAccount != null)
            {
                if (tokenBalance <= 29)
                    InsufficientNotice.text = "Insufficient";
            }
        }
    }
    public void ConfirmBtn()
    {
        double tokenBalance = 0;

        try
        {
            tokenBalance = WalletController.Instance.CurrentAARTTokenAccount.Account.Data.Parsed.Info.TokenAmount.AmountDouble;
        }
        catch (Exception ex)
        {
            WalletController.Instance.WalletStart();
            this.gameObject.SetActive(false);
            return;
        }

        //tokenBalance = WalletController.Instance.CurrentAARTTokenAccount.Account.Data.Parsed.Info.TokenAmount.AmountDouble;
        if (tokenBalance <= 29)
        {
            WalletController.Instance.WalletStart();
            this.gameObject.SetActive(false);
        }
        else
        {
            //trade
            Func<Task> AARTTransferTask = async () =>
            {
                var transferResult = await WalletController.Instance.TransferToken(WalletController.Instance.CurrentAARTTokenAccount.PublicKey, WalletController.Instance.AARTCANOEADDRESS, CanoeDeFi.Instance.CurrentWallet.GetAccount(0), WalletController.Instance.AARTMINT, 29000000);
                Debug.Log("transfer done :" + transferResult.Reason);
                if (transferResult.Reason == "OK" || transferResult.Reason == "ok")
                {
                    //WalletController.Instance.ShowNotice("The request is successful!");
                    WalletController.Instance.RefreshBanlace();
                    WalletController.Instance.OnReborn();
                    this.gameObject.SetActive(false);
                }
                else
                {
                    Debug.Log("transfer resson:" + transferResult.Reason);
                    WalletController.Instance.ShowNotice("The request is failed!");
                }
            };


            ////trade
            //Func<Task> AARTTransferTask = async () =>
            //{
            //    if (WalletController.Instance.CurrentAARTTokenAccount == null)
            //    {
            //        var SPLResult = await CanoeDeFi.Instance.GetOwnedTokenAccounts();
            //        foreach (var item in SPLResult)
            //        {

            //            if (item.Account.Data.Parsed.Info.Mint == WalletController.Instance.AARTMINT)
            //            {
            //                WalletController.Instance.CurrentAARTTokenAccount = item;
            //            }
            //        }
            //    }
            //    RequestResult<string> transferResult = await CanoeDeFi.Instance.TransferToken(WalletController.Instance.CurrentAARTTokenAccount.PublicKey, WalletController.Instance.AARTCANOEADDRESS, WalletController.Instance.CurrentWallet.GetAccount(0), WalletController.Instance.AARTMINT, 6, 29);

            //    if (transferResult.Reason == "OK" || transferResult.Reason == "ok")
            //    {
            //        WalletController.Instance.ShowNotice("The request is successful!");
            //        WalletController.Instance.RefreshBanlace();
            //        WalletController.Instance.OnReborn();
            //        this.gameObject.SetActive(false);
            //    }
            //    else
            //    {
            //        WalletController.Instance.ShowNotice("The request is failed!");
            //    }

            //};
            AARTTransferTask();
        }
    }

}
