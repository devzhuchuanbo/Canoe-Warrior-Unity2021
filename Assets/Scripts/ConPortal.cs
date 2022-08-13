using System;
using UnityEngine;

public class ConPortal : MonoBehaviour
{
	private void Start()
	{
	}

	private void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Player" && !this.sended)
		{
			this.samset.gameObject.SetActive(true);
			coll.gameObject.SendMessage("CongPortal", base.gameObject);
			this.sended = true;
			this.AudioFinish.Play();
			base.Invoke("P", 2f);
		}
	}

	private void P()
	{
		this.portal.gameObject.SetActive(true);
		this.AudioFinish.clip = this.samsetClip;
		this.AudioFinish.Play();
	}

	public AudioSource AudioFinish;

	public AudioClip samsetClip;

	public Transform samset;

	public Transform portal;

	private bool sended;
}
