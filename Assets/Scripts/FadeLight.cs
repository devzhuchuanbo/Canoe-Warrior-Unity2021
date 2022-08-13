using System;
using UnityEngine;

public class FadeLight : MonoBehaviour
{
	private void Awake()
	{
		this.mStartTime = Time.time;
		this.mEndTime = this.mStartTime + this.maxTime;
	}

	private void Update()
	{
		if (this.lightToDim)
		{
			this.lightToDim.intensity = Mathf.InverseLerp(this.mEndTime, this.mStartTime, Time.time) * 4f;
		}
	}

	public Light lightToDim;

	public float maxTime = 30f;

	private float mEndTime;

	private float mStartTime;
}
