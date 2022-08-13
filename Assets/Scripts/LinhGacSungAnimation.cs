using System;
using UnityEngine;

public class LinhGacSungAnimation : MonoBehaviour
{
	public void Ban()
	{
		this.mainScript.Ban();
	}

	public void nap()
	{
		this.mainScript.Nap();
	}

	public LinhGacSung mainScript;
}
