using System;
using UnityEngine;
using UnityEngine.UI;

public class MainSlideLevels : MonoBehaviour
{
	private void Awake()
	{
		if (this.required)
		{
			int num = 0;
			this.lvMax = this.requiredKey + "1";
			if (PlayerPrefs.HasKey(this.lvMax))
			{
				for (int i = 15; i >= 1; i--)
				{
					string key = this.requiredKey + i.ToString() + "Scroll";
					int @int = PlayerPrefs.GetInt(key);
					num += @int;
				}
			}
			if (num < this.requiredValue)
			{
				this.firstLevel.SendMessage("CheckFirstLevel");
				this.requiredText.gameObject.SetActive(true);
			}
			else
			{
				this.requiredText.gameObject.SetActive(false);
			}
		}
	}

	private void OnEnable()
	{
		this.lvMax = this.bigLevelName + "1";
		if (PlayerPrefs.HasKey(this.lvMax))
		{
			this.fr1 = this.content.GetLocalPositionVector2XY();
			this.fr = this.fr1.x;
			this.tg = false;
			int num = 15;
			while (!this.tg && num > 1)
			{
				this.lvMax = this.bigLevelName + num.ToString();
				if (PlayerPrefs.GetInt(this.lvMax) == 1)
				{
					this.tg = true;
				}
				else
				{
					num--;
				}
			}
			this.target1 = this.target[num - 1];
			iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
			{
				"from",
				this.fr,
				"to",
				this.target1,
				"time",
				1,
				"easetype",
				iTween.EaseType.easeOutQuad,
				"onUpdate",
				"SlideLevel"
			}));
		}
	}

	private void Start()
	{
		this.lvMax = this.bigLevelName + "1";
		this.scrollTong = 0;
		if (PlayerPrefs.HasKey(this.lvMax))
		{
			for (int i = 15; i >= 1; i--)
			{
				this.allScroll = this.bigLevelName + i.ToString() + "Scroll";
				int @int = PlayerPrefs.GetInt(this.allScroll);
				this.scrollTong += @int;
			}
		}
		this.scrollTongText.text = this.scrollTong + "/45";
	}

	private void SlideLevel(float i)
	{
		this.content.SetLocalPositionX(i);
	}

	public RectTransform content;

	public string bigLevelName;

	public Text scrollTongText;

	public bool required;

	public string requiredKey;

	public int requiredValue;

	public Transform firstLevel;

	public Transform requiredText;

	public float[] target;

	private float fr;

	private float target1;

	private Vector2 fr1;

	private string lvMax;

	private string allScroll;

	private bool tg;

	private int scrollTong;
}
