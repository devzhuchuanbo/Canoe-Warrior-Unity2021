using System;
using UnityEngine;

public class RFX4_LightCurves : MonoBehaviour
{
	private void Awake()
	{
		this.lightSource = base.GetComponent<Light>();
		this.lightSource.intensity = this.LightCurve.Evaluate(0f);
	}

	private void OnEnable()
	{
		this.startTime = Time.time;
		this.canUpdate = true;
	}

	private void Update()
	{
		float num = Time.time - this.startTime;
		if (this.canUpdate)
		{
			float intensity = this.LightCurve.Evaluate(num / this.GraphTimeMultiplier) * this.GraphIntensityMultiplier;
			this.lightSource.intensity = intensity;
		}
		if (num >= this.GraphTimeMultiplier)
		{
			if (this.IsLoop)
			{
				this.startTime = Time.time;
			}
			else
			{
				this.canUpdate = false;
			}
		}
	}

	public AnimationCurve LightCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

	public float GraphTimeMultiplier = 1f;

	public float GraphIntensityMultiplier = 1f;

	public bool IsLoop;

	[HideInInspector]
	public bool canUpdate;

	private float startTime;

	private Light lightSource;
}
