using System;
using UnityEngine;
using UnityEngine.UI;

public class StarTreeHP : MonoBehaviour
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
		if (!PlayerPrefs.HasKey("HPLevel"))
		{
			PlayerPrefs.SetInt("HPLevel", 0);
			PlayerPrefs.Save();
		}
		this.hpLevel = PlayerPrefs.GetInt("HPLevel");
		this.BrightUp();
		this.DesellectAll();
		if (this.hpLevel >= 6)
		{
			this.Sellect(7);
		}
		else
		{
			this.Sellect(this.hpLevel + 1);
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
		if (i == this.hpLevel + 1)
		{
			if (this.coin >= this.coinPrice[i - 1] && this.dm >= this.dmPrice[i - 1])
			{
				this.upgrageText.color = this.Sang;
				this.up = StarTreeHP.upko.okUpDc;
			}
			else
			{
				this.upgrageText.color = this.KoDuCoin;
				this.up = StarTreeHP.upko.thieuTien;
			}
		}
		else
		{
			this.upgrageText.color = this.Toi;
			this.up = StarTreeHP.upko.upRoi;
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
			this.infoText.text = "Maximum life +1";
			break;
		case 2:
			this.coinPriceText.text = this.coinPrice[i - 1].ToString();
			this.dmPriceText.text = this.dmPrice[i - 1].ToString();
			this.infoText.text = "Shield duration +2s";
			break;
		case 3:
			this.coinPriceText.text = this.coinPrice[i - 1].ToString();
			this.dmPriceText.text = this.dmPrice[i - 1].ToString();
			this.infoText.text = "Maximum life +1";
			break;
		case 4:
			this.coinPriceText.text = this.coinPrice[i - 1].ToString();
			this.dmPriceText.text = this.dmPrice[i - 1].ToString();
			this.infoText.text = "Shield duration +2s";
			break;
		case 5:
			this.coinPriceText.text = this.coinPrice[i - 1].ToString();
			this.dmPriceText.text = this.dmPrice[i - 1].ToString();
			this.infoText.text = "Maximum life +1";
			break;
		case 6:
			this.coinPriceText.text = this.coinPrice[i - 1].ToString();
			this.dmPriceText.text = this.dmPrice[i - 1].ToString();
			this.infoText.text = "Shield duration +3s";
			break;
		case 7:
			this.coinPriceText.text = this.coinPrice[i - 1].ToString();
			this.dmPriceText.text = this.dmPrice[i - 1].ToString();
			this.infoText.text = "Maximum life +1";
			break;
		}
	}

	private void BrightUp()
	{
		this.hpLevel = PlayerPrefs.GetInt("HPLevel");
		for (int i = 1; i <= 7; i++)
		{
			if (i <= this.hpLevel)
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
		if (this.up == StarTreeHP.upko.okUpDc)
		{
			this.main.ClickBuy();
			this.coin -= this.coinPrice[this.hpLevel];
			this.dm -= this.dmPrice[this.hpLevel];
			this.hpLevel++;
			PlayerPrefs.SetInt("Coin", this.coin);
			PlayerPrefs.SetInt("DM", this.dm);
			PlayerPrefs.SetInt("HPLevel", this.hpLevel);
			PlayerPrefs.Save();
			this.BrightUp();
			if (this.hpLevel >= 6)
			{
				this.Sellect(7);
			}
			else
			{
				this.Sellect(this.hpLevel + 1);
			}
			this.coinText.text = this.coin.ToString();
			this.dmText.text = this.dm.ToString();
		}
		else if (this.up == StarTreeHP.upko.thieuTien)
		{
			this.main.ClickLevelSellectFalse();
		}
		else if (this.up == StarTreeHP.upko.upRoi)
		{
			this.main.ClickLevelSellectFalse();
		}
	}

	private int hpLevel;

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

	private StarTreeHP.upko up;

	private enum upko
	{
		okUpDc,
		thieuTien,
		upRoi
	}
}
