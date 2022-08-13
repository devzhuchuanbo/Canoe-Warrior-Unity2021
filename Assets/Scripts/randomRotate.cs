// dnSpy decompiler from Assembly-UnityScript.dll class: randomRotate
using System;
using UnityEngine;

[Serializable]
public class randomRotate : MonoBehaviour
{
	public randomRotate()
	{
		this.rotateEverySecond = (float)1;
	}

	public virtual void Start()
	{
		this.randomRot();
		this.InvokeRepeating("randomRot", (float)0, this.rotateEverySecond);
	}

	public virtual void Update()
	{
		this.transform.rotation = Quaternion.Lerp(this.transform.rotation, this.rotTarget, Time.time * Time.deltaTime);
	}

	public virtual void randomRot()
	{
		this.rotTarget = UnityEngine.Random.rotation;
	}

	public virtual void Main()
	{
	}

	private Quaternion rotTarget;

	public float rotateEverySecond;
}
