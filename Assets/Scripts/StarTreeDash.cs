using System;
using UnityEngine;
using UnityEngine.UI;

public class StarTreeDash : MonoBehaviour
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
		if (!PlayerPrefs.HasKey("DashLevel"))
		{
			PlayerPrefs.SetInt("DashLevel", 0);
			PlayerPrefs.Save();
		}
		this.dashLevel = PlayerPrefs.GetInt("DashLevel");
		this.BrightUp();
		this.DesellectAll();
		if (this.dashLevel >= 6)
		{
			this.Sellect(7);
			this.shield.gameObject.SetActive(true);
		}
		else
		{
			this.Sellect(this.dashLevel + 1);
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
		if (i == this.dashLevel + 1)
		{
			if (this.coin >= this.coinPrice[i - 1] && this.dm >= this.dmPrice[i - 1])
			{
				this.upgrageText.color = this.Sang;
				this.up = StarTreeDash.upko.okUpDc;
			}
			else
			{
				this.upgrageText.color = this.KoDuCoin;
				this.up = StarTreeDash.upko.thieuTien;
			}
		}
		else
		{
			this.upgrageText.color = this.Toi;
			this.up = StarTreeDash.upko.upRoi;
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
			this.infoText.text = "Cooldown -1s";
			break;
		case 3:
			this.coinPriceText.text = this.coinPrice[i - 1].ToString();
			this.dmPriceText.text = this.dmPrice[i - 1].ToString();
			this.infoText.text = "Cooldown -1s";
			break;
		case 4:
			this.coinPriceText.text = this.coinPrice[i - 1].ToString();
			this.dmPriceText.text = this.dmPrice[i - 1].ToString();
			this.infoText.text = "Cooldown -1s";
			break;
		case 5:
			this.coinPriceText.text = this.coinPrice[i - 1].ToString();
			this.dmPriceText.text = this.dmPrice[i - 1].ToString();
			this.infoText.text = "Cooldown -1s";
			break;
		case 6:
			this.coinPriceText.text = this.coinPrice[i - 1].ToString();
			this.dmPriceText.text = this.dmPrice[i - 1].ToString();
			this.infoText.text = "Cooldown -1s";
			break;
		case 7:
			this.coinPriceText.text = this.coinPrice[i - 1].ToString();
			this.dmPriceText.text = this.dmPrice[i - 1].ToString();
			this.infoText.text = "Cooldown -1s                       Destroys bear traps";
			break;
		}
	}

	private void BrightUp()
	{
		this.dashLevel = PlayerPrefs.GetInt("DashLevel");
		for (int i = 1; i <= 7; i++)
		{
			if (i <= this.dashLevel)
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
		if (this.up == StarTreeDash.upko.okUpDc)
		{
			this.main.ClickBuy();
			this.coin -= this.coinPrice[this.dashLevel];
			this.dm -= this.dmPrice[this.dashLevel];
			this.dashLevel++;
			PlayerPrefs.SetInt("Coin", this.coin);
			PlayerPrefs.SetInt("DM", this.dm);
			PlayerPrefs.SetInt("DashLevel", this.dashLevel);
			PlayerPrefs.Save();
			this.BrightUp();
			if (this.dashLevel >= 6)
			{
				this.Sellect(7);
				this.shield.gameObject.SetActive(true);
			}
			else
			{
				this.Sellect(this.dashLevel + 1);
			}
			this.coinText.text = this.coin.ToString();
			this.dmText.text = this.dm.ToString();
		}
		else if (this.up == StarTreeDash.upko.thieuTien)
		{
			this.main.ClickLevelSellectFalse();
		}
		else if (this.up == StarTreeDash.upko.upRoi)
		{
			this.main.ClickLevelSellectFalse();
		}
	}

	private int dashLevel;

	public Transform shield;

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

	private StarTreeDash.upko up;

	private enum upko
	{
		okUpDc,
		thieuTien,
		upRoi
	}
}
