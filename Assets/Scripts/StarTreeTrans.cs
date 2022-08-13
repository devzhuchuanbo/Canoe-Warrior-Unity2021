using System;
using UnityEngine;
using UnityEngine.UI;

public class StarTreeTrans : MonoBehaviour
{
	private void OnEnable()
	{
		if (!PlayerPrefs.HasKey("Coin"))
		{
			PlayerPrefs.SetInt("Coin", 0);
			PlayerPrefs.Save();
		}
		this.coin = PlayerPrefs.GetInt("Coin");
		this.coinText.text = this.coin.ToString();
		if (!PlayerPrefs.HasKey("DM"))
		{
			PlayerPrefs.SetInt("DM", 0);
			PlayerPrefs.Save();
		}
		this.dm = PlayerPrefs.GetInt("DM");
		this.dmText.text = this.dm.ToString();
		if (!PlayerPrefs.HasKey("TransLevel"))
		{
			PlayerPrefs.SetInt("TransLevel", 0);
			PlayerPrefs.Save();
		}
		this.transLevel = PlayerPrefs.GetInt("TransLevel");
		this.BrightUp();
		this.DesellectAll();
		if (this.transLevel >= 6)
		{
			this.Sellect(7);
		}
		else
		{
			this.Sellect(this.transLevel + 1);
		}
	}

	public void DesellectAll()
	{
		foreach (Transform transform in this.v)
		{
			transform.gameObject.SetActive(false);
		}
	}

	public void Sellect(int i)
	{
		this.DesellectAll();
		this.v[i - 1].gameObject.SetActive(true);
		if (i == this.transLevel + 1)
		{
			if (this.coin >= this.coinPrice[i - 1] && this.dm >= this.dmPrice[i - 1])
			{
				this.upgrageText.color = this.Sang;
				this.up = StarTreeTrans.upko.okUpDc;
			}
			else
			{
				this.upgrageText.color = this.KoDuCoin;
				this.up = StarTreeTrans.upko.thieuTien;
			}
		}
		else
		{
			this.upgrageText.color = this.Toi;
			this.up = StarTreeTrans.upko.upRoi;
		}
		this.ShowInfo(i);
	}

	private void ShowInfo(int i)
	{
		switch (i)
		{
		case 1:
			this.coinPriceText.text = this.coinPrice[i - 1].ToString();
			this.dmPriceText.text = this.dmPrice[i - 1].ToString();
			this.infoText.text = "Cooldown -1s";
			break;
		case 2:
			this.coinPriceText.text = this.coinPrice[i - 1].ToString();
			this.dmPriceText.text = this.dmPrice[i - 1].ToString();
			this.infoText.text = "Disguise duration +1s";
			break;
		case 3:
			this.coinPriceText.text = this.coinPrice[i - 1].ToString();
			this.dmPriceText.text = this.dmPrice[i - 1].ToString();
			this.infoText.text = "Cooldown -1s";
			break;
		case 4:
			this.coinPriceText.text = this.coinPrice[i - 1].ToString();
			this.dmPriceText.text = this.dmPrice[i - 1].ToString();
			this.infoText.text = "Disguise duration +1s";
			break;
		case 5:
			this.coinPriceText.text = this.coinPrice[i - 1].ToString();
			this.dmPriceText.text = this.dmPrice[i - 1].ToString();
			this.infoText.text = "Cooldown -1s";
			break;
		case 6:
			this.coinPriceText.text = this.coinPrice[i - 1].ToString();
			this.dmPriceText.text = this.dmPrice[i - 1].ToString();
			this.infoText.text = "Disguise duration +1s";
			break;
		case 7:
			this.coinPriceText.text = this.coinPrice[i - 1].ToString();
			this.dmPriceText.text = this.dmPrice[i - 1].ToString();
			this.infoText.text = "Cooldown -2s";
			break;
		}
	}

	private void BrightUp()
	{
		this.transLevel = PlayerPrefs.GetInt("TransLevel");
		for (int i = 1; i <= 7; i++)
		{
			if (i <= this.transLevel)
			{
				this.br[i - 1].gameObject.SetActive(true);
			}
			else
			{
				this.br[i - 1].gameObject.SetActive(false);
			}
		}
	}

	public void UpgrageIt()
	{
		if (this.up == StarTreeTrans.upko.okUpDc)
		{
			this.main.ClickBuy();
			this.coin -= this.coinPrice[this.transLevel];
			this.dm -= this.dmPrice[this.transLevel];
			this.transLevel++;
			PlayerPrefs.SetInt("Coin", this.coin);
			PlayerPrefs.SetInt("DM", this.dm);
			PlayerPrefs.SetInt("TransLevel", this.transLevel);
			PlayerPrefs.Save();
			this.BrightUp();
			if (this.transLevel >= 6)
			{
				this.Sellect(7);
			}
			else
			{
				this.Sellect(this.transLevel + 1);
			}
			this.coinText.text = this.coin.ToString();
			this.dmText.text = this.dm.ToString();
		}
		else if (this.up == StarTreeTrans.upko.thieuTien)
		{
			this.main.ClickLevelSellectFalse();
		}
		else if (this.up == StarTreeTrans.upko.upRoi)
		{
			this.main.ClickLevelSellectFalse();
		}
	}

	private int transLevel;

	public Transform[] v;

	public Transform[] br;

	public int[] coinPrice;

	public int[] dmPrice;

	public Color Sang;

	public Color Toi;

	public Color KoDuCoin;

	public Text coinPriceText;

	public Text dmPriceText;

	public Text infoText;

	public Text upgrageText;

	public Text coinText;

	public Text dmText;

	private int coin;

	private int dm;

	public mainlv main;

	private StarTreeTrans.upko up;

	private enum upko
	{
		okUpDc,
		thieuTien,
		upRoi
	}
}
