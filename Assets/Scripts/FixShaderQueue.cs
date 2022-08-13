using System;
using UnityEngine;

public class FixShaderQueue : MonoBehaviour
{
	private void Start()
	{
		this.rend = base.GetComponent<Renderer>();
		if (this.rend != null)
		{
			this.rend.sharedMaterial.renderQueue += this.AddQueue;
		}
		else
		{
			base.Invoke("SetProjectorQueue", 0.1f);
		}
	}

	private void SetProjectorQueue()
	{
		base.GetComponent<Projector>().material.renderQueue += this.AddQueue;
	}

	private void OnDisable()
	{
		if (this.rend != null)
		{
			this.rend.sharedMaterial.renderQueue = -1;
		}
	}

	public int AddQueue = 1;

	private Renderer rend;
}
