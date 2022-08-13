using System;
using UnityEngine;

public class FadeInOutScale : MonoBehaviour
{
	private void Start()
	{
		this.t = base.transform;
		this.oldScale = this.t.localScale;
		this.isInitialized = true;
	}

	private void InitDefaultVariables()
	{
		this.t.localScale = Vector3.zero;
		this.time = 0f;
		this.oldSin = 0f;
		this.canUpdate = true;
		this.updateTime = true;
	}

	private void OnEnable()
	{
		if (this.isInitialized)
		{
			this.InitDefaultVariables();
		}
	}

	private void Update()
	{
		if (!this.canUpdate)
		{
			return;
		}
		if (this.updateTime)
		{
			this.time = Time.time;
			this.updateTime = false;
		}
		float num = Mathf.Sin((Time.time - this.time) / this.Speed);
		float num2 = num * this.MaxScale;
		if (this.FadeInOutStatus == FadeInOutStatus.In)
		{
			this.t.localScale = new Vector3(this.oldScale.x * num2, this.oldScale.y * num2, this.oldScale.z * num2);
		}
		if (this.FadeInOutStatus == FadeInOutStatus.Out)
		{
			this.t.localScale = new Vector3(this.MaxScale * this.oldScale.x - this.oldScale.x * num2, this.MaxScale * this.oldScale.y - this.oldScale.y * num2, this.MaxScale * this.oldScale.z - this.oldScale.z * num2);
		}
		if (this.oldSin > num)
		{
			this.canUpdate = false;
		}
		this.oldSin = num;
	}

	public FadeInOutStatus FadeInOutStatus;

	public float Speed = 1f;

	public float MaxScale = 2f;

	private Vector3 oldScale;

	private float time;

	private float oldSin;

	private bool updateTime = true;

	private bool canUpdate = true;

	private Transform t;

	private bool isInitialized;
}
