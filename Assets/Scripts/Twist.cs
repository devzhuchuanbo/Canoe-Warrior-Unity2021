// dnSpy decompiler from Assembly-UnityScript.dll class: Twist
using System;
using UnityEngine;

[Serializable]
public class Twist : MonoBehaviour
{
	public Twist()
	{
		this.twist = (float)10;
	}

	public virtual void Update()
	{
		this.transform.Rotate(Vector3.back * this.twist * Time.deltaTime);
	}

	public virtual void Main()
	{
	}

	public float twist;
}
