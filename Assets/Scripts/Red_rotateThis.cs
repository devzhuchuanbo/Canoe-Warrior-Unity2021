// dnSpy decompiler from Assembly-UnityScript.dll class: Red_rotateThis
using System;
using UnityEngine;

[Serializable]
public class Red_rotateThis : MonoBehaviour
{
	public Red_rotateThis()
	{
		this.rotationSpeedX = (float)90;
		this.local = true;
		this.rotationVector = new Vector3(this.rotationSpeedX, this.rotationSpeedY, this.rotationSpeedZ);
	}

	public virtual void Update()
	{
		if (this.local)
		{
			this.transform.Rotate(new Vector3(this.rotationSpeedX, this.rotationSpeedY, this.rotationSpeedZ) * Time.deltaTime);
		}
		if (!this.local)
		{
			this.transform.Rotate(new Vector3(this.rotationSpeedX, this.rotationSpeedY, this.rotationSpeedZ) * Time.deltaTime, Space.World);
		}
	}

	public virtual void Main()
	{
	}

	public float rotationSpeedX;

	public float rotationSpeedY;

	public float rotationSpeedZ;

	public bool local;

	private Vector3 rotationVector;
}
