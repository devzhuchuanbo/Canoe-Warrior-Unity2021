using System;
using UnityEngine;

public class RFX4_ReplaceModelOnCollision : MonoBehaviour
{
	private void OnCollisionEnter(Collision collision)
	{
		if (!this.isCollided)
		{
			this.isCollided = true;
			this.PhysicsObjects.SetActive(true);
			MeshRenderer component = base.GetComponent<MeshRenderer>();
			if (component != null)
			{
				component.enabled = false;
			}
			Rigidbody component2 = base.GetComponent<Rigidbody>();
			component2.isKinematic = true;
			component2.detectCollisions = false;
		}
	}

	private void OnEnable()
	{
		this.isCollided = false;
		this.PhysicsObjects.SetActive(false);
		MeshRenderer component = base.GetComponent<MeshRenderer>();
		if (component != null)
		{
			component.enabled = true;
		}
		Rigidbody component2 = base.GetComponent<Rigidbody>();
		component2.isKinematic = false;
		component2.detectCollisions = true;
	}

	public GameObject PhysicsObjects;

	private bool isCollided;

	private Transform t;
}
