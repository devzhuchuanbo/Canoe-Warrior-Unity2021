using System;
using UnityEngine;

public class Quit : MonoBehaviour
{
	private void Start()
	{
		if (!PlayerPrefs.HasKey("Rate"))
		{
			this.rate = 0;
			this.rated = 0;
			this.tx = 1;
			PlayerPrefs.SetInt("Rate", this.rate);
			PlayerPrefs.SetInt("Rated", this.rated);
			PlayerPrefs.SetInt("Tanxuat", this.tx);
		}
		else
		{
			this.rate = PlayerPrefs.GetInt("Rate");
			this.rated = PlayerPrefs.GetInt("Rated");
			this.tx = PlayerPrefs.GetInt("Tanxuat");
		}
	}

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
			if (this.rated == 0)
			{
				if (PlayerPrefs.HasKey("5"))
				{
					int @int = PlayerPrefs.GetInt("5");
					if (@int > 0)
					{
						if (this.rate > this.tx)
						{
							this.rate = 0;
							PlayerPrefs.SetInt("Rate", this.rate);
							PlayerPrefs.Save();
							UnityEngine.Debug.Log("Do somethings here!");
							this.bang.gameObject.transform.position = Vector3.Lerp(this.bang.transform.position, this.Vec3, 1f);
						}
						else
						{
							this.rate++;
							PlayerPrefs.SetInt("Rate", this.rate);
							PlayerPrefs.Save();
							Application.Quit();
							UnityEngine.Debug.Log("Quit");
						}
					}
					else
					{
						Application.Quit();
						UnityEngine.Debug.Log("Quit");
					}
				}
				else
				{
					Application.Quit();
					UnityEngine.Debug.Log("Quit");
				}
			}
			else
			{
				Application.Quit();
				UnityEngine.Debug.Log("Quit");
			}
		}
	}

	public Color ColorON;

	public Color ColorOFF;

	public GameObject bang;

	public Vector3 Vec3;

	public SpriteRenderer ButtonEnabledSprite;

	public AudioSource click;

	private bool tg;

	private int rate;

	private int rated;

	private int tx;
}
