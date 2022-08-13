using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemScript : MonoBehaviour
{
	private void OnEnable()
	{
		if (this.specChar == string.Empty)
		{
			this.priceText = this.tPrice.GetComponent<Text>();
			this.priceText.text = this.price.ToString();
		}
		this.Reset();
	}

	public void Reset()
	{
		this.quangSang.gameObject.SetActive(false);
		this.inUse = PlayerPrefs.GetString("TransInUse");
		this.coin = PlayerPrefs.GetInt("Coin");
		if (this.itemName == this.inUse)
		{
			this.tPrice.gameObject.SetActive(false);
			this.tBuyed.gameObject.SetActive(false);
			this.tInUse.gameObject.SetActive(true);
		}
		else
		{
			int @int = PlayerPrefs.GetInt(this.itemName);
			if (@int == 1)
			{
				this.tPrice.gameObject.SetActive(false);
				this.tBuyed.gameObject.SetActive(true);
				this.tInUse.gameObject.SetActive(false);
			}
			else if (this.price <= this.coin)
			{
				this.tPrice.gameObject.SetActive(true);
				this.tBuyed.gameObject.SetActive(false);
				this.tInUse.gameObject.SetActive(false);
			}
			else
			{
				this.tPrice.gameObject.SetActive(true);
				this.tBuyed.gameObject.SetActive(false);
				this.tInUse.gameObject.SetActive(false);
			}
		}
	}

	public void Sellect()
	{
		this.choiseButton.gameObject.SetActive(true);
		this.choiseButton.ItemSellected(this.itemName, this.price, base.transform, this.specChar);
		this.quangSang.gameObject.SetActive(true);
	}

	public ChoiseBtn choiseButton;

	public Transform quangSang;

	public Transform tPrice;

	public Transform tBuyed;

	public Transform tInUse;

	public string itemName;

	public int price;

	private string inUse;

	private int coin;

	private Text priceText;

	public string specChar;
}
