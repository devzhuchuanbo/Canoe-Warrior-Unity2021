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

    private void OnEnable()
    {
        RefreshBalance();
    }
    public void RefreshBalance()
    {
        Func<Task> solBalance = async () =>
        {
            double solBalance = await CanoeDeFi.Instance.GetSolAmmountAsync();
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
                    AARTValue.text = item.Account.Data.Parsed.Info.TokenAmount.AmountDouble.ToString();
                }
            }
        };

        aartBalance();

        CoinValue.text = PlayerPrefs.GetInt("Coin", 0).ToString();
        DiamandValue.text = PlayerPrefs.GetInt("DM", 0).ToString();
    }


}
