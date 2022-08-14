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

    private Queue<Text> textQueue = new Queue<Text>();
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
        textQueue.Clear();
        textQueue = new Queue<Text>(Texts);


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
            Selections[i].GetComponent<GernerateWalletConfirmItem>().word = words[i];
        }
    }
    public void OnSelectionCick(Text text)
    {
        if (string.IsNullOrEmpty(text.text))
        {
            return;
        }
        //fill a mnemonic text
        //Debug.Log($"Index of{index} clicked, hold string{text.text}");
        Text _text = textQueue.Dequeue();
        _text.text = text.text;
        text.text = "";


        //check if is the last one
        if (textQueue.Count==0)
        {
            bool tf = CheckResult();
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
                CanoeDeFi.Instance.LoginWithNewGeneratedWallet(WalletController.Instance.Mnemonic, WalletController.Instance.PASSWORD);
                WalletController.Instance.ShowWalletHomePage();
                this.gameObject.SetActive(false);
                Debug.Log("New wallet login~");
            }
        }
    }

    private bool CheckResult()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < Texts.Length; i++)
        {
            sb.Append(Texts[i].text);
            if (i != Texts.Length - 1)
            {
                sb.Append(" ");
            }
        }

        Mnemonic _selected = new Mnemonic(sb.ToString());
        if (_selected.ToString() == WalletController.Instance.Mnemonic.ToString())
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