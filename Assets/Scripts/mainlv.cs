using System;
using UnityEngine;
using UnityEngine.UI;

public class mainlv : MonoBehaviour
{
	private void Start()
	{
		if (!PlayerPrefs.HasKey("AudioValue"))
		{
			PlayerPrefs.SetFloat("AudioValue", 1f);
			PlayerPrefs.SetInt("MusicOn", 1);
			PlayerPrefs.SetString("TransInUse", "bienhinh_goccay");
			PlayerPrefs.SetInt("bienhinh_goccay", 1);
			PlayerPrefs.Save();
		}
		if (!PlayerPrefs.HasKey("TransInUse"))
		{
			PlayerPrefs.SetString("TransInUse", "bienhinh_goccay");
			PlayerPrefs.SetInt("bienhinh_goccay", 1);
			PlayerPrefs.Save();
		}
		if (PlayerPrefs.GetInt("MusicOn") == 1)
		{
			this.themeMusic.Play();
		}
		if (!PlayerPrefs.HasKey("G11"))
		{
			PlayerPrefs.SetInt("G11", 1);
			this.hd.gameObject.SetActive(true);
			PlayerPrefs.Save();
		}
		AudioListener.volume = PlayerPrefs.GetFloat("AudioValue");
		if (PlayerPrefs.GetFloat("AudioValue") == 0f)
		{
			this.musiconoff.text = "Music : Off";
		}
		else
		{
			this.musiconoff.text = "Music : On";
		}
		if (this.gm)
		{
			PlayerPrefs.SetInt("Coin", 100000);
			PlayerPrefs.SetInt("DM", 10000);
			PlayerPrefs.Save();
		}
		//this.adCT = GameObject.Find("AdController").GetComponent<AdCreator>();
		//this.adCT.CheckAd();
		this.activeGift = true;
		base.InvokeRepeating("CheckAd", 1f, 1f);
		if (!PlayerPrefs.HasKey("RateCountDown"))
		{
			PlayerPrefs.SetInt("RateCountDown", 3);
			PlayerPrefs.Save();
			this.rateCountDown = 3;
		}
		else
		{
			this.rateCountDown = PlayerPrefs.GetInt("RateCountDown");
			this.rateCountDown--;
			PlayerPrefs.SetInt("RateCountDown", this.rateCountDown);
			PlayerPrefs.Save();
		}
		if (this.rateCountDown < 1)
		{
			this.levelChoise.gameObject.SetActive(false);
			this.RatePanel.gameObject.SetActive(true);
		}
		Resources.UnloadUnusedAssets();
	}

	private void Update()
	{
		if (this.videosHas > 0)
		{
			if (!this.activeGift)
			{
				this.FreeGift.gameObject.SetActive(true);
				this.activeGift = true;
			}
		}
		else if (this.activeGift)
		{
			this.FreeGift.gameObject.SetActive(false);
			this.activeGift = false;
		}
	}

	private void CheckAd()
	{
		//this.adCT.CheckAd();
	}

	public void ShowRewardAd()
	{
	//	this.adCT.CheckAd();
	//	if (AdCreator.GiftAdReady)
	//	{
		//	this.adCT.ShowFullAD(0);
		//}
	}

	public void ShowComplete()
	{
		int num = PlayerPrefs.GetInt("Coin");
		int num2 = PlayerPrefs.GetInt("DM");
		num += 2000;
		num2 += 50;
		this.coinText.text = num.ToString();
		this.dmText.text = num2.ToString();
		PlayerPrefs.SetInt("Coin", num);
		PlayerPrefs.SetInt("DM", num2);
		PlayerPrefs.Save();
		this.videosHas--;
		this.addcoin.gameObject.SetActive(true);
	}

	public void lvLoad(string lv)
	{
		this.loading.SetActive(true);
		UnityEngine.SceneManagement.SceneManager.LoadScene(lv);
	}

	public void ClickLevelSellect()
	{
		this.clickLevel.Play();
	}

	public void ClickLevelSellectFalse()
	{
		this.clickLevelFalse.Play();
	}

	public void ClickPlayButton()
	{
		this.clickPlay.Play();
	}

	public void ClickNomal()
	{
		this.clickNomal.Play();
	}

	public void ClickBuy()
	{
		this.clickBuy.Play();
	}

	public void ClickWin()
	{
		this.clickWin.Play();
	}

	public void RateOK()
	{
		PlayerPrefs.SetInt("RateCountDown", 100);
		PlayerPrefs.Save();
		Application.OpenURL("");
	}

	public void RateNo()
	{
		PlayerPrefs.SetInt("RateCountDown", 30);
		PlayerPrefs.Save();
	}

	public void RateLater()
	{
		PlayerPrefs.SetInt("RateCountDown", 10);
		PlayerPrefs.Save();
	}

	public void Policy()
	{
		Application.OpenURL("");
	}

	public void MusicOnOff()
	{
		if (PlayerPrefs.GetFloat("AudioValue") == 0f)
		{
			this.musiconoff.text = "Music : On";
			PlayerPrefs.SetFloat("AudioValue", 1f);
			PlayerPrefs.Save();
		}
		else
		{
			this.musiconoff.text = "Music : Off";
			PlayerPrefs.SetFloat("AudioValue", 0f);
			PlayerPrefs.Save();
		}
		AudioListener.volume = PlayerPrefs.GetFloat("AudioValue");
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	public GameObject loading;

	public GameObject levelChoise;

	public GameObject RatePanel;

	public AudioSource clickLevel;

	public AudioSource clickLevelFalse;

	public AudioSource clickPlay;

	public AudioSource clickNomal;

	public AudioSource clickBuy;

	public AudioSource clickWin;

	public AudioSource themeMusic;

	public Transform hd;

	public Transform addcoin;

	private AdCreator adCT;

	private bool activeGift;

	public int videosHas;

	public Transform FreeGift;

	public Text coinText;

	public Text dmText;

	public Text musiconoff;

	public bool gm;

	private int rateCountDown;
}
