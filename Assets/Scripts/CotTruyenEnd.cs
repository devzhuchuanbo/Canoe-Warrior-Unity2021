using System;
using UnityEngine;

public class CotTruyenEnd : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	public void ChangeTheme()
	{
		this.themeAudioSource.clip = this.theme2;
		this.themeAudioSource.Play();
	}

	public void ChangeTheme2()
	{
		this.themeAudioSource.clip = this.theme3;
		this.themeAudioSource.Play();
	}

	public void BackToMain()
	{
		this.loadingPanel.gameObject.SetActive(true);
		UnityEngine.SceneManagement.SceneManager.LoadScene("Select");
	}

	public void ResetAllLevels()
	{
		for (int i = 1; i <= 15; i++)
		{
			string text = "Truc" + i.ToString();
			string key = text + "Scroll";
			if (text != "Truc1")
			{
				PlayerPrefs.SetInt(text, 0);
			}
			PlayerPrefs.SetInt(key, 0);
			text = "Rung" + i.ToString();
			key = text + "Scroll";
			PlayerPrefs.SetInt(text, 0);
			PlayerPrefs.SetInt(key, 0);
			text = "Nui" + i.ToString();
			key = text + "Scroll";
			PlayerPrefs.SetInt(text, 0);
			PlayerPrefs.SetInt(key, 0);
			PlayerPrefs.SetInt("Boss1", 0);
			PlayerPrefs.Save();
			this.loadingPanel.gameObject.SetActive(true);
			UnityEngine.SceneManagement.SceneManager.LoadScene("Select");
		}
	}

	public AudioSource themeAudioSource;

	public AudioClip theme2;

	public AudioClip theme3;

	public Transform loadingPanel;
}
