using System;
using UnityEngine;

public class Touch_BTN_Jump : MonoBehaviour
{
	public void OnPress_IE()
	{
		this.NinjaMovScript.Button_Jump_press();
		this.ButtonEnabledSprite.color = this.ColorON;
	}

	public void OnRelease_IE()
	{
		this.NinjaMovScript.Button_Jump_release();
		this.ButtonEnabledSprite.color = this.ColorOFF;
	}

	public NinjaMovementScript NinjaMovScript;

	public Color ColorON;

	public Color ColorOFF;

	public SpriteRenderer ButtonEnabledSprite;
}
