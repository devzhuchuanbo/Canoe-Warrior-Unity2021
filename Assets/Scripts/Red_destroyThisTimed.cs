// dnSpy decompiler from Assembly-UnityScript.dll class: Red_destroyThisTimed
using System;
using UnityEngine;

[Serializable]
public class Red_destroyThisTimed : MonoBehaviour
{
	public Red_destroyThisTimed()
	{
		this.destroyTime = (float)5;
	}

	public virtual void Start()
	{
		UnityEngine.Object.Destroy(this.gameObject, this.destroyTime);
	}

	public virtual void Update()
	{
	}

	public virtual void Main()
	{
	}

	public float destroyTime;
}
