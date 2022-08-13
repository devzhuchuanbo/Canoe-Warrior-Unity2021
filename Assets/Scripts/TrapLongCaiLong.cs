using System;
using UnityEngine;

public class TrapLongCaiLong : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D coll)
	{
		if (!this.sended)
		{
			GameObject.FindGameObjectWithTag("Effect").GetComponent<EffectController>().LongRoi();
			this.sended = true;
		}
	}

	private bool sended;
}
