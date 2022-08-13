using Canoe;
using Solana.Unity.Wallet.Bip39;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Wallet_GenerateWalletConfirm : MonoBehaviour
{
    public Text[] Texts;
    public Button[] Selections;
    private void OnEnable()
    {
        Reset();
    }
    private void Reset()
    {
        foreach (var item in Texts)
        {
            item.text = "--";
        }
        // fill the selections
        Mnemonic mnemonic = WalletController.Instance.Mnemonic;
        string[] words = mnemonic.ToString().Split(" ");
        if (words.Length != 12)
        {
            Debug.LogError("words length error");
            throw new System.Exception("words length error");
        }
        string[] randomWords = GetRandomWords(words);

        for (int i = 0; i < Selections.Length; i++)
        {
            Selections[i].GetComponent<Text>().text = words[i];
            // add click events
            Selections[i].onClick.AddListener(delegate () { OnSelectionCick(i, Selections[i].GetComponent<Text>()); });
        }

    }
    public void OnSelectionCick(int index,Text text)
    {
        if (string.IsNullOrEmpty(text.text))
        {
            return;
        }
        //fill mnemonic text
        Debug.Log($"Index of{index} clicked, hold string{text.text}");
        Texts[index].text=text.text;
        text.text = "" ;

        //check if is the last one
        bool isLast = false;
        foreach (var item in Texts)
        {
            if (!string.IsNullOrEmpty(item.text))
            {
                isLast = false;
            }
        }


        //check result
        if (isLast)
        {
         bool tf=CheckResult();
            // wrong mnemonic
            if (!tf)
            {
               WalletController.Instance.ShowNotice("Incorrect mnemonic");
                Reset();
            }
            else
            {
                //create wallet
                //enter wallet page
                CanoeDeFi.Instance.LoginWithNewGeneratedWallet(WalletController.Instance.Mnemonic,WalletController.Instance.PASSWORD);
            }
        }
    }

    private  bool CheckResult()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < Texts.Length; i++)
        {
            sb.Append(Texts[i].text);
            if (i!= Texts.Length-1)
            {
                sb.Append(" ");
            }
        }

        Mnemonic _selected = new Mnemonic(sb.ToString());
        if (_selected==WalletController.Instance.Mnemonic)
        {
            return true;
        }
        return false;
    }

    private string[] GetRandomWords(string[] wordsDic)
    {
        for (int i = 0; i < wordsDic.Length; i++)
        {
            string temp = wordsDic[i];
            int randomIndex = Random.Range(0, wordsDic.Length);
            wordsDic[i] = wordsDic[randomIndex];
            wordsDic[randomIndex] = temp;
        }
        return wordsDic;
    }
}