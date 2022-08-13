using System;
using UnityEngine;

public class LevelSelectButton : MonoBehaviour
{
	private void Start()
	{
		this.LevelText.text = this.level.ToString();
		this.levelKey = this.level.ToString();
		if (!PlayerPrefs.HasKey(this.levelKey))
		{
			if (this.levelKey != "1")
			{
				PlayerPrefs.SetInt(this.levelKey, 0);
				PlayerPrefs.Save();
				this.star = 0;
			}
			else
			{
				PlayerPrefs.SetInt(this.levelKey, 1);
				PlayerPrefs.Save();
				this.star = 1;
			}
		}
		else
		{
			this.star = PlayerPrefs.GetInt(this.levelKey);
		}
		if (this.star == 0)
		{
			this.starRenderer.sprite = this.lockSprite;
		}
		else if (this.star == 1)
		{
			this.starRenderer.sprite = this.star_0;
		}
		else if (this.star == 2)
		{
			this.starRenderer.sprite = this.star_1;
		}
		else if (this.star == 3)
		{
			this.starRenderer.sprite = this.star_2;
		}
		else if (this.star == 4)
		{
			this.starRenderer.sprite = this.star_3;
		}
	}

	public void OnPress_IE()
	{
		this.ButtonEnabledSprite.color = this.ColorON;
		this.click.Play();
		if (this.star > 0)
		{
			this.tg = true;
		}
	}

	public void OnRelease_IE()
	{
		this.ButtonEnabledSprite.color = this.ColorOFF;
		if (this.tg)
		{
			this.show.SetActive(true);
			UnityEngine.SceneManagement.SceneManager.LoadScene(this.levelKey);
		}
	}

	public Color ColorON;

	public Color ColorOFF;

	public SpriteRenderer ButtonEnabledSprite;

	public GameObject show;

	public AudioSource click;

	public int level;

	public TextMesh LevelText;

	public SpriteRenderer starRenderer;

	public Sprite star_0;

	public Sprite star_1;

	public Sprite star_2;

	public Sprite star_3;

	public Sprite lockSprite;

	private string levelKey;

	private int star;

	private bool tg;
}
