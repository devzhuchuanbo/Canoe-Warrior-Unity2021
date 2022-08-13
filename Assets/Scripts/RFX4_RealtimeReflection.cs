using System;
using UnityEngine;

public class RFX4_RealtimeReflection : MonoBehaviour
{
	private void Awake()
	{
		this.probe = base.GetComponent<ReflectionProbe>();
		this.camT = Camera.main.transform;
	}

	private void Update()
	{
		Vector3 position = this.camT.position;
		this.probe.transform.position = new Vector3(position.x, position.y * -1f, position.z);
		this.probe.RenderProbe();
	}

	private ReflectionProbe probe;

	private Transform camT;
}
