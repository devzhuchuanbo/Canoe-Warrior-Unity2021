using System;
using UnityEngine;
using UnityEngine.UI;

public class MainEventsLog : MonoBehaviour
{
	private void Awake()
	{
		this.GetInfoUpgrage();
	}

	private void Start()
	{
		this.pausing = false;
		this.gameOver = false;
		this.gameFinish = false;
		this.AdShow = false;
		this.isDashFill = false;
		this.isFiFill = false;
		this.isTransFill = false;
		this.isTrans2Fill = false;
		this.rand1 = UnityEngine.Random.Range(0, this.AudioClip_CheckpointVoice.Length);
		this.rand2 = UnityEngine.Random.Range(0, this.AudioClip_CheckpointVoice.Length);
		this.ra1 = UnityEngine.Random.Range(0, this.AudioClip_Jump.Length);
		this.ra2 = UnityEngine.Random.Range(0, this.AudioClip_Jump.Length);
		this.timeToTalk = Time.time;
		this.timeToEnemyXien = Time.time;
		this.timeToEnemyChem = Time.time;
		this.BlackSceneAnim = this.BlackScene.GetComponent<Animator>();
		this.scrollKey = this.currentLevel + "Scroll";
		if (!PlayerPrefs.HasKey(this.scrollKey))
		{
			PlayerPrefs.SetInt(this.scrollKey, 0);
			PlayerPrefs.Save();
		}
		this.oldScroll = PlayerPrefs.GetInt(this.scrollKey);
		this.newScroll = 0;
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
		if (!PlayerPrefs.HasKey("Chet"))
		{
			PlayerPrefs.SetInt("Chet", 0);
			PlayerPrefs.Save();
		}
		this.coin = PlayerPrefs.GetInt("Coin");
		this.dM = PlayerPrefs.GetInt("DM");
		this.chet = PlayerPrefs.GetInt("Chet");
		this.CoinText.text = this.coin.ToString();
		this.DMText.text = this.dM.ToString();
		this.ScrollText.text = "x 0";
		this.Life_bar.text = "x  " + this.Life.ToString();
		this.PauseCanvas.SetActive(false);
		this.SettingCanvas.SetActive(false);
		this.NoMoneyCanvas.SetActive(false);
		this.NoVideosCanvas.SetActive(false);
		this.HoiSinhCanvas.SetActive(false);
		this.GameOverCamvas.SetActive(false);
		this.FinishCavas.SetActive(false);
		this.Trans2CoolDown.gameObject.SetActive(false);
		if (!PlayerPrefs.HasKey("AudioValue"))
		{
			PlayerPrefs.SetFloat("AudioValue", 1f);
			PlayerPrefs.Save();
		}
		AudioListener.volume = PlayerPrefs.GetFloat("AudioValue");
		this.AudioSlider.value = AudioListener.volume;
		int num = PlayerPrefs.GetInt("Theme");
		if (num >= 4)
		{
			num = 0;
		}
		this.AudioSource_themeMusic.clip = this.themeMusicClip[num];
		num++;
		if (num >= 4)
		{
			num = 0;
		}
		PlayerPrefs.SetInt("Theme", num);
		if (!PlayerPrefs.HasKey("MusicOn"))
		{
			PlayerPrefs.SetInt("MusicOn", 1);
			PlayerPrefs.Save();
		}
		if (PlayerPrefs.GetInt("MusicOn") == 1)
		{
			this.musicOn = true;
			this.MusicText.text = "On";
			this.AudioSource_themeMusic.Play();
		}
		else
		{
			this.musicOn = false;
			this.MusicText.text = "Off";
			this.AudioSource_themeMusic.Stop();
		}
		if (this.canCheck)
		{
			this.checkButton.SetActive(true);
		}
		else
		{
			this.checkButton.SetActive(false);
		}
		this.CheckEnough();
		this.fiGSlideBorder.gameObject.SetActive(false);
		this.fiGSlide1.gameObject.SetActive(false);
		this.fiGSlide2.gameObject.SetActive(false);
		//this.adCT = GameObject.Find("AdController").GetComponent<AdCreator>();
		if (this.adCT)
		{
		}
		Resources.UnloadUnusedAssets();
		LogLevelStartedEvent();

	}

	private void Update()
	{
		if (this.fiGing)
		{
			this.FiGAdd += Time.deltaTime;
			this.fiGSlide1.fillAmount = this.FiGAdd / this.fiGTime;
			this.fiGSlide2.fillAmount = this.FiGAdd / this.fiGTime;
		}
		if (this.isDashFill)
		{
			this.colD = (this.timeDashFill - Time.time) / this.dtime;
			if (this.colD > 0f)
			{
				this.UpdateValue1(this.colD);
			}
			else
			{
				this.UpdateValue1(0f);
				this.isDashFill = false;
			}
		}
		if (this.isFiFill)
		{
			this.colF = (this.timeFiFill - Time.time) / this.ftime;
			if (this.colF > 0f)
			{
				this.UpdateValue2(this.colF);
			}
			else
			{
				this.UpdateValue2(0f);
				this.isFiFill = false;
			}
		}
		if (this.isTransFill)
		{
			this.colT = (this.timeTransFill - Time.time) / this.ttime;
			if (this.colT > 0f)
			{
				this.UpdateValue3(this.colT);
			}
			else
			{
				this.UpdateValue3(0f);
				this.isTransFill = false;
			}
		}
		if (this.isTrans2Fill)
		{
			this.colT2 = (this.timeTrans2Fill - Time.time) / this.t2time;
			if (this.colT2 > 0f)
			{
				this.UpdateValue4(this.colT2);
			}
			else
			{
				this.UpdateValue4(0f);
				this.isTrans2Fill = false;
			}
		}
	}

	public void Finish(int i)
	{
		this.kieucong = i;
		this.SaveResuil();
		if (this.kieucong != 2)
		{
			base.Invoke("FinishBusiness", 3f);
		}
		else
		{
			base.Invoke("FinishBusiness", 6f);
		}
	}

	public void SaveResuil()
	{
		PlayerPrefs.SetInt(this.nextLevel, 1);
		if (this.newScroll > this.oldScroll)
		{
			PlayerPrefs.SetInt(this.scrollKey, this.newScroll);
			PlayerPrefs.Save();
		}
		PlayerPrefs.SetInt("Coin", this.coin);
		PlayerPrefs.SetInt("DM", this.dM);
		PlayerPrefs.Save();
	}

	private void FinishBusiness()
	{
		this.BlackScene.SetActive(true);
		this.BlackSceneAnim.SetTrigger("Hit");
		base.Invoke("FinishBoard", 1f);
	}

	private void FinishBoard()
	{
		LogLevelCompleteEvent();

		if (this.kieucong == 0)
		{
			this.FinishCavas.SetActive(true);
			Animator component = this.FinishCavas.GetComponent<Animator>();
			component.SetTrigger("Hit");
			Invoke("BackToMain", 1.5f);

		}
		else
		{
			this.FinishCavas.SetActive(true);
			Animator component2 = this.FinishCavas.GetComponent<Animator>();
			component2.SetTrigger("Hit1");
			Invoke("BackToMain", 1.5f);

		}
	}

	public void PlayerDied(string s)
	{
		LogLevelFailedEvent();

		this.Life--;
		if (this.Life >= 0)
		{
			this.Life_bar.text = "x  " + this.Life.ToString();
		}
		else
		{
			this.Life = 0;
		}
		this.dieing = true;
		this.AudioSource_DashOk.Stop();
		this.AudioSource_Checkpoint.Stop();
		this.AudioSource_Jump_ManSound.Stop();
		this.AudioSource_baygau2.Stop();
		if (s == "KillZone")
		{
			this.AudioSource_Death.clip = this.AudioClip_DeathKillZone[UnityEngine.Random.Range(0, this.AudioClip_DeathKillZone.Length)];
			this.AudioSource_Death.Play();
		}
		else if (s == "Doc")
		{
			this.AudioSource_Death.clip = this.AudioClip_DeathDoc[UnityEngine.Random.Range(0, this.AudioClip_DeathDoc.Length)];
			this.AudioSource_Death.Play();
		}
		else if (s == "Chay")
		{
			this.AudioSource_Death.clip = this.AudioClip_DeathChay[UnityEngine.Random.Range(0, this.AudioClip_DeathChay.Length)];
			this.AudioSource_Death.Play();
		}
		else
		{
			this.AudioSource_Death.clip = this.AudioClip_Death[UnityEngine.Random.Range(0, this.AudioClip_Death.Length)];
			this.AudioSource_Death.Play();
		}
		this.chet++;
		PlayerPrefs.SetInt("Coin", this.coin);
		PlayerPrefs.SetInt("DM", this.dM);
		PlayerPrefs.SetInt("Chet", this.chet);
		PlayerPrefs.Save();
		this.isDashFill = false;
		this.isFiFill = false;
		this.isTransFill = false;
		this.isTrans2Fill = false;
		this.DashCoolDown.fillAmount = 0f;
		this.FiCoolDown.fillAmount = 0f;
		this.TransCoolDown.fillAmount = 0f;
		this.Trans2CoolDown.fillAmount = 0f;
		this.PlaySkillsCanvas.SetActive(true);
		this.Trans2CoolDown.gameObject.SetActive(false);
		this.FiGRelease();
	}

	public void playerJump()
	{
		if (Time.time > this.timeToTalk)
		{
			int num = UnityEngine.Random.Range(0, 100);
			if (num < 60)
			{
				this.ra = UnityEngine.Random.Range(0, this.AudioClip_Jump.Length);
				while (this.ra == this.ra1 || this.ra == this.ra2)
				{
					this.ra = UnityEngine.Random.Range(0, this.AudioClip_Jump.Length);
				}
				this.ra2 = this.ra1;
				this.ra1 = this.ra;
				this.AudioSource_Jump_ManSound.clip = this.AudioClip_Jump[this.ra];
				this.AudioSource_Jump_ManSound.Play();
				this.timeToTalk = Time.time + 2.5f;
			}
		}
	}

	public void playerDinhBayGau()
	{
		this.AudioSource_baygau2.Play();
		this.AudioSource_DashOk.clip = this.AudioClip_DinhBayGau[UnityEngine.Random.Range(0, this.AudioClip_DinhBayGau.Length)];
		this.AudioSource_DashOk.Play();
	}

	public void playerDinhBayLong()
	{
		this.AudioSource_baygau2.Play();
		this.AudioSource_DashOk.clip = this.AudioClip_DinhBayLong[UnityEngine.Random.Range(0, this.AudioClip_DinhBayLong.Length)];
		this.AudioSource_DashOk.Play();
	}

	public void EnemyXienKK()
	{
		if (Time.time > this.timeToEnemyXien)
		{
			int num = UnityEngine.Random.Range(0, 100);
			if (num < 90)
			{
				this.AudioSource_EnemyXien.clip = this.AudioClip_EnemyXienkk[UnityEngine.Random.Range(0, this.AudioClip_EnemyXienkk.Length)];
				this.AudioSource_EnemyXien.Play();
				this.timeToEnemyXien = Time.time + 3f;
			}
		}
	}

	public void EnemyXienKG()
	{
		if (Time.time > this.timeToEnemyXien)
		{
			int num = UnityEngine.Random.Range(0, 100);
			if (num < 90)
			{
				this.AudioSource_EnemyXien.clip = this.AudioClip_EnemyXienkg[UnityEngine.Random.Range(0, this.AudioClip_EnemyXienkg.Length)];
				this.AudioSource_EnemyXien.Play();
				this.timeToEnemyXien = Time.time + 3f;
			}
		}
	}

	public void EnemyChem()
	{
		this.AudioSource_EnemyChem.clip = this.AudioClip_EnemyChem[UnityEngine.Random.Range(0, this.AudioClip_EnemyChem.Length)];
		this.AudioSource_EnemyChem.Play();
	}

	public void EnemyChemK()
	{
		this.AudioSource_EnemyChem.clip = this.AudioClip_EnemyChemK[UnityEngine.Random.Range(0, this.AudioClip_EnemyChemK.Length)];
		this.AudioSource_EnemyChem.Play();
	}

	public void EnemyDeath()
	{
		this.AudioSource_EnemyDeath.clip = this.AudioClip_EnemyDeath[UnityEngine.Random.Range(0, this.AudioClip_EnemyDeath.Length)];
		this.AudioSource_EnemyDeath.Play();
	}

	public void SetGiat(Vector3 p)
	{
		this.AudioSource_SetGiat.gameObject.transform.position = p;
		this.AudioSource_SetGiat.clip = this.AudioClip_Set[UnityEngine.Random.Range(0, this.AudioClip_Set.Length)];
		this.AudioSource_SetGiat.Play();
	}

	public void playerDashOk(int enemynumber)
	{
		if (!this.dieing)
		{
			if (enemynumber == 1)
			{
				int num = UnityEngine.Random.Range(0, 100);
				if (num < 40)
				{
					this.ran = UnityEngine.Random.Range(0, this.AudioClip_DashOk.Length);
					while (this.ran == this.ran1 || this.ran == this.ran2)
					{
						this.ran = UnityEngine.Random.Range(0, this.AudioClip_DashOk.Length);
					}
					this.ran2 = this.ran1;
					this.ran1 = this.ran;
					this.AudioSource_DashOk.clip = this.AudioClip_DashOk[this.ran];
					this.AudioSource_DashOk.Play();
					this.timeToTalk = Time.time + 4f;
				}
			}
			else
			{
				this.ran = UnityEngine.Random.Range(0, this.AudioClip_DashOk.Length);
				while (this.ran == this.ran1 || this.ran == this.ran2)
				{
					this.ran = UnityEngine.Random.Range(0, this.AudioClip_DashOk.Length);
				}
				this.ran2 = this.ran1;
				this.ran1 = this.ran;
				this.AudioSource_DashOk.clip = this.AudioClip_DashOk[this.ran];
				this.AudioSource_DashOk.Play();
				this.timeToTalk = Time.time + 4f;
			}
		}
	}

	public void Step()
	{
		this.AudioSource_FootStep.clip = this.AudioClip_FootStep[UnityEngine.Random.Range(0, this.AudioClip_FootStep.Length)];
		this.AudioSource_FootStep.Play();
	}

	public void AnMau()
	{
		this.Life++;
		this.Life_bar.text = "x  " + this.Life.ToString();
	}

	public void AnTien(int t, Transform p)
	{
		int num;
		if (t == 0)
		{
			num = UnityEngine.Random.Range(this.minCoinNho, this.maxCoinNho);
		}
		else
		{
			num = UnityEngine.Random.Range(this.minCoinTo, this.maxCoinTo);
		}
		this.coin += num;
		this.CoinText.text = this.coin.ToString();
		this.HUDOut.ShowCoinHudOut(num, p);
		this.CheckEnough();
	}

	public void AnDM(Transform p)
	{
		int num = UnityEngine.Random.Range(this.minDM, this.maxDM);
		this.dM += num;
		this.DMText.text = this.dM.ToString();
		this.HUDOut.ShowDMHudOut(num, p);
		this.CheckEnough();
	}

	public void AnScroll()
	{
		this.newScroll++;
		this.ScrollText.text = "x " + this.newScroll.ToString();
	}

	private void disableMove()
	{
		this.NinjaMovScript.Btn_Left_bool = false;
		this.NinjaMovScript.Btn_Right_bool = false;
	}

	public void TruPhiCheck()
	{
		this.coin -= this.checkCoin;
		this.dM -= this.checkDm;
		this.CoinText.text = this.coin.ToString();
		this.DMText.text = this.dM.ToString();
		this.HUDOut.ShowCoinHudOut(-this.checkCoin, this.NinjaMovScript.transform);
		this.HUDOut.ShowDMHudOut(-this.checkDm, this.NinjaMovScript.transform);
		this.CheckEnough();
	}

	private void CheckEnough()
	{
		if (this.coin >= this.checkCoin && this.dM >= this.checkDm)
		{
			this.CheckCoolDown.fillAmount = 0f;
			this.enoughToCheck = true;
		}
		else
		{
			this.CheckCoolDown.fillAmount = 1f;
			this.enoughToCheck = false;
		}
	}

	public void Pause()
	{
		this.AudioSource_Click.Play();
		Time.timeScale = 0f;
		this.RemoveControl();
		this.PauseCanvas.SetActive(true);
		this.SettingCanvas.SetActive(false);
	}

	public void Resume()
	{
		Time.timeScale = 1f;
		this.GetControl();
		this.AudioSource_Click.Play();
		this.PauseCanvas.SetActive(false);
		this.SettingCanvas.SetActive(false);
		PlayerPrefs.SetFloat("AudioValue", this.AudioSlider.value);
		PlayerPrefs.Save();
	}

	public void Restart()
	{
		LogLevelRestartEvent();
		Time.timeScale = 1f;
		this.AudioSource_Click.Play();
		this.LoadingPanel.SetActive(true);
		if (this.adCT && !this.xemadchua)
		{
		}
		UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
	}

	public void RestartNoAd2()
	{
		LogLevelRestartEvent();
		Time.timeScale = 1f;
		this.AudioSource_Click.Play();
		this.LoadingPanel.SetActive(true);
		UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
	}

	public void Setting()
	{
		this.AudioSource_Click.Play();
		this.SettingCanvas.SetActive(true);
		this.PauseCanvas.SetActive(false);
	}

	public void BackToMain()
	{
		Time.timeScale = 1f;
		this.AudioSource_Click.Play();
		this.LoadingPanel.SetActive(true);
		if (this.adCT && !this.xemadchua)
		{
		}
		UnityEngine.SceneManagement.SceneManager.LoadScene("Select");
	}

	public void BackToMainNoAds()
	{
		Time.timeScale = 1f;
		this.AudioSource_Click.Play();
		this.LoadingPanel.SetActive(true);
		if (this.adCT && !this.xemadchua)
		{
		}
		UnityEngine.SceneManagement.SceneManager.LoadScene("Select");
	}

	public void BackToMainNoAds2()
	{
		Time.timeScale = 1f;
		this.AudioSource_Click.Play();
		this.LoadingPanel.SetActive(true);
		UnityEngine.SceneManagement.SceneManager.LoadScene("Select");
	}

	public void NextLevel()
	{
		Time.timeScale = 1f;
		this.AudioSource_Click.Play();
		this.LoadingPanel.SetActive(true);
		UnityEngine.SceneManagement.SceneManager.LoadScene(this.nextLevel);
	}

	public void AudioChange()
	{
		AudioListener.volume = this.AudioSlider.value;
	}

	public void MusicOnOff()
	{
		if (!this.musicOn)
		{
			this.musicOn = true;
			this.MusicText.text = "On";
			this.AudioSource_Click.Play();
			this.AudioSource_themeMusic.Play();
			PlayerPrefs.SetInt("MusicOn", 1);
			PlayerPrefs.Save();
		}
		else
		{
			this.musicOn = false;
			this.MusicText.text = "Off";
			this.AudioSource_themeMusic.Stop();
			PlayerPrefs.SetInt("MusicOn", 0);
			PlayerPrefs.Save();
		}
	}

	public void RemoveControl()
	{
		this.PlayCanvas.SetActive(false);
	}

	public void GetControl()
	{
		this.PlayCanvas.SetActive(true);
	}

	public void FadeIn()
	{
		this.RemoveControl();
		this.BlackScene.SetActive(true);
		this.cameraScript.FollowSpeed -= 2f;
		if (!this.BlackSceneAnim)
		{
			this.BlackSceneAnim = this.BlackScene.GetComponent<Animator>();
		}
		this.BlackSceneAnim.SetTrigger("FadeIn");
	}

	public void FadeInDone()
	{
		this.BlackScene.SetActive(false);
		this.NinjaMovScript.Btn_Left_bool = false;
		this.NinjaMovScript.Btn_Right_bool = false;
		this.GetControl();
		this.cameraScript.FollowSpeed += 2f;
		this.NinjaMovScript.ActiveCheckpoint = this.NinjaMovScript.gameObject.transform.position;
	}

	public void FadeInDonelv1(int i)
	{
		if (i == 0)
		{
			this.cameraScript.FollowSpeed += 2f;
		}
		else
		{
			this.BlackScene.SetActive(false);
			this.NinjaMovScript.Btn_Left_bool = false;
			this.NinjaMovScript.Btn_Right_bool = false;
			this.GetControl();
			this.NinjaMovScript.ActiveCheckpoint = this.NinjaMovScript.gameObject.transform.position;
		}
	}

	public void DashCoolDownFill(float t)
	{
		this.isDashFill = true;
		this.dtime = t;
		this.timeDashFill = Time.time + t;
	}

	private void UpdateValue1(float cool)
	{
		this.DashCoolDown.fillAmount = cool;
	}

	public void FiCoolDownFill(float t)
	{
		this.isFiFill = true;
		this.ftime = t;
		this.timeFiFill = Time.time + t;
	}

	private void UpdateValue2(float cool)
	{
		this.FiCoolDown.fillAmount = cool;
	}

	public void TransCoolDownFill(float t)
	{
		this.isTransFill = true;
		this.ttime = t;
		this.timeTransFill = Time.time + t;
	}

	private void UpdateValue3(float cool)
	{
		this.TransCoolDown.fillAmount = cool;
	}

	public void Trans2CoolDownFill(float t)
	{
		this.isTrans2Fill = true;
		this.t2time = t;
		this.timeTrans2Fill = Time.time + t;
	}

	private void UpdateValue4(float cool)
	{
		this.Trans2CoolDown.fillAmount = cool;
	}

	public void HetMang()
	{
		if (this.hoisinhchua)
		{
			base.Invoke("hoisinhroi", 1f);
			this.AudioSource_Event.clip = this.AudioClip_gameOver;
			this.AudioSource_Event.Play();
		}
		else
		{
			base.Invoke("chuahoisinh", 0.7f);
			this.AudioSource_Event.clip = this.AudioClip_3mang;
			this.AudioSource_Event.Play();
		}
	}

	private void hoisinhroi()
	{
		this.GameOverCamvas.SetActive(true);
		this.HoiSinhCanvas.SetActive(false);
	}

	private void chuahoisinh()
	{
		this.GameOverCamvas.SetActive(false);
		this.HoiSinhCanvas.SetActive(true);
		this.mangchet.text = "You have died " + this.chet.ToString() + " times";
	}

	public void live3000()
	{
		this.AudioSource_Click.Play();
		if (this.coin >= 3000)
		{
			this.coin -= 3000;
			PlayerPrefs.SetInt("Coin", this.coin);
			PlayerPrefs.Save();
			this.CoinText.text = this.coin.ToString();
			this.hoisinhchua = true;
			this.hoisinh();
		}
		else
		{
			this.NoMoneyCanvas.SetActive(true);
		}
	}

	public void liveVideo()
	{
		this.AudioSource_Click.Play();
		if (this.adCT)
		{
			//if (AdCreator.GiftAdReady)
			//{
			//	this.adCT.ShowFullAD(1);
			//	this.xemadchua = true;
		//	}
		///	else
		//	{
			//	this.NoVideosCanvas.SetActive(true);
		//	}
		}
	}

	public void XemXongVideo()
	{
		this.hoisinhchua = true;
		this.hoisinh();
	}

	public void BackToRebornBoard()
	{
		this.AudioSource_Click.Play();
		this.NoMoneyCanvas.SetActive(false);
		this.NoVideosCanvas.SetActive(false);
	}

	public void nothanks()
	{
		this.AudioSource_Click.Play();
		this.GameOverCamvas.SetActive(true);
		this.HoiSinhCanvas.SetActive(false);
	}

	private void hoisinh()
	{
		this.GameOverCamvas.SetActive(false);
		this.HoiSinhCanvas.SetActive(false);
		this.Life = 2;
        this.Life_bar.text = "x  " + this.Life.ToString();
		this.NinjaMovScript.iReborn();
	}

	public void ComeBackCP()
	{
		PlayerPrefs.SetInt("Coin", this.coin);
		PlayerPrefs.SetInt("DM", this.dM);
		PlayerPrefs.Save();
		this.dieing = false;
		int num = UnityEngine.Random.Range(0, 100);
		if (num < 30)
		{
			this.rand = UnityEngine.Random.Range(0, this.AudioClip_CheckpointVoice.Length);
			while (this.rand == this.rand1 || this.rand == this.rand2)
			{
				this.rand = UnityEngine.Random.Range(0, this.AudioClip_CheckpointVoice.Length);
			}
			this.rand2 = this.rand1;
			this.rand1 = this.rand;
			this.AudioSource_Death.Stop();
			this.AudioSource_Checkpoint.clip = this.AudioClip_CheckpointVoice[this.rand];
			this.AudioSource_Checkpoint.Play();
			this.timeToTalk = Time.time + 4f;
		}
	}

	public void EnemiesGoBack1()
	{
		this.AudioSource_EnemiesGback.clip = this.AudioClip_EnemiesGback_clip[UnityEngine.Random.Range(0, this.AudioClip_EnemiesGback_clip.Length)];
		this.AudioSource_EnemiesGback.Play();
	}

	public void TingTing()
	{
		this.AudioSource_Ting.clip = this.tingting;
		this.AudioSource_Ting.Play();
	}

	public void BienHinh(bool xuoi)
	{
		if (xuoi)
		{
			this.PlaySkillsCanvas.gameObject.SetActive(false);
			this.Trans2CoolDown.gameObject.SetActive(true);
		}
		else
		{
			this.PlaySkillsCanvas.gameObject.SetActive(true);
			this.Trans2CoolDown.gameObject.SetActive(false);
		}
	}
	//FB
	public void LogLevelStartedEvent()
	{

	}
	public void LogLevelRestartEvent()
	{


	}
	public void LogLevelFailedEvent()
	{


	}
	public void LogLevelCompleteEvent()
	{


	}
	//FB

	public void CritHUD(Vector3 p)
	{
		Vector3 p2 = p + new Vector3(0f, 2f, 0f);
		this.HUDCrit.ShowHudPos(p2);
		this.NinjaMovScript.CritEnemy();
	}

	private void GetInfoUpgrage()
	{
		float[] array = new float[]
		{
			16f,
			15f,
			14f,
			13f,
			12f,
			11f,
			10f,
			9f
		};
		int[] array2 = new int[]
		{
			6,
			9,
			12,
			16,
			20,
			25,
			30,
			35
		};
		int[] array3 = new int[]
		{
			3,
			4,
			4,
			5,
			5,
			6,
			6,
			7
		};
		float[] array4 = new float[]
		{
			8f,
			8f,
			10f,
			10f,
			12f,
			12f,
			15f,
			15f
		};
		float[] array5 = new float[]
		{
			12f,
			11f,
			11f,
			10f,
			10f,
			9f,
			9f,
			7f
		};
		float[] array6 = new float[]
		{
			4f,
			4f,
			5f,
			5f,
			6f,
			6f,
			7f,
			7f
		};
		if (!PlayerPrefs.HasKey("DashLevel"))
		{
			PlayerPrefs.SetInt("DashLevel", 0);
			PlayerPrefs.SetInt("FiLevel", 0);
			PlayerPrefs.SetInt("HPLevel", 0);
			PlayerPrefs.SetInt("TransLevel", 0);
			PlayerPrefs.Save();
		}
		int @int = PlayerPrefs.GetInt("DashLevel");
		int int2 = PlayerPrefs.GetInt("FiLevel");
		int int3 = PlayerPrefs.GetInt("HPLevel");
		int int4 = PlayerPrefs.GetInt("TransLevel");
		if (@int == 7)
		{
			this.NinjaMovScript.MaxSkillDash = true;
		}
		else
		{
			this.NinjaMovScript.MaxSkillDash = false;
		}
		this.dashCoolDown = array[@int];
		this.crit = array2[int2];
		this.Life = array3[int3];
		this.timeBV = array4[int3];
		this.transCoolDown = array5[int4];
		this.transTime = array6[int4];
		if (this.lvBoss)
		{
			this.Life = 3;
		}
	}

	public void FiGCoolDownFill(float t)
	{
		iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
		{
			"from",
			1,
			"to",
			0,
			"time",
			t,
			"onUpdate",
			"UpdateValue3"
		}));
	}

	public void FiGPress(float t)
	{
		if (this.enoughToCheck)
		{
			this.fiGSlideBorder.gameObject.SetActive(true);
			this.fiGSlide1.gameObject.SetActive(true);
			this.fiGSlide2.gameObject.SetActive(true);
			this.fiGTime = t;
			this.FiGAdd = 0f;
			this.fiGing = true;
		}
	}

	public void FiGRelease()
	{
		this.fiGSlideBorder.gameObject.SetActive(false);
		this.fiGSlide1.gameObject.SetActive(false);
		this.fiGSlide2.gameObject.SetActive(false);
		this.FiGAdd = 0f;
		this.fiGing = false;
	}

	[NonSerialized]
	public bool gameOver;

	[NonSerialized]
	public bool gameFinish;

	[NonSerialized]
	public bool pausing;

	[NonSerialized]
	public bool AdShow;

	[NonSerialized]
	public int BannerId;

	public string currentLevel;

	public string nextLevel;

	private int oldScroll;

	private int newScroll;

	private int coin;

	private int dM;

	private string scrollKey;

	public NinjaMovementScript NinjaMovScript;

	[HideInInspector]
	public int Life;

	public bool canCheck;

	public bool enoughToCheck;

	public GameObject PlayCanvas;

	public GameObject PlaySkillsCanvas;

	public GameObject PauseCanvas;

	public GameObject SettingCanvas;

	public GameObject HoiSinhCanvas;

	public GameObject NoMoneyCanvas;

	public GameObject NoVideosCanvas;

	public GameObject GameOverCamvas;

	public GameObject FinishCavas;

	public GameObject BlackScene;

	public GameObject LoadingPanel;

	public GameObject checkButton;

	public Text mangchet;

	public Slider AudioSlider;

	public Text MusicText;

	public Image Trans2CoolDown;

	public Image DashCoolDown;

	public Image FiCoolDown;

	public Image TransCoolDown;

	public Image CheckCoolDown;

	public Image fiGSlide1;

	public Image fiGSlide2;

	public Image fiGSlideBorder;

	public CameraFollowTarget cameraScript;

	public HUDText HUD;

	public HUDText HUDOut;

	public HUDText HUDCrit;

	public Text Life_bar;

	public Text CoinText;

	public Text DMText;

	public Text ScrollText;

	public int minCoinTo;

	public int maxCoinTo;

	public int minCoinNho;

	public int maxCoinNho;

	public int minDM;

	public int maxDM;

	public AudioSource AudioSource_themeMusic;

	public AudioSource AudioSource_Death;

	public AudioSource AudioSource_Jump_ManSound;

	public AudioSource AudioSource_Event;

	public AudioSource AudioSource_FootStep;

	public AudioSource AudioSource_EnemiesGback;

	public AudioSource AudioSource_Checkpoint;

	public AudioSource AudioSource_DashOk;

	public AudioSource AudioSource_Ting;

	public AudioSource AudioSource_SetGiat;

	public AudioSource AudioSource_EnemyXien;

	public AudioSource AudioSource_EnemyChem;

	public AudioSource AudioSource_EnemyDeath;

	public AudioSource AudioSource_baygau2;

	public AudioSource AudioSource_Click;

	public AudioClip[] themeMusicClip;

	public AudioClip[] AudioClip_Death;

	public AudioClip[] AudioClip_DeathDoc;

	public AudioClip[] AudioClip_DeathChay;

	public AudioClip[] AudioClip_DeathKillZone;

	public AudioClip[] AudioClip_Jump;

	public AudioClip[] AudioClip_DashOk;

	public AudioClip[] AudioClip_FootStep;

	public AudioClip[] AudioClip_EnemiesGback_clip;

	public AudioClip[] AudioClip_CheckpointVoice;

	public AudioClip[] AudioClip_EnemyXienkk;

	public AudioClip[] AudioClip_EnemyXienkg;

	public AudioClip[] AudioClip_EnemyChem;

	public AudioClip[] AudioClip_EnemyChemK;

	public AudioClip[] AudioClip_EnemyDeath;

	public AudioClip[] AudioClip_DinhBayGau;

	public AudioClip[] AudioClip_DinhBayLong;

	public AudioClip[] AudioClip_Set;

	public AudioClip AudioClip_3mang;

	public AudioClip AudioClip_gameOver;

	public AudioClip tingting;

	private bool musicOn;

	private int dataStar;

	private int dataNextStar;

	private int IntersShowInt;

	private bool isDashFill;

	private bool isFiFill;

	private bool isTransFill;

	private bool isTrans2Fill;

	private float timeDashFill;

	private float timeFiFill;

	private float timeTransFill;

	private float timeTrans2Fill;

	private float dtime;

	private float ftime;

	private float ttime;

	private float t2time;

	private float colD;

	private float colF;

	private float colT;

	private float colT2;

	public bool hoisinhchua;

	private Animator BlackSceneAnim;

	private float FiGAdd;

	private float fiGTime;

	private bool fiGing;

	private int rand1;

	public string gacurrentLevel;


	private int rand2;

	private int rand;

	private int ra1;

	private int ra2;

	private int ra;

	private int ran;

	private int ran1;

	private int ran2;

	private float timeToTalk;

	private float timeToEnemyXien;

	private float timeToEnemyChem;

	private bool dieing;

	private int skillDash;

	private int skillFi;

	private int skillHP;

	private int skillTrans;

	[HideInInspector]
	public int crit;

	[HideInInspector]
	public float dashCoolDown;

	[HideInInspector]
	public float timeBV;

	[HideInInspector]
	public float transCoolDown;

	[HideInInspector]
	public float transTime;

	private int chet;

	private AdCreator adCT;

	private bool xemadchua;

	public int checkCoin;

	public int checkDm;

	public bool lvBoss;

	private int kieucong;
}
