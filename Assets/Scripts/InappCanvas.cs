using System;
using UnityEngine;
using UnityEngine.UI;

public class InappCanvas : MonoBehaviour
{
	private void Awake()
	{
		//	InAppProject.init();
	}

	private void OnEnable()
	{
		//PlayerPrefs.DeleteAll();
		if (PlayerPrefs.GetInt("AdNumInt") == 1)
		{
			this.adsPrice.gameObject.SetActive(false);
			this.adsPured.gameObject.SetActive(true);
		}


		if (!PlayerPrefs.HasKey("Coin"))
		{
			PlayerPrefs.SetInt("Coin", 0);
			PlayerPrefs.Save();
		}
		if (!PlayerPrefs.HasKey("DM"))
		{
			PlayerPrefs.SetInt("DM", 0);
			PlayerPrefs.Save();
		}
		this.coin = PlayerPrefs.GetInt("Coin");
		this.coinText.text = this.coin.ToString();
		this.dm = PlayerPrefs.GetInt("DM");
		this.dmText.text = this.dm.ToString();
		this.Refresh();
	}

	public void Refresh()
	{
		this.coin = PlayerPrefs.GetInt("Coin");
		this.coinText.text = this.coin.ToString();
		this.dm = PlayerPrefs.GetInt("DM");
		this.dmText.text = this.dm.ToString();
		if (PlayerPrefs.GetInt("Halo") == 1)
		{
			this.haloPrice.gameObject.SetActive(false);
			this.haloPured.gameObject.SetActive(true);
			PlayerPrefs.SetInt("Halo", 1);
			PlayerPrefs.Save();
		}
		else
		{
			this.haloPrice.gameObject.SetActive(true);
			this.haloPured.gameObject.SetActive(false);
		}
	}

	private void Update()
	{
		if (InappCanvas.c != 0)
		{
			this.coin += InappCanvas.c;
			this.coinText.text = this.coin.ToString();
			PlayerPrefs.SetInt("Coin", this.coin);
			PlayerPrefs.Save();
			InappCanvas.c = 0;
		}
		if (InappCanvas.d != 0)
		{
			this.dm += InappCanvas.d;
			this.dmText.text = this.dm.ToString();
			PlayerPrefs.SetInt("DM", this.dm);
			PlayerPrefs.Save();
			InappCanvas.d = 0;
		}
		if (InappCanvas.a)
		{
			this.Refresh();
			InappCanvas.a = false;
		}
	}


	public void AddGolds()
	{
		InappCanvas.c = 20000;
		LogBuy20kGoldEvent();
	}

	public void AddDM()
	{
		InappCanvas.d = 2000;
		LogBuy2kDiamondsEvent();

	}

	public void AddRemoveAds()
	{
		if (!PlayerPrefs.HasKey("AdNumInt"))
		{
			PlayerPrefs.SetInt("AdNumInt", 1);
			PlayerPrefs.Save();
			this.adsPrice.gameObject.SetActive(false);
			this.adsPured.gameObject.SetActive(true);
			InappCanvas.a = true;
			LogBuyNoAdsEvent();
		}
	}

	public void AddHalo()
	{
		if (!PlayerPrefs.HasKey("Halo"))
		{
			PlayerPrefs.SetInt("Halo", 1);
			PlayerPrefs.Save();
			InappCanvas.a = true;
			InappCanvas.d = 6000;
			InappCanvas.c = 60000;
			LogBuyOfferEvent();
		}

	}
	public void LogBuyNoAdsEvent()
	{


	}
	public void LogBuy20kGoldEvent()
	{


	}
	public void LogBuy2kDiamondsEvent()
	{


	}
	public void LogBuyOfferEvent()
	{


	}
	public Text coinText;

	public Text dmText;

	private int coin;

	private int dm;

	public Image adsPrice;

	public Image adsPured;

	public Image haloPrice;

	public Image haloPured;

	public static int d;

	public static int c;

	public static bool a;
}
