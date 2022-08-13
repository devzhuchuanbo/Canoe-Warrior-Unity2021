using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wallet_Notice : MonoBehaviour
{
    public Text text;
    public void ShowNotice(string msg)
    {
        text.text = msg;
    }
}
