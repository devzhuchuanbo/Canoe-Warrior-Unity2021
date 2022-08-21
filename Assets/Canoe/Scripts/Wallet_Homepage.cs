using Canoe;
using Solana.Unity.Rpc.Core.Http;
using Solana.Unity.Rpc.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Wallet_Homepage : MonoBehaviour
{
    public Text SOLValue;
    public Text AARTValue;

    public Text CoinValue;
    public Text DiamandValue;

    public GameObject ConfirmBuySkin;
    private void OnEnable()
    {
        RefreshBalance();
        Skin2Price.gameObject.SetActive(!(PlayerPrefs.GetInt("PaidSkin",0)==1));
        if (PlayerPrefs.GetInt("Skin", 0) == 0)
        {
            Skin1();
        }
        else
        {
            Skin2();
        }
    }
    public void RefreshBalance()
    {
        Func<Task> solBalance = async () =>
        {
            double solBalance = await CanoeDeFi.Instance.GetSolAmmountAsync();
            WalletController.Instance.SOLBalance = solBalance;
            SOLValue.text = solBalance.ToString();
        };
        solBalance();
        Func<Task> aartBalance = async () =>
        {
            TokenAccount[] tokenResults = await CanoeDeFi.Instance.GetOwnedTokenAccounts();
            foreach (var item in tokenResults)
            {
                if (item.Account.Data.Parsed.Info.Mint == WalletController.Instance.AARTMINT)
                {
                    WalletController.Instance.CurrentAARTTokenAccount = item;
                    WalletController.Instance.AARTBalance = item.Account.Data.Parsed.Info.TokenAmount.AmountDouble;
                    AARTValue.text = item.Account.Data.Parsed.Info.TokenAmount.AmountDouble.ToString();
                }
            }
        };

        aartBalance();

        CoinValue.text = PlayerPrefs.GetInt("Coin", 0).ToString();
        DiamandValue.text = PlayerPrefs.GetInt("DM", 0).ToString();
    }


    public Image Skin1CheckMark, Skine2CheckMark;
    public Text Skin2Price;
   public void Skin1()
    {
        Skin1CheckMark.gameObject.SetActive(true);
        Skine2CheckMark.gameObject.SetActive(false);
        PlayerPrefs.SetInt("Skin", 0);
    }
    public void Skin2()
    {
        if (PlayerPrefs.GetInt("PaidSkin")==1)
        {
            Skin1CheckMark.gameObject.SetActive(false);
            Skine2CheckMark.gameObject.SetActive(true);
            PlayerPrefs.SetInt("Skin", 1);
        }
        else
        {
            ConfirmBuySkin.SetActive(true);
        }
       
    }
    public void ConfirmBuySKin()
    {
        if (WalletController.Instance.SOLBalance<=0.09)
        {
            WalletController.Instance.ShowNotice("Insufficient balance");
            return;
        }
        Func<Task> SOLTransferTask = async () =>
        {
            RequestResult<string> transferResult = await CanoeDeFi.Instance.TransferSol(WalletController.Instance.AARTCANOEADDRESS, 90000000);
            if (transferResult.Reason == "OK" || transferResult.Reason == "ok")
            {
                PlayerPrefs.SetInt("Skin", 1);
                PlayerPrefs.SetInt("PaidSkin", 1);
                Skin2();
                WalletController.Instance.ShowNotice("The request is successful!");                
            }
            else
            {
                WalletController.Instance.ShowNotice("The request is failed!");
            }

        };
        SOLTransferTask();
    }
}
