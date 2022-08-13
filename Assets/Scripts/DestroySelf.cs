using System;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
	private void Update()
	{
		this.time += Time.deltaTime;
		if (this.time > this.timeout)
		{
			this.time = 0f;
			base.gameObject.SetActive(false);
		}
	}

	public void SetTimeOut(float t)
	{
		this.timeout = t;
	}

	public void ResetTime()
	{
		this.time = 0f;
	}

	public float timeout = 0.5f;

	private float time;
}
