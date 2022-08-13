using System;
using UnityEngine;

public class UnlockAll : MonoBehaviour
{
	private void Start()
	{
		for (int i = 1; i < 36; i++)
		{
			PlayerPrefs.SetInt(i.ToString(), 2);
		}
		PlayerPrefs.Save();
	}

	private void Update()
	{
	}
}
