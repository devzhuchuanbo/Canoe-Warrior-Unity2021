using System;
using UnityEngine;

namespace TrajectoryExample
{
	[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
	public class MouseOrbit : MonoBehaviour
	{
		private void Start()
		{
			Vector3 eulerAngles = base.transform.eulerAngles;
			this.x = eulerAngles.y;
			this.y = eulerAngles.x;
			if (base.GetComponent<Rigidbody>())
			{
				base.GetComponent<Rigidbody>().freezeRotation = true;
			}
		}

		private void Update()
		{
			this.prevRealTime = this.thisRealTime;
			this.thisRealTime = Time.realtimeSinceStartup;
			if (Input.GetMouseButtonDown(2))
			{
				Ray ray = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);
				RaycastHit raycastHit;
				if (Physics.Raycast(ray, out raycastHit) && raycastHit.collider.transform.parent && raycastHit.collider.transform.parent.name == "Targets")
				{
					this.target = raycastHit.collider.transform;
				}
			}
		}

		public float deltaTime
		{
			get
			{
				if (Time.timeScale > 0f)
				{
					return Time.deltaTime / Time.timeScale;
				}
				return Time.realtimeSinceStartup - this.prevRealTime;
			}
		}

		private void LateUpdate()
		{
			if (this.target)
			{
				if (Input.GetMouseButton(1))
				{
					this.x += UnityEngine.Input.GetAxis("Mouse X") * this.xSpeed * this.distance * 0.02f;
					this.y -= UnityEngine.Input.GetAxis("Mouse Y") * this.ySpeed * 0.02f;
					this.y = MouseOrbit.ClampAngle(this.y, this.yMinLimit, this.yMaxLimit);
				}
				Quaternion quaternion = Quaternion.Euler(this.y, this.x, 0f);
				this.distance = Mathf.Clamp(this.distance - UnityEngine.Input.GetAxis("Mouse ScrollWheel") * 5f, this.distanceMin, this.distanceMax);
				Vector3 point = new Vector3(0f, 0f, -this.distance);
				Vector3 b = quaternion * point + this.target.position;
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, quaternion, this.deltaTime * this.smoothSpeed);
				base.transform.position = Vector3.Lerp(base.transform.position, b, this.deltaTime * this.smoothSpeed);
			}
		}

		public static float ClampAngle(float angle, float min, float max)
		{
			if (angle < -360f)
			{
				angle += 360f;
			}
			if (angle > 360f)
			{
				angle -= 360f;
			}
			return Mathf.Clamp(angle, min, max);
		}

		public Transform target;

		public float distance = 5f;

		public float xSpeed = 120f;

		public float ySpeed = 120f;

		public float smoothSpeed = 16f;

		public float yMinLimit = -20f;

		public float yMaxLimit = 80f;

		public float distanceMin = 0.5f;

		public float distanceMax = 15f;

		private float x;

		private float y;

		private float prevRealTime;

		private float thisRealTime;
	}
}
