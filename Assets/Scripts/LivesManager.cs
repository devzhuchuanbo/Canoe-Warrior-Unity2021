using System;
using UnityEngine;
using UnityEngine.UI;

public class LivesManager : MonoBehaviour
{
	public bool canLooseLife()
	{
		return this.currentLives > 0 && !this.isUnlimitedLives();
	}

	public void looseOneLife()
	{
		if (this.currentLives == 0)
		{
			return;
		}
		this.currentLives--;
		PlayerPrefs.SetInt("lm_current_lives", this.currentLives);
		PlayerPrefs.Save();
		if (this.currentLives == this.getMaxNumberOfLives() - 1)
		{
			this.setLifeRegenerationTimer();
			base.InvokeRepeating("checkRegenerationTime", 0f, 1f);
		}
	}

	public bool canRefillLives()
	{
		return this.currentLives < this.getMaxNumberOfLives() && !this.isUnlimitedLives();
	}

	public void refillOneLife()
	{
		if (this.currentLives == this.getMaxNumberOfLives())
		{
			return;
		}
		this.currentLives++;
		PlayerPrefs.SetInt("lm_current_lives", this.currentLives);
		PlayerPrefs.Save();
		if (this.currentLives < this.getMaxNumberOfLives())
		{
			this.setLifeRegenerationTimer();
		}
	}

	public void refillAllLives()
	{
		while (this.currentLives < this.getMaxNumberOfLives())
		{
			this.refillOneLife();
		}
	}

	public bool canGetUnlimitedLives()
	{
		return !this.isUnlimitedLives();
	}

	public void refillXLives(int livesToAdd)
	{
		for (int i = 0; i < livesToAdd; i++)
		{
			this.refillOneLife();
		}
	}

	public void getUnlimitedLives()
	{
		if (this.isUnlimitedLives())
		{
			return;
		}
		this.refillAllLives();
		this.setUnlimitedTimer();
		base.InvokeRepeating("checkUnlimitedTime", 0f, 1f);
	}

	public bool canGetExtraLifeSlot()
	{
		return this.extraLives < 2;
	}

	public void getExtraLifeSlot()
	{
		this.extraLives++;
		PlayerPrefs.SetInt("lm_extra_slots", this.extraLives);
		PlayerPrefs.Save();
		this.refillAllLives();
		this.updateUserInterface();
	}

	public bool canPlay()
	{
		return this.isUnlimitedLives() || this.currentLives > 0;
	}

	public double getRefillSecondsLeft()
	{
		return double.Parse(this.regenerationTimestamp) - this.getCurrentTimeInSeconds();
	}

	public double getFullRefillSecondsLeft()
	{
		double num = 0.0;
		double num2 = (double)(this.getMaxNumberOfLives() - this.currentLives);
		if (num2 > 0.0)
		{
			num = this.getRefillSecondsLeft();
		}
		if (num2 > 1.0)
		{
			int num3 = 0;
			while ((double)num3 < num2 - 1.0)
			{
				num += 300.0;
				num3++;
			}
		}
		return num;
	}

	private void Start()
	{
		if (PlayerPrefs.GetInt("lm_first_time") == 0)
		{
			this.firstTimeInit();
		}
		this.currentLives = PlayerPrefs.GetInt("lm_current_lives");
		this.extraLives = PlayerPrefs.GetInt("lm_extra_slots");
		this.regenerationTimestamp = PlayerPrefs.GetString("lm_reset_timestamp");
		this.unlimitedTimestamp = PlayerPrefs.GetString("lm_unlimited_timestamp");
		this.updateUserInterface();
		if (this.currentLives < this.getMaxNumberOfLives())
		{
			base.InvokeRepeating("checkRegenerationTime", 0f, 1f);
		}
		if (this.isUnlimitedLives())
		{
			base.InvokeRepeating("checkUnlimitedTime", 0f, 1f);
		}
	}

	private void firstTimeInit()
	{
		UnityEngine.Debug.Log("First Time Init");
		PlayerPrefs.SetInt("lm_current_lives", 5);
		PlayerPrefs.SetString("lm_reset_timestamp", "0");
		PlayerPrefs.SetString("lm_unlimited_timestamp", "0");
		PlayerPrefs.SetString("lm_extra_slots", "0");
		PlayerPrefs.SetInt("lm_first_time", 1);
		PlayerPrefs.Save();
	}

	public void reset()
	{
		PlayerPrefs.DeleteAll();
		PlayerPrefs.Save();
		this.Start();
	}

	private void updateUserInterface()
	{
		GameObject.Find("TEXT_CURRENT_LIVES").GetComponent<Text>().text = this.getCurrentLivesMsg();
		GameObject.Find("TEXT_TIMER").GetComponent<Text>().text = this.getTimeLeftMsg();
	}

	private string getCurrentLivesMsg()
	{
		if (this.isUnlimitedLives())
		{
			return "∞";
		}
		return this.currentLives.ToString();
	}

	private string getTimeLeftMsg()
	{
		if (this.isUnlimitedLives())
		{
			return this.secondsToTimeFormatter(this.getUnlimitedSecondsLeft());
		}
		if (this.currentLives == this.getMaxNumberOfLives())
		{
			return "Full";
		}
		return this.secondsToTimeFormatter(this.getRefillSecondsLeft());
	}

	private string secondsToTimeFormatter(double seconds)
	{
		if (seconds < 3600.0)
		{
			return string.Format("{0:0}:{1:00}", Mathf.Floor((float)seconds / 60f), Mathf.RoundToInt((float)seconds % 60f));
		}
		return Mathf.CeilToInt((float)seconds / 3600f).ToString() + " hrs";
	}

	private void checkUnlimitedTime()
	{
		if (this.getUnlimitedSecondsLeft() <= 0.0)
		{
			this.unlimitedTimestamp = "0";
			PlayerPrefs.SetString("lm_unlimited_timestamp", "0");
			PlayerPrefs.Save();
			base.CancelInvoke("checkUnlimitedTime");
		}
		this.updateUserInterface();
	}

	private void checkRegenerationTime()
	{
		double refillSecondsLeft = this.getRefillSecondsLeft();
		if (refillSecondsLeft <= 0.0)
		{
			int num = 1;
			num += (int)Mathf.Abs((float)refillSecondsLeft) / 300;
			this.refillXLives(num);
			int num2 = (int)Mathf.Abs((float)refillSecondsLeft) % 300;
			if (num2 > 0)
			{
				this.regenerationTimestamp = (this.getCurrentTimeInSeconds() + 300.0 - (double)num2).ToString();
				PlayerPrefs.SetString("lm_reset_timestamp", this.regenerationTimestamp);
				PlayerPrefs.Save();
			}
		}
		if (this.currentLives == this.getMaxNumberOfLives())
		{
			base.CancelInvoke("checkRegenerationTime");
			this.regenerationTimestamp = "0";
			PlayerPrefs.SetString("lm_reset_timestamp", "0");
			PlayerPrefs.Save();
		}
		this.updateUserInterface();
	}

	private void setLifeRegenerationTimer()
	{
		this.regenerationTimestamp = (this.getCurrentTimeInSeconds() + 300.0).ToString();
		PlayerPrefs.SetString("lm_reset_timestamp", this.regenerationTimestamp);
		PlayerPrefs.Save();
	}

	private void setUnlimitedTimer()
	{
		this.unlimitedTimestamp = (this.getCurrentTimeInSeconds() + 15.0).ToString();
		PlayerPrefs.SetString("lm_unlimited_timestamp", this.unlimitedTimestamp);
		PlayerPrefs.Save();
	}

	private double getUnlimitedSecondsLeft()
	{
		return double.Parse(this.unlimitedTimestamp) - this.getCurrentTimeInSeconds();
	}

	private double getCurrentTimeInSeconds()
	{
		DateTime d = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		return (DateTime.UtcNow - d).TotalSeconds;
	}

	private int getMaxNumberOfLives()
	{
		return 5 + this.extraLives;
	}

	private bool isUnlimitedLives()
	{
		return this.getUnlimitedSecondsLeft() > 0.0;
	}

	private const string ID_CURRENT_LIVES = "lm_current_lives";

	private const string ID_REGENERATION_TIMESTAMP = "lm_reset_timestamp";

	private const string ID_UNLIMITED_TIMESTAMP = "lm_unlimited_timestamp";

	private const string ID_EXTRA_LIVE_SLOTS = "lm_extra_slots";

	private const string ID_FIRST_TIME = "lm_first_time";

	private int currentLives;

	private int extraLives;

	private string regenerationTimestamp;

	private string unlimitedTimestamp;
}
