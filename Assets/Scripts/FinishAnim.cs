using System;
using UnityEngine;

public class FinishAnim : MonoBehaviour
{
	private void Start()
	{
		this.lifet = this.LifeText.GetComponent<TextMesh>();
		this.killt = this.KillText.GetComponent<TextMesh>();
		this.collectt = this.CollectText.GetComponent<TextMesh>();
		this.completet = this.CompletedText.GetComponent<TextMesh>();
		this.lifet.text = string.Empty;
		this.killt.text = string.Empty;
		this.collectt.text = string.Empty;
		this.tg1 = 0;
		this.tg2 = 0;
		this.tg3 = 0;
	}

	public void LifeTextUpdate()
	{
	}

	public void KillTextUpdate()
	{
	}

	public void CollectTextUpdate()
	{
	}

	private void UpdateLifeDisplay(int newLife)
	{
		this.lifet.text = newLife + "/3";
		if (this.tg1 < newLife)
		{
			this.numberSound.Play();
			this.tg1 = newLife;
		}
	}

	private void UpdateKillDisplay(int newLife)
	{
		if (this.tg2 < newLife)
		{
			this.numberSound.Play();
			this.tg2 = newLife;
		}
	}

	private void UpdateCollectDisplay(int newLife)
	{
		if (this.tg3 < newLife)
		{
			this.numberSound.Play();
			this.tg3 = newLife;
		}
	}

	public MainEventsLog mainEventLogScript;

	public GameObject LifeText;

	public GameObject KillText;

	public GameObject CollectText;

	public GameObject CompletedText;

	private TextMesh lifet;

	private TextMesh killt;

	private TextMesh collectt;

	private TextMesh completet;

	private int tg1;

	private int tg2;

	private int tg3;

	public AudioSource numberSound;
}
