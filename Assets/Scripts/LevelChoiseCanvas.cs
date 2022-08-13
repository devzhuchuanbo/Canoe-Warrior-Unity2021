using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelChoiseCanvas : MonoBehaviour
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
		this.playButton.gameObject.SetActive(false);
		this.ScrollX3.gameObject.SetActive(false);
		if (PlayerPrefs.GetInt("Boss1") == 1)
		{
			this.boss.gameObject.SetActive(true);
			this.truc.gameObject.SetActive(false);
			this.rung.gameObject.SetActive(false);
			this.nui.gameObject.SetActive(false);
		}
		else if (PlayerPrefs.GetInt("Nui1") == 1)
		{
			this.boss.gameObject.SetActive(false);
			this.truc.gameObject.SetActive(false);
			this.rung.gameObject.SetActive(false);
			this.nui.gameObject.SetActive(true);
		}
		else if (PlayerPrefs.GetInt("Rung1") == 1)
		{
			this.boss.gameObject.SetActive(false);
			this.truc.gameObject.SetActive(false);
			this.rung.gameObject.SetActive(true);
			this.nui.gameObject.SetActive(false);
		}
		else
		{
			this.boss.gameObject.SetActive(false);
			this.truc.gameObject.SetActive(true);
			this.rung.gameObject.SetActive(false);
			this.nui.gameObject.SetActive(false);
		}
		if (PlayerPrefs.GetInt("RemoveAds") == 1)
		{
			this.removeAdsIcon.gameObject.SetActive(false);
		}
		else
		{
			this.removeAdsIcon.gameObject.SetActive(true);
		}
	}

    public void AddMoreCoin()
    {
        PlayerPrefs.SetInt("Coin", 60000000);
        PlayerPrefs.Save();
    }

	public Text coinText;

	public Text dmText;

	public Transform playButton;

	public Transform ScrollX3;

	public Transform truc;

	public Transform rung;

	public Transform nui;

	public Transform boss;

	private int coin;

	private int dm;

	public Transform removeAdsIcon;
}
