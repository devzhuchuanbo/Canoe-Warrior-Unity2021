using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopCanvas : MonoBehaviour
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
		this.choiseBtn.gameObject.SetActive(false);
	}

	private void Start()
	{
		iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
		{
			"from",
			this.beginvalue,
			"to",
			0,
			"time",
			1,
			"easetype",
			iTween.EaseType.easeOutQuad,
			"onUpdate",
			"SlideLevel"
		}));
	}

	public void RefreshAndSave()
	{
		this.coinText.text = this.coin.ToString();
		this.dmText.text = this.dm.ToString();
		PlayerPrefs.SetInt("Coin", this.coin);
		PlayerPrefs.SetInt("DM", this.dm);
		PlayerPrefs.Save();
	}

	private void SlideLevel(float i)
	{
		this.content.SetLocalPositionX(i);
	}

	public Text coinText;

	public Text dmText;

	public RectTransform content;

	public Transform choiseBtn;

	[HideInInspector]
	public int coin;

	[HideInInspector]
	public int dm;

	public int beginvalue;
}
