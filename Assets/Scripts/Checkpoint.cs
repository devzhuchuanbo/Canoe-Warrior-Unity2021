using System;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
	private void Start()
	{
		this.anim = base.GetComponent<Animator>();
		this.action = false;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player" && !this.action)
		{
			this.action = true;
			other.GetComponent<NinjaMovementScript>().EnterCheckPoint(base.gameObject.transform.position);
			base.GetComponent<Collider2D>().enabled = false;
			this.anim.SetTrigger("Hit");
		}
	}

	private Animator anim;

	private bool action;
}
