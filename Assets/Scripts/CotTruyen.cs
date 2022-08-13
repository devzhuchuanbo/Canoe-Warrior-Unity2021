using System;
using UnityEngine;

public class CotTruyen : MonoBehaviour
{
	private void Start()
	{
		if (!PlayerPrefs.HasKey("TransInUse"))
		{
			PlayerPrefs.SetString("TransInUse", "bienhinh_goccay");
			PlayerPrefs.SetInt("bienhinh_goccay", 1);
			PlayerPrefs.Save();
		}
	}

	public void FinishStoryAnimation()
	{
		this.loadingPanel.gameObject.SetActive(true);
		UnityEngine.SceneManagement.SceneManager.LoadScene("truc1");
	}

	public void PlayS(int i)
	{
		if (i == 1)
		{
			this.sound1.Play();
		}
		else if (i == 2)
		{
			this.sound2.Play();
		}
		else if (i == 3)
		{
			this.sound3.Play();
		}
		else if (i == 4)
		{
			this.sound4.Play();
		}
		else if (i == 5)
		{
			this.sound5.Play();
		}
		else
		{
			this.sound6.Play();
		}
	}

	public Transform loadingPanel;

	public AudioSource sound1;

	public AudioSource sound2;

	public AudioSource sound3;

	public AudioSource sound4;

	public AudioSource sound5;

	public AudioSource sound6;

	private bool canSkip;
}
