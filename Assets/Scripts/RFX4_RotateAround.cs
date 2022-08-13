using System;
using UnityEngine;

public class RFX4_RotateAround : MonoBehaviour
{
	private void Start()
	{
		this.t = base.transform;
		this.rotation = this.t.rotation;
	}

	private void OnEnable()
	{
		this.currentTime = 0f;
		if (this.t != null)
		{
			this.t.rotation = this.rotation;
		}
	}

	private void Update()
	{
		if (this.currentTime >= this.LifeTime && this.LifeTime > 0.0001f)
		{
			return;
		}
		this.currentTime += Time.deltaTime;
		this.t.Rotate(this.RotateVector * Time.deltaTime);
	}

	public Vector3 Offset = Vector3.forward;

	public Vector3 RotateVector = Vector3.forward;

	public float LifeTime = 1f;

	private Transform t;

	private float currentTime;

	private Quaternion rotation;
}
