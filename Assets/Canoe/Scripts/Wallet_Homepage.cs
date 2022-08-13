using Canoe;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wallet_Homepage : MonoBehaviour
{
    public Text SOLValue;
    public Text AARTValue;
    
    public async void UpdateBalance()
    { 
   double solBalance= await CanoeDeFi.Instance.GetSolAmmountAsync(); 
   //double aartBalance= await CanoeDeFi.Instance.GetOwnedTokenAccounts(); 
    }
    public async void UpdateAARTValue()
    {
       var tokenResults= await CanoeDeFi.Instance.GetOwnedTokenAccounts(); 
    }
    public void RecieveBtn()
    { 
    }
    public void TransferBtn()
    { 
    }
    public void TrandeBtn()
    { 
    }
}
