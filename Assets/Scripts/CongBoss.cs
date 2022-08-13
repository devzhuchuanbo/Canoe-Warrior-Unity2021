using System;
using UnityEngine;

public class CongBoss : MonoBehaviour
{
	private void Start()
	{
		this.anim = base.GetComponent<Animator>();
		this.open = false;
		this.active = false;
	}

	private void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Player" && !this.open)
		{
			if (this.active)
			{
				this.open = true;
				this.anim.SetTrigger("Open");
				base.Invoke("Close", this.TimeToClose);
			}
			else
			{
				this.open = true;
				this.anim.SetTrigger("Open");
			}
		}
	}

	private void Close()
	{
		this.anim.SetTrigger("Close");
	}

	public void Closed()
	{
		base.Invoke("TriggerOn", 2f);
	}

	private void TriggerOn()
	{
		this.open = false;
	}

	private void Active()
	{
		this.anim.SetTrigger("Close");
		this.active = true;
	}

	private bool open;

	private Animator anim;

	public float TimeToClose;

	private bool active;
}
