using System;
using UnityEngine;

public class AutoRotator : MonoBehaviour
{
	private void Update()
	{
		base.transform.Rotate(new Vector3(0f, 0f, this.RotationSpeed * Time.deltaTime));
	}

	public float RotationSpeed;
}
