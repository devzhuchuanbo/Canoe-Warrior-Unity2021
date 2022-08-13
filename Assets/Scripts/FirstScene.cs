using System;
using UnityEngine;

public class FirstScene : MonoBehaviour
{
	void Awake()
	{
	}
	private void Start()
	{

		if (!PlayerPrefs.HasKey("AudioValue"))
		{
			PlayerPrefs.SetFloat("AudioValue", 1f);
			PlayerPrefs.SetInt("MusicOn", 1);
			PlayerPrefs.SetInt("Theme", 0);
			PlayerPrefs.Save();
		}
		AudioListener.volume = PlayerPrefs.GetFloat("AudioValue");
		if (PlayerPrefs.GetInt("MusicOn") == 1)
		{
			this.themeMusic.Play();
		}
		this.t = Time.time;

	}

	private void Update()
	{
		if (!this.ok && Time.time > this.t + 5f)
		{
			this.ok = true;
			this.Play();
		}
	}

	public void Play()
	{
		this.click.Play();
		bool flag;
		if (!PlayerPrefs.HasKey("CotTruyen"))
		{
			PlayerPrefs.SetInt("CotTruyen", 0);
			PlayerPrefs.Save();
			flag = false;
		}
		else
		{
			flag = true;
		}
		if (!flag)
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene("cot1");
		}
		else
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene("Select");
		}
	}

	public AudioSource click;

	public AudioSource themeMusic;

	private float t;

	private bool ok;
}
