using Canoe;
using Solana.Unity.Rpc.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Wallet_Homepage : MonoBehaviour
{
    public Text SOLValue;
    public Text AARTValue;
    private async void OnEnable()
    {
      await  UpdateBalance();
      await  UpdateAARTValue();
    }
    public async Task UpdateBalance()
    {
        double solBalance = await CanoeDeFi.Instance.GetSolAmmountAsync();
        SOLValue.text = solBalance.ToString();
        //double aartBalance= await CanoeDeFi.Instance.GetOwnedTokenAccounts(); 
    }
    public async Task UpdateAARTValue()
    {
       TokenAccount[] tokenResults = await CanoeDeFi.Instance.GetOwnedTokenAccounts();
        foreach (var item in tokenResults)
        {
            if (item.Account.Data.Parsed.Info.Mint==WalletController.Instance.AARTMINT)
            {
                WalletController.Instance.CurrentAARTTokenAccount = item;
                AARTValue.text = item.Account.Data.Parsed.Info.TokenAmount.AmountDouble.ToString();
            }
        }
    }

}
