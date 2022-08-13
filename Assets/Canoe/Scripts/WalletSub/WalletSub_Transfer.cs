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
    public void TestBtn()
    {
        Debug.Log("Start transfer");
        Func<UniTaskVoid> ClickAfterSeconds = async () =>
        {
            while (true)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(5.0));                 //µ»¥˝5√Î÷” 
                Debug.Log("5s");
            }

        };
        ClickAfterSeconds();

        Debug.Log("Done");

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

            Func<UniTaskVoid> ClickAfterSeconds = async () =>
            {
                //while (true)
                //{
                    //await UniTask.Delay(TimeSpan.FromSeconds(5.0));                 //µ»¥˝5√Î÷” 
                    //await UniTask.Delay(TimeSpan.FromSeconds(5.0));                 //µ»¥˝5√Î÷”
                    RequestResult<string> transferResult = await CanoeDeFi.Instance.TransferSol(TargetAddress.text, (ulong)transferAmount);
                    Debug.Log("5s");
                    if (transferResult.Reason == "OK" || transferResult.Reason == "ok")
                    {
                        WalletController.Instance.ShowNotice("The request is successful!");
                    }
                    else
                    {
                        WalletController.Instance.ShowNotice("The request is failed!");
                    }
                //}

            };
            ClickAfterSeconds();

            //RequestResult<string> transferResult = await CanoeDeFi.Instance.TransferSol(TargetAddress.text, (ulong)transferAmount);
            //if (transferResult.Reason!="OK"|| transferResult.Reason != "ok")
            //{
            //    WalletController.Instance.ShowNotice("The request is successful!");
            //}
            //else
            //{
            //    WalletController.Instance.ShowNotice("The request is failed!");
            //}
        }
    }
}
