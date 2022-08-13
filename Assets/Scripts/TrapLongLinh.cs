using System;
using UnityEngine;

public class TrapLongLinh : MonoBehaviour
{
	private void Start()
	{
		this.playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<NinjaMovementScript>();
		base.Invoke("Hehe", 0.3f);
	}

	private void Hehe()
	{
		GameObject.FindGameObjectWithTag("Effect").GetComponent<EffectController>().DamBayLong();
	}

	public void Xien()
	{
		GameObject.FindGameObjectWithTag("Effect").GetComponent<EffectController>().ToeMau(this.dauGiao.position);
		if (!this.playerScript)
		{
			this.playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<NinjaMovementScript>();
		}
		this.playerScript.NinjaBiXien();
	}

	public Transform dauGiao;

	private NinjaMovementScript playerScript;
}
