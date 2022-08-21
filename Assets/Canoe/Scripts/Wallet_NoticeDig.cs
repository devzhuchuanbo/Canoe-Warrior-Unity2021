using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wallet_NoticeDig : MonoBehaviour
{
    public Text msg;


    public void ShowAARTTrad()
    {
        msg.text = " AART is insufficient \n [Trade to get AART]";
    }

    public void CanelBtn()
    {
        this.gameObject.SetActive(false);
    }

    public void ConfirmBtn()
    {
        WalletController.Instance.WalletStart();
        this.gameObject.SetActive(false);
    }

}
