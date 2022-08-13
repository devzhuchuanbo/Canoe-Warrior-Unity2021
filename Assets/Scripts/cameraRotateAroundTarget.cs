// dnSpy decompiler from Assembly-UnityScript.dll class: cameraRotateAroundTarget
using System;
using UnityEngine;

[AddComponentMenu("Camera-Control/Mouse Orbit")]
[Serializable]
public class cameraRotateAroundTarget : MonoBehaviour
{
	public cameraRotateAroundTarget()
	{
		this.distance = 10f;
		this.xSpeed = 250f;
		this.ySpeed = 120f;
		this.yMinLimit = -20;
		this.yMaxLimit = 80;
		this.zoomRate = 25;
	}

	public virtual void Start()
	{
		Transform transform = GameObject.Find("Camera Target").transform;
		if (transform)
		{
			this.target = transform;
		}
		Vector3 eulerAngles = this.transform.eulerAngles;
		this.x = eulerAngles.y;
		this.y = eulerAngles.x;
	}

	public virtual void Update()
	{
		if (!Input.GetMouseButton(0) && this.target)
		{
			this.x += UnityEngine.Input.GetAxis("Mouse X") * this.xSpeed * Time.deltaTime;
			this.y -= UnityEngine.Input.GetAxis("Mouse Y") * this.ySpeed * Time.deltaTime;
			this.distance += -(UnityEngine.Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime) * (float)this.zoomRate * Mathf.Abs(this.distance);
			this.y = cameraRotateAroundTarget.ClampAngle(this.y, (float)this.yMinLimit, (float)this.yMaxLimit);
			Quaternion rotation = Quaternion.Euler(this.y, this.x, (float)0);
			Vector3 b = new Vector3(this.target.position.x, this.target.position.y + this.yOffset, this.target.position.z);
			Vector3 position = rotation * new Vector3((float)0, (float)0, -this.distance) + b;
			this.transform.rotation = rotation;
			this.transform.position = position;
		}
	}

	public static float ClampAngle(float angle, float min, float max)
	{
		if (angle < (float)-360)
		{
			angle += (float)360;
		}
		if (angle > (float)360)
		{
			angle -= (float)360;
		}
		return Mathf.Clamp(angle, min, max);
	}

	public virtual void Main()
	{
	}

	public Transform target;

	public float distance;

	public float xSpeed;

	public float ySpeed;

	public int yMinLimit;

	public int yMaxLimit;

	public int zoomRate;

	public float yOffset;

	private float x;

	private float y;
}
