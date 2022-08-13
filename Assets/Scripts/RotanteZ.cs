using System;
using UnityEngine;

public class RotanteZ : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		base.transform.Rotate(Vector3.forward * (this.speed * Time.deltaTime));
	}

	public float speed;
}
