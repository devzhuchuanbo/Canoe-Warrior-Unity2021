using System;
using UnityEngine;

public class Trap3 : MonoBehaviour
{
	private void Start()
	{
		this.player = GameObject.FindGameObjectWithTag("Player");
	}

	private void Update()
	{
		if (!this.player)
		{
			this.trap3AudioSource.Stop();
		}
		else if (this.player.transform.position.x - 20f > base.transform.position.x || this.player.transform.position.x + 20f < base.transform.position.x)
		{
			this.trap3AudioSource.Stop();
		}
		else
		{
			this.trap3AudioSource.Play();
		}
	}

	public AudioSource trap3AudioSource;

	private GameObject player;
}
