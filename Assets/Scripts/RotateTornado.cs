// dnSpy decompiler from Assembly-UnityScript.dll class: RotateTornado
using System;
using UnityEngine;

[Serializable]
public class RotateTornado : MonoBehaviour
{
	public RotateTornado()
	{
		this.speed = 30f;
		this.pivot = new Vector3((float)0, (float)50, (float)0);
	}

	public virtual void Update()
	{
		this.transform.RotateAround(this.pivot, Vector3.up, this.speed * Time.deltaTime);
	}

	public virtual void Main()
	{
	}

	public float speed;

	private Vector3 pivot;
}
