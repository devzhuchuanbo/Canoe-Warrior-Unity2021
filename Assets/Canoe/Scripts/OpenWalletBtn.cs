using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWalletBtn : MonoBehaviour
{
    public void OpenWallet()
    {
        WalletController.Instance.WalletStart();
    }
}
