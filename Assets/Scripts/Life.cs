using System;
using UnityEngine;

public class Life : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	public void LifeDown(int i)
	{
		if (i == 2)
		{
			UnityEngine.Object.Destroy(this.Life3.gameObject);
		}
		else if (i == 1)
		{
			UnityEngine.Object.Destroy(this.Life2.gameObject);
		}
		else
		{
			UnityEngine.Object.Destroy(this.Life1.gameObject);
		}
	}

	public Transform Life3;

	public Transform Life2;

	public Transform Life1;
}
