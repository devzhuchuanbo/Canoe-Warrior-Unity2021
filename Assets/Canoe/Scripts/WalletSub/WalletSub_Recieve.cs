using Canoe;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WalletSub_Recieve : MonoBehaviour
{
    public Text Addrss;
    public RawImage QRCode;
    private void OnEnable()
    {
        Addrss.text = CanoeDeFi.Instance.CurrentWallet.Account.PublicKey;
        //TODO: QRCode
        QRCode.texture = QRHelper.GenerateQRImage(Addrss.text);
    }
    public void CotyAddress()
    {
        UniClipboard.SetText(CanoeDeFi.Instance.CurrentWallet.Account.PublicKey);
    }
}
