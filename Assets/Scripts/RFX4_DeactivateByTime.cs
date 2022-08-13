using System;
using UnityEngine;

public class RFX4_DeactivateByTime : MonoBehaviour
{
	private void OnEnable()
	{
		this.canUpdateState = true;
	}

	private void Update()
	{
		if (this.canUpdateState)
		{
			this.canUpdateState = false;
			base.Invoke("DeactivateThis", this.DeactivateTime);
		}
	}

	private void DeactivateThis()
	{
		base.gameObject.SetActive(false);
	}

	public float DeactivateTime = 3f;

	private bool canUpdateState;
}
