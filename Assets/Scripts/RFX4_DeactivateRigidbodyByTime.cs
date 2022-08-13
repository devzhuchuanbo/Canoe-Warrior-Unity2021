using System;
using UnityEngine;

public class RFX4_DeactivateRigidbodyByTime : MonoBehaviour
{
	private void OnEnable()
	{
		Rigidbody component = base.GetComponent<Rigidbody>();
		component.isKinematic = false;
		component.detectCollisions = true;
		base.Invoke("Deactivate", this.TimeDelayToDeactivate);
	}

	private void Deactivate()
	{
		Rigidbody component = base.GetComponent<Rigidbody>();
		component.isKinematic = true;
		component.detectCollisions = false;
	}

	public float TimeDelayToDeactivate = 6f;
}
