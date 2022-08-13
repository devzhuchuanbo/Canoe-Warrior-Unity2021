using System;
using UnityEngine;

public class HuongDan : MonoBehaviour
{
	private void Start()
	{
		if (!PlayerPrefs.HasKey(this.nameGuild))
		{
			PlayerPrefs.SetInt(this.nameGuild, 1);
			PlayerPrefs.Save();
		}
		else
		{
			this.isActive = PlayerPrefs.GetInt(this.nameGuild);
		}
		if (this.isActive == 2)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			this.guid.gameObject.SetActive(true);
			this.mainEvenLogScript.pausing = true;
			PlayerPrefs.SetInt(this.nameGuild, 2);
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	public MainEventsLog mainEvenLogScript;

	public GameObject guid;

	public string nameGuild;

	private int isActive;
}
