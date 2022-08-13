using System;
using UnityEngine;

public class Trap7 : MonoBehaviour
{
	private void Start()
	{
		this.player = GameObject.FindGameObjectWithTag("Player");
	}

	private void Trap7PlaySound()
	{
		if (!this.player)
		{
			this.trap7AudioSource.Stop();
		}
		else if (this.player.transform.position.x - 20f > base.transform.position.x || this.player.transform.position.x + 20f < base.transform.position.x)
		{
			this.trap7AudioSource.Stop();
		}
		else
		{
			this.trap7AudioSource.Play();
			this.TiaLua.transform.position = this.Up.position;
			this.TiaLua.Play();
		}
	}

	public AudioSource trap7AudioSource;

	public ParticleSystem TiaLua;

	public Transform Up;

	private GameObject player;
}
