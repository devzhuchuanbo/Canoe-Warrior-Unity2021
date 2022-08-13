using System;
using UnityEngine;

public class CotPhunDocSole : MonoBehaviour
{
	private void Start()
	{
		if (this.PhunSau)
		{
			base.Invoke("phun", this.phunTime);
		}
		else
		{
			base.Invoke("phun", 0f);
		}
	}

	private void phun()
	{
		this.PhunPart.Play();
		base.Invoke("DocSatOn", this.delayt);
		base.Invoke("NgungPhun", this.phunTime);
	}

	private void NgungPhun()
	{
		this.PhunPart.Stop();
		base.Invoke("DocSatOff", this.delayt);
		base.Invoke("phun", this.phunTime);
	}

	private void DocSatOn()
	{
		this.DocSat.SetActive(true);
	}

	private void DocSatOff()
	{
		this.DocSat.SetActive(false);
	}

	public ParticleSystem PhunPart;

	public bool PhunSau;

	public float phunTime;

	public float delayt;

	public GameObject DocSat;
}
