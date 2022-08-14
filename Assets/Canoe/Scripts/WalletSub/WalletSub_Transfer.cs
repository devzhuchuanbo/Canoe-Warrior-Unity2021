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
    public bool isTransferSOL = true;

    private double transferAmount = 0;

    private void OnEnable()
    {
        TargetAddress.text = "";
        TargetAddress.text = "0";
    }
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

            Func<UniTaskVoid> UniTransfer = async () =>
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
            UniTransfer();
        }
        //transfer aart
        else
        {

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
