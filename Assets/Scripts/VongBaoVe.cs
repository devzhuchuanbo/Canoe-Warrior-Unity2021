using System;
using UnityEngine;

public class VongBaoVe : MonoBehaviour
{
	private void Start()
	{
		this.timeBV = Time.time - 1f;
		this.remove = false;
		this.player = GameObject.FindGameObjectWithTag("Player");
		this.particle = base.GetComponent<ParticleSystem>();
	}

	private void FixedUpdate()
	{
		if (this.timeBV > Time.time)
		{
			base.transform.position = this.player.transform.position;
		}
		else if (!this.remove)
		{
			this.remove = true;
			base.transform.position = new Vector3(100f, 100f, 0f);
			this.particle.Stop();
		}
	}

	public void BV(float t)
	{
		this.timeBV = Time.time + t;
		this.remove = false;
		if (this.player == null)
		{
			this.player = GameObject.FindGameObjectWithTag("Player");
		}
		base.transform.position = this.player.transform.position;
		this.particle.Play();
	}

	public void playerDie()
	{
		this.timeBV = Time.time;
	}

	[HideInInspector]
	public float timeBV;

	private ParticleSystem particle;

	private GameObject player;

	private bool remove;
}
