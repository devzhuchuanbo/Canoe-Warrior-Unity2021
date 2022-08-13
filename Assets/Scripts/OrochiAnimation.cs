using System;
using UnityEngine;

public class OrochiAnimation : MonoBehaviour
{
	private void Start()
	{
	}

	public void Xien()
	{
		this.mainScript.Xien();
	}

	public void ChemFx()
	{
		this.mainScript.XienFx();
	}

	public void Roi()
	{
		this.mainScript.RoiXuong();
	}

	public void GoiQuai()
	{
		this.mainScript.SummonBee();
	}

	public OrochiBoss mainScript;
}
