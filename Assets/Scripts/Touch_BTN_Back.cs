using System;
using UnityEngine;

public class Touch_BTN_Back : MonoBehaviour
{
	public void OnPress_IE()
	{
		this.ButtonEnabledSprite.color = this.ColorON;
		this.ClickSound.Play();
		this.tg = true;
	}

	public void OnRelease_IE()
	{
		this.ButtonEnabledSprite.color = this.ColorOFF;
		if (this.tg)
		{
			this.tg = false;
			UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelect");
		}
	}

	public Color ColorON;

	public Color ColorOFF;

	public AudioSource ClickSound;

	public MainEventsLog mainEventScript;

	public SpriteRenderer ButtonEnabledSprite;

	private bool tg;
}
