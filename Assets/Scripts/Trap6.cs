using System;
using UnityEngine;

public class Trap6 : MonoBehaviour
{
	private void Start()
	{
		this.player = GameObject.FindGameObjectWithTag("Player");
	}

	private void Trap6PlaySound()
	{
		if (!this.player)
		{
			this.trap6AudioSource.Stop();
		}
		else if (this.player.transform.position.x - 20f > base.transform.position.x || this.player.transform.position.x + 20f < base.transform.position.x)
		{
			this.trap6AudioSource.Stop();
		}
		else
		{
			this.trap6AudioSource.Play();
			UnityEngine.Object.Instantiate(this.TiaLua, this.vec3[0].position, this.vec3[0].rotation);
			UnityEngine.Object.Instantiate(this.TiaLua, this.vec3[1].position, this.vec3[1].rotation);
		}
	}

	public AudioSource trap6AudioSource;

	public GameObject TiaLua;

	public Transform[] vec3;

	private GameObject player;
}
