//// dnSpy decompiler from Assembly-UnityScript.dll class: RFX4_MouseOrbit
//using System;
//using Boo.Lang.Runtime;
//using UnityEngine;

//[AddComponentMenu("Camera-Control/Mouse Orbit")]
//[Serializable]
//public class RFX4_MouseOrbit : MonoBehaviour
//{
//	public RFX4_MouseOrbit()
//	{
//		this.distance = 10f;
//		this.xSpeed = 250f;
//		this.ySpeed = 120f;
//		this.yMinLimit = -20;
//		this.yMaxLimit = 80;
//	}

//	public virtual void Start()
//	{
//		Vector3 eulerAngles = this.transform.eulerAngles;
//		this.x = eulerAngles.y;
//		this.y = eulerAngles.x;
//		if (this.GetComponent<Rigidbody>())
//		{
//			this.GetComponent<Rigidbody>().freezeRotation = true;
//		}
//	}

//	public virtual void LateUpdate()
//	{
//		if (this.distance < (float)2)
//		{
//			this.distance = (float)2;
//		}
//		this.distance -= UnityEngine.Input.GetAxis("Mouse ScrollWheel") * (float)2;
//		if (this.target && (Input.GetMouseButton(0) || Input.GetMouseButton(1)))
//		{
//			Vector3 mousePosition = UnityEngine.Input.mousePosition;
//			if (Screen.dpi < (float)1)
//			{
//			}
//			int num;
//			if (Screen.dpi < (float)200)
//			{
//				num = 1;
//			}
//			else
//			{
//				num = (int)(Screen.dpi / 200f);
//			}
//			if (mousePosition.x < (float)(380 * num) && (float)Screen.height - mousePosition.y < (float)(250 * num))
//			{
//				return;
//			}
//			Cursor.visible = false;
//			Cursor.lockState = CursorLockMode.Locked;
//			this.x += UnityEngine.Input.GetAxis("Mouse X") * this.xSpeed * 0.02f;
//			this.y -= UnityEngine.Input.GetAxis("Mouse Y") * this.ySpeed * 0.02f;
//			this.y = RFX4_MouseOrbit.ClampAngle(this.y, (float)this.yMinLimit, (float)this.yMaxLimit);
//			Quaternion rotation = Quaternion.Euler(this.y, this.x, (float)0);
//			Vector3 position = rotation * new Vector3((float)0, (float)0, -this.distance) + this.target.position;
//			this.transform.rotation = rotation;
//			this.transform.position = position;
//		}
//		else
//		{
//			Cursor.visible = true;
//			Cursor.lockState = CursorLockMode.None;
//		}
//		if (!RuntimeServices.EqualityOperator(this.prevDistance, this.distance))
//		{
//			this.prevDistance = this.distance;
//			Quaternion rotation2 = Quaternion.Euler(this.y, this.x, (float)0);
//			Vector3 position2 = rotation2 * new Vector3((float)0, (float)0, -this.distance) + this.target.position;
//			this.transform.rotation = rotation2;
//			this.transform.position = position2;
//		}
//	}

//	public static float ClampAngle(float angle, float min, float max)
//	{
//		if (angle < (float)-360)
//		{
//			angle += (float)360;
//		}
//		if (angle > (float)360)
//		{
//			angle -= (float)360;
//		}
//		return Mathf.Clamp(angle, min, max);
//	}

//	public virtual void Main()
//	{
//	}

//	public Transform target;

//	public float distance;

//	public float xSpeed;

//	public float ySpeed;

//	public int yMinLimit;

//	public int yMaxLimit;

//	private float x;

//	private float y;

//	public object prevDistance;
//}
