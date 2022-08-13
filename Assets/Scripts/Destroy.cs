using System;
using UnityEngine;

public class Destroy : MonoBehaviour
{
	private void Awake()
	{
		UnityEngine.Object.Destroy(base.gameObject, this.lifetime);
	}

	public float lifetime = 2f;
}
