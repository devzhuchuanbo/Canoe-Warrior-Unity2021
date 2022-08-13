using System;
using UnityEngine;

public class RateNo : MonoBehaviour
{
	public void OnPress_IE()
	{
		this.tg = true;
		this.ButtonEnabledSprite.color = this.ColorON;
		this.click.Play();
	}

	public void OnRelease_IE()
	{
		this.ButtonEnabledSprite.color = this.ColorOFF;
		if (this.tg)
		{
			UnityEngine.Debug.Log("No,quit");
			PlayerPrefs.SetInt("Rated", 1);
			PlayerPrefs.Save();
			Application.Quit();
		}
	}

	public Color ColorON;

	public Color ColorOFF;

	public SpriteRenderer ButtonEnabledSprite;

	private bool tg;

	public AudioSource click;
}
