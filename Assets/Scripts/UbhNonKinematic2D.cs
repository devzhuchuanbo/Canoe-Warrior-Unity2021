using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class UbhNonKinematic2D : UbhMonoBehaviour
{
	private void Awake()
	{
		base.rigidbody2D.isKinematic = true;
		base.enabled = false;
	}

	private void Update()
	{
		base.enabled = false;
	}

	private void OnEnable()
	{
		base.enabled = false;
	}

	private void OnDisable()
	{
	}
}
