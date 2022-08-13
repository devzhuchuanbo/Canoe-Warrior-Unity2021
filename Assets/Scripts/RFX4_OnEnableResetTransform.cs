using System;
using UnityEngine;

public class RFX4_OnEnableResetTransform : MonoBehaviour
{
	private void OnEnable()
	{
		if (!this.isInitialized)
		{
			this.isInitialized = true;
			this.t = base.transform;
			this.startPosition = this.t.position;
			this.startRotation = this.t.rotation;
			this.startScale = this.t.localScale;
		}
		else
		{
			this.t.position = this.startPosition;
			this.t.rotation = this.startRotation;
			this.t.localScale = this.startScale;
		}
	}

	private void OnDisable()
	{
		if (!this.isInitialized)
		{
			this.isInitialized = true;
			this.t = base.transform;
			this.startPosition = this.t.position;
			this.startRotation = this.t.rotation;
			this.startScale = this.t.localScale;
		}
		else
		{
			this.t.position = this.startPosition;
			this.t.rotation = this.startRotation;
			this.t.localScale = this.startScale;
		}
	}

	private Transform t;

	private Vector3 startPosition;

	private Quaternion startRotation;

	private Vector3 startScale;

	private bool isInitialized;
}
