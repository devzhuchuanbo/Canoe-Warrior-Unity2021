using System;
using UnityEngine;

public class RFX4_DemoReactivation : MonoBehaviour
{
	private void Start()
	{
		base.InvokeRepeating("Reactivate", 0f, this.ReactivationTime);
	}

	private void Reactivate()
	{
		this.Effect.SetActive(false);
		this.Effect.SetActive(true);
	}

	public float ReactivationTime = 5f;

	public GameObject Effect;
}
