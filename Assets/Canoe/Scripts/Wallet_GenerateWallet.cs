using Canoe;
using Solana.Unity.Wallet.Bip39;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wallet_GenerateWallet : MonoBehaviour
{
    public Text[] Texts;
    private void OnEnable()
    {
        Mnemonic mnemonic = CanoeDeFi.Instance.GenerateNewWallet();
        WalletController.Instance.Mnemonic = mnemonic; ;
        string[] words = mnemonic.ToString().Split(" ");
        if (words.Length != 12)
        {
            Debug.LogError("words length error");
            throw new System.Exception("words length error");
        }
        for (int i = 0; i < Texts.Length; i++)
        {
            Texts[i].text = words[i];
        }
    }
}
