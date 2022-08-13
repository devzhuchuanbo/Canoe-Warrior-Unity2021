using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GernerateWalletConfirmItem : MonoBehaviour
{
    [HideInInspector]
    public string word;
    [HideInInspector]
    public int index;
    public Wallet_GenerateWalletConfirm wallet_GenerateWalletConfirm;

    Text text;

    private void Start()
    {
        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(OnBtnClick);
        text = GetComponent<Text>();
    }
    public void OnBtnClick()
    {
        wallet_GenerateWalletConfirm.OnSelectionCick(index, text);
    }
}
