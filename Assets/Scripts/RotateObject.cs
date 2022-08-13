using System;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
	private void Start()
	{
		this.rotate = true;
	}

	private void Update()
	{
		if (this.rotate)
		{
			base.transform.Rotate(Vector3.up * (7f * Time.deltaTime));
		}
	}

	public bool rotate = true;
}
