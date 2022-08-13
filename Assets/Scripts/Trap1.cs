using System;
using UnityEngine;

public class Trap1 : MonoBehaviour
{
	private void Start()
	{
		this.anim = base.gameObject.GetComponent<Animator>();
	}

	private void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.tag == "Player")
		{
			this.anim.SetTrigger("Action");
		}
	}

	private void PlaySoundTrap1()
	{
		this.Trap1Sound.Play();
		this.Dust.transform.position = this.vec3.position;
		this.Dust.Play();
	}

	private void PlayDust()
	{
	}

	private Animator anim;

	public AudioSource Trap1Sound;

	public ParticleSystem Dust;

	public Transform vec3;
}
