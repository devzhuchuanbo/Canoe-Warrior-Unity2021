using System;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
	private void Start()
	{
		this.lvname = base.gameObject.name;
		this.s = this.lvname + "Scroll";
		if (!PlayerPrefs.HasKey(this.lvname))
		{
			if (this.lvname == "Truc1")
			{
				PlayerPrefs.SetInt(this.lvname, 1);
				PlayerPrefs.Save();
			}
			else
			{
				PlayerPrefs.SetInt(this.lvname, 0);
				PlayerPrefs.SetInt(this.s, 0);
				PlayerPrefs.Save();
			}
		}
		this.lvValue = PlayerPrefs.GetInt(this.lvname);
		this.lvScroll = PlayerPrefs.GetInt(this.s);
		if (!this.Checked)
		{
			if (this.lvValue == 1)
			{
				this.canPlay = true;
			}
			else
			{
				this.canPlay = false;
			}
			if (this.canPlay)
			{
				this.iconLock.gameObject.SetActive(false);
			}
			else
			{
				this.iconLock.gameObject.SetActive(true);
			}
		}
		else
		{
			this.canPlay = false;
			this.iconLock.gameObject.SetActive(true);
		}
		this.mailS = this.mainScript.GetComponent<mainlv>();
	}

	private void OnEnable()
	{
		this.DeSellect();
	}

	private void CheckFirstLevel()
	{
		this.Checked = true;
		this.canPlay = false;
		this.iconLock.gameObject.SetActive(true);
	}

	public void Sellect()
	{
		if (!this.mailS.gm)
		{
			if (this.canPlay)
			{
				if (this.clickNum == 1)
				{
					this.playButton.gameObject.SetActive(true);
					this.playButton.gameObject.SendMessage("LevelSellected", base.transform);
					this.quangSang.gameObject.SetActive(true);
					this.clickNum = 2;
					this.mainScript.gameObject.SendMessage("ClickLevelSellect", SendMessageOptions.DontRequireReceiver);
				}
				else
				{
					this.mainScript.gameObject.SendMessage("ClickPlayButton", SendMessageOptions.DontRequireReceiver);
					this.playButton.gameObject.SendMessage("PlayLevelSellected");
				}
			}
			else
			{
				this.mainScript.gameObject.SendMessage("ClickLevelSellectFalse", SendMessageOptions.DontRequireReceiver);
				this.playButton.gameObject.SendMessage("LevelNotUnlock", SendMessageOptions.DontRequireReceiver);
				this.playButton.gameObject.SetActive(false);
			}
		}
		else if (this.clickNum == 1)
		{
			this.playButton.gameObject.SetActive(true);
			this.playButton.gameObject.SendMessage("LevelSellected", base.transform);
			this.quangSang.gameObject.SetActive(true);
			this.clickNum = 2;
			this.mainScript.gameObject.SendMessage("ClickLevelSellect", SendMessageOptions.DontRequireReceiver);
		}
		else
		{
			this.mainScript.gameObject.SendMessage("ClickPlayButton", SendMessageOptions.DontRequireReceiver);
			this.playButton.gameObject.SendMessage("PlayLevelSellected");
		}
	}

	public void DeSellect()
	{
		this.quangSang.gameObject.SetActive(false);
		this.clickNum = 1;
	}

	public Transform playButton;

	public Transform quangSang;

	public Transform iconLock;

	public Transform mainScript;

	private string lvname;

	private string s;

	private bool canPlay;

	private int lvValue;

	private int lvScroll;

	private int clickNum = 1;

	private bool Checked;

	private mainlv mailS;
}
