using Canoe;
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
        var tokenResults = await CanoeDeFi.Instance.GetOwnedTokenAccounts();
    }

}
