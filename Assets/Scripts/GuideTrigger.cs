using System;
using UnityEngine;

public class GuideTrigger : MonoBehaviour
{
	private void Start()
	{
		this.action = false;
		this.imdone = false;
		this.canresume = false;
		if (!PlayerPrefs.HasKey(this.G))
		{
			LogTutorialCompleteEvent();

			PlayerPrefs.SetInt(this.G, 0);
			PlayerPrefs.Save();
		}
		if (PlayerPrefs.GetInt(this.G) == 1)
		{
			this.action = true;
			this.imdone = true;
			this.mainBtn.gameObject.SetActive(true);
		}
	}

	private void Update()
	{
		if (this.action && !this.imdone && Time.unscaledTime > this.timeToResume)
		{
			this.canresume = true;
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player" && !this.action)
		{
			foreach (Transform transform in this.ShowObj)
			{
				if (transform)
				{
					transform.gameObject.SetActive(true);
				}
			}
			this.mainBtn.gameObject.SetActive(true);
			this.pauseButton.gameObject.SetActive(false);
			this.action = true;
			Time.timeScale = 0f;
			this.timeToResume = Time.unscaledTime + this.timeFreeze;
		}
	}

	public void Resume()
	{
		if (this.canresume)
		{
			foreach (Transform transform in this.HideObj)
			{
				if (transform)
				{
					transform.gameObject.SetActive(false);
				}
			}
			this.pauseButton.gameObject.SetActive(true);
			this.imdone = true;
			Time.timeScale = 1f;
			PlayerPrefs.SetInt(this.G, 1);
			PlayerPrefs.Save();
		}
	}
	//FB
	public void LogTutorialCompleteEvent()
	{


	}

	public string G;

	public Transform[] ShowObj;

	public Transform[] HideObj;

	public Transform pauseButton;

	public Transform mainBtn;

	public float timeFreeze;

	private bool action;

	private bool imdone;

	private bool canresume;

	private float timeToResume;
}
