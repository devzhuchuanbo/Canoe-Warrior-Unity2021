using System;
using UnityEngine;
using UnityEngine.UI;

public class ChoiseBtn : MonoBehaviour
{
	private void OnEnable()
	{
		this.coin = PlayerPrefs.GetInt("Coin");
		this.coinText.text = this.coin.ToString();
	}

	public void ItemSellected(string iName, int iPrice, Transform t, string iSpecChar)
	{
		foreach (Transform transform in this.item)
		{
			transform.SendMessage("Reset");
		}
		this.itemName = iName;
		this.price = iPrice;
		this.curr = t;
		this.specChar = iSpecChar;
		this.inUse = PlayerPrefs.GetString("TransInUse");
		if (this.itemName == this.inUse)
		{
			this.mua = ChoiseBtn.muako.muaroidangdung;
			this.TextBtn.text = "USE";
			this.TextBtn.color = this.gray;
		}
		else
		{
			int @int = PlayerPrefs.GetInt(this.itemName);
			if (@int == 1)
			{
				this.mua = ChoiseBtn.muako.muaroikodung;
				this.TextBtn.text = "USE";
				this.TextBtn.color = this.gold;
			}
			else if (this.specChar == string.Empty)
			{
				if (this.price <= this.coin)
				{
					this.mua = ChoiseBtn.muako.chuamuonmua;
					this.TextBtn.text = "BUY";
					this.TextBtn.color = this.gold;
				}
				else
				{
					this.mua = ChoiseBtn.muako.deodutienmua;
					this.TextBtn.text = "BUY";
					this.TextBtn.color = this.red;
				}
			}
			else
			{
				this.mua = ChoiseBtn.muako.khoacmnr;
				this.TextBtn.text = "UNLOCK";
				this.TextBtn.color = this.gold;
			}
		}
	}

	public void ClickOk()
	{
		if (this.mua == ChoiseBtn.muako.muaroidangdung)
		{
			this.main.ClickLevelSellectFalse();
		}
		else if (this.mua == ChoiseBtn.muako.muaroikodung)
		{
			this.main.ClickNomal();
			PlayerPrefs.SetString("TransInUse", this.itemName);
			PlayerPrefs.Save();
		}
		else if (this.mua == ChoiseBtn.muako.deodutienmua)
		{
			this.main.ClickLevelSellectFalse();
		}
		else if (this.mua == ChoiseBtn.muako.chuamuonmua)
		{
			this.main.ClickBuy();
			this.coin -= this.price;
			PlayerPrefs.SetInt("Coin", this.coin);
			PlayerPrefs.SetInt(this.itemName, 1);
			PlayerPrefs.Save();
			this.coinText.text = this.coin.ToString();
		}
		else if (this.mua == ChoiseBtn.muako.khoacmnr)
		{
			this.main.ClickBuy();
			this.shopCanvas.gameObject.SetActive(false);
			this.iapCanvas.gameObject.SetActive(true);
		}
		if (this.mua != ChoiseBtn.muako.khoacmnr)
		{
			this.curr.SendMessage("Sellect");
		}
	}

	public Transform[] item;

	public Text TextBtn;

	public Text coinText;

	public Color gold;

	public Color red;

	public Color gray;

	[HideInInspector]
	public int coin;

	private string itemName;

	private string inUse;

	private Transform curr;

	private int price;

	public mainlv main;

	private string specChar;

	public Transform iapCanvas;

	public Transform shopCanvas;

	private ChoiseBtn.muako mua;

	private enum muako
	{
		muaroidangdung,
		muaroikodung,
		chuamuonmua,
		deodutienmua,
		khoacmnr
	}
}
