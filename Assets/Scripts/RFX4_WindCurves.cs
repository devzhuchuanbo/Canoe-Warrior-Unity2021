using System;
using UnityEngine;

public class RFX4_WindCurves : MonoBehaviour
{
	private void Awake()
	{
		this.windZone = base.GetComponent<WindZone>();
		this.windZone.windMain = this.WindCurve.Evaluate(0f);
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
			float windMain = this.WindCurve.Evaluate(num / this.GraphTimeMultiplier) * this.GraphIntensityMultiplier;
			this.windZone.windMain = windMain;
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

	public AnimationCurve WindCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

	public float GraphTimeMultiplier = 1f;

	public float GraphIntensityMultiplier = 1f;

	public bool IsLoop;

	private bool canUpdate;

	private float startTime;

	private WindZone windZone;
}
