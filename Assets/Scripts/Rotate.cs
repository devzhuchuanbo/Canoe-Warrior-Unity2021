// dnSpy decompiler from Assembly-UnityScript.dll class: Rotate
using System;
using UnityEngine;

[Serializable]
public class Rotate : MonoBehaviour
{
	public Rotate()
	{
		this.Speed = 3f;
		this.Rot = 80f;
	}

	public virtual void Awake()
	{
		this.bottom = this.transform.position.y;
	}

	public virtual void Update()
	{
		this.transform.Rotate(new Vector3((float)0, this.Rot, (float)0) * Time.deltaTime, Space.World);
	}

	public virtual void Main()
	{
	}

	public float Speed;

	public float Rot;

	private float bottom;
}
