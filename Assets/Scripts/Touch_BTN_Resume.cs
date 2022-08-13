using System;
using UnityEngine;

public class Touch_BTN_Resume : MonoBehaviour
{
	public void OnPress_IE()
	{
		this.tg = true;
		this.ButtonEnabledSprite.color = this.ColorON;
		this.ClickSound.Play();
	}

	public void OnRelease_IE()
	{
		this.ButtonEnabledSprite.color = this.ColorOFF;
		if (this.tg)
		{
			this.tg = false;
			this.mainScript.pausing = false;
			this.mainScript.AdShow = false;
		}
	}

	public Color ColorON;

	public Color ColorOFF;

	public AudioSource ClickSound;

	public MainEventsLog mainScript;

	public SpriteRenderer ButtonEnabledSprite;

	private bool tg;
}
