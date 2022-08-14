using Canoe;
using Solana.Unity.SDK;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wallet_ImportWallet : MonoBehaviour
{
    public InputField inputField;
    public void ConfirmBtn()
    {
        if (string.IsNullOrEmpty(inputField.text))
        {
            WalletController.Instance.ShowNotice("Mnemonic cannot be empty");
        }
        try
        {
            //check if the mnemonic currect ;
            if (!WalletKeyPair.CheckMnemonicValidity(inputField.text))
            {
                WalletController.Instance.ShowNotice("Mnemonic is in incorect format");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }

        bool tf = CanoeDeFi.Instance.RestoreWalletWithMenmonic(inputField.text, WalletController.Instance.PASSWORD);
        if (tf)
        {
            //enter the homepage of wallet
            WalletController.Instance.ShowWalletHomePage();
            this.gameObject.SetActive(false);
            WalletController.Instance.Panel_NewUser.SetActive(false);
        }
        else
        {
            WalletController.Instance.ShowNotice("network error, please try again later");
        }
    }
}
