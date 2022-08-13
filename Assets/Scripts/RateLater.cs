using System;
using UnityEngine;

public class RateLater : MonoBehaviour
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
			UnityEngine.Debug.Log("Later,quit");
			Application.Quit();
		}
	}

	public Color ColorON;

	public Color ColorOFF;

	public SpriteRenderer ButtonEnabledSprite;

	private bool tg;

	public AudioSource click;
}
