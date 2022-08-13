using System;
using UnityEngine;

public class RFX4_StartDelay : MonoBehaviour
{
	private void OnEnable()
	{
		this.ActivatedGameObject.SetActive(false);
		base.Invoke("ActivateGO", this.Delay);
	}

	private void ActivateGO()
	{
		this.ActivatedGameObject.SetActive(true);
	}

	private void OnDisable()
	{
		base.CancelInvoke("ActivateGO");
	}

	public GameObject ActivatedGameObject;

	public float Delay = 1f;
}
