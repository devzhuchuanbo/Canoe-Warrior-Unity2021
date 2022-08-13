using System;
using System.Collections;
using UnityEngine;

public class Touch_BTN_ShareFB : MonoBehaviour
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
			base.StartCoroutine(this.PostFBScreenshot());
		}
	}

	private IEnumerator PostFBScreenshot()
	{
		yield return new WaitForEndOfFrame();
		int width = Screen.width;
		int height = Screen.height;
		Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
		tex.ReadPixels(new Rect(0f, 0f, (float)width, (float)height), 0, 0);
		tex.Apply();
		UnityEngine.Object.Destroy(tex);
		yield break;
	}

	public Color ColorON;

	public Color ColorOFF;

	public AudioSource ClickSound;

	public SpriteRenderer ButtonEnabledSprite;

	private bool tg;
}
