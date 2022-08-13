using Canoe;
using Solana.Unity.Rpc.Core.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WalletSub_Transfer : MonoBehaviour
{
    public InputField TargetAddress;
    public InputField Amount;
    public Image TokenImage;
    public bool isTransferSOL = true;

    private UInt32 transferAmount = 0;
    public void SelectSOL()
    {
        isTransferSOL = true;
    }
    public void SelectToken()
    {
        isTransferSOL = false;
    }
    public void SetAllBtn()
    { 

    }
    public async void TransferBtn()
    {
        if (string.IsNullOrEmpty(TargetAddress.text))
        {
            WalletController.Instance.ShowNotice("address can't be empty");
            return;
        }
       
        try
        {
            transferAmount = Convert.ToUInt32(Amount.text);
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
            RequestResult<string> transferResult = await CanoeDeFi.Instance.TransferSol(TargetAddress.text, transferAmount);
            if (transferResult.Reason!="OK"|| transferResult.Reason != "ok")
            {
                WalletController.Instance.ShowNotice("The request is successful!");
            }
            else
            {
                WalletController.Instance.ShowNotice("The request is failed!");
            }
        }
    }
}
