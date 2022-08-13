using System;
using UnityEngine;

public class RFX4_PhysXSetImpulse : MonoBehaviour
{
	private void Start()
	{
		this.rig = base.GetComponent<Rigidbody>();
		this.t = base.transform;
	}

	private void FixedUpdate()
	{
		if (this.rig != null)
		{
			this.rig.AddForce(this.t.forward * this.Force, this.ForceMode);
		}
	}

	private void OnDisable()
	{
		if (this.rig != null)
		{
			this.rig.velocity = Vector3.zero;
		}
	}

	public float Force = 1f;

	public ForceMode ForceMode;

	private Rigidbody rig;

	private Transform t;
}
