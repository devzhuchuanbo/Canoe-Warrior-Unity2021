using System;
using UnityEngine;

public class RFX4_CopyPosition : MonoBehaviour
{
	private void Start()
	{
		this.t = base.transform;
	}

	private void Update()
	{
		this.t.position = this.CopiedTransform.position;
	}

	public Transform CopiedTransform;

	private Transform t;
}
