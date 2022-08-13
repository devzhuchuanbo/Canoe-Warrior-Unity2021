using System;
using UnityEngine;

public class Touch_BTN_Play : MonoBehaviour
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
			UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelect");
		}
	}

	public Color ColorON;

	public Color ColorOFF;

	public SpriteRenderer ButtonEnabledSprite;

	public AudioSource click;

	private bool tg;
}
