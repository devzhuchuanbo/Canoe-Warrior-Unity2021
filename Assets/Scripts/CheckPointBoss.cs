using System;
using UnityEngine;

public class CheckPointBoss : MonoBehaviour
{
	private void Start()
	{
		this.action = false;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player" && !this.action)
		{
			this.action = true;
			this.bossPlatform.gameObject.SetActive(true);
			this.mauBoss.gameObject.SetActive(true);
		}
	}

	public Transform bossPlatform;

	public Transform mauBoss;

	private bool action;
}
