using System;
using UnityEngine;

public class FiTieuPool : MonoBehaviour
{
	private void Start()
	{
		this.l = 0;
		this.r = 0;
	}

	public Transform GetFiL()
	{
		if (this.l >= this.FiL.Length)
		{
			this.l = 0;
		}
		this.l++;
		return this.FiL[this.l - 1];
	}

	public Transform GetFiR()
	{
		if (this.r >= this.FiR.Length)
		{
			this.r = 0;
		}
		this.r++;
		return this.FiR[this.r - 1];
	}

	public Transform[] FiL;

	public Transform[] FiR;

	public Transform GoldFiL;

	public Transform GoldFiR;

	private int l;

	private int r;
}
