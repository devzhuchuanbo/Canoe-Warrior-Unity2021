using System;
using UnityEngine;

public class AutoDestruc : MonoBehaviour
{
	private void Start()
	{
		UnityEngine.Object.Destroy(base.gameObject, 1f);
	}
}
