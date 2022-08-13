using System;
using UnityEngine;

public class CucMau : MonoBehaviour
{
	private void Start()
	{
		this.anim = base.GetComponent<Animator>();
		this.action = false;
	}

	private void OnCollisionEnter2D(Collision2D coll)
	{
		if (!this.action)
		{
			this.action = true;
			if (this.anim)
			{
				this.anim.enabled = true;
			}
		}
	}

	private Animator anim;

	private bool action;
}
