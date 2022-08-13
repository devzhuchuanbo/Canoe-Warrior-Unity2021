// dnSpy decompiler from Assembly-UnityScript.dll class: Red_animSpeedRandomizer
using System;
using UnityEngine;

[Serializable]
public class Red_animSpeedRandomizer : MonoBehaviour
{
	public Red_animSpeedRandomizer()
	{
		this.minSpeed = 0.7f;
		this.maxSpeed = 1.5f;
	}

	public virtual void Start()
	{
		this.GetComponent<Animation>()[this.GetComponent<Animation>().clip.name].speed = UnityEngine.Random.Range(this.minSpeed, this.maxSpeed);
	}

	public virtual void Main()
	{
	}

	public float minSpeed;

	public float maxSpeed;
}
