using System;
using UnityEngine;
using UnityEngine.UI;

public class LauncherFPS : MonoBehaviour
{
	private void Start()
	{
		this.tp = base.gameObject.AddComponent<TrajectoryPredictor>();
		this.tp.drawDebugOnPrediction = true;
		this.tp.accuracy = 0.99f;
		this.tp.lineWidth = 0.03f;
		this.tp.iterationLimit = 50;
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			this.launch = true;
		}
		if (UnityEngine.Input.GetKey(KeyCode.R) || UnityEngine.Input.GetKey(KeyCode.UpArrow))
		{
			this.force += this.moveSpeed / 10f;
		}
		if (UnityEngine.Input.GetKey(KeyCode.F) || UnityEngine.Input.GetKey(KeyCode.DownArrow))
		{
			this.force -= this.moveSpeed / 10f;
		}
		this.force = Mathf.Clamp(this.force, 10f, 150f);
		if (this.launch)
		{
			this.launch = false;
			this.Launch();
		}
	}

	private void LateUpdate()
	{
		this.tp.debugLineDuration = Time.unscaledDeltaTime;
		this.tp.Predict3D(this.launchPoint.position, this.launchPoint.forward * this.force, Physics2D.gravity, 0f);
		Vector3 vector = this.tp.hitInfo2D.point;
		if (this.infoText && this.tp.hitInfo3D.collider)
		{
			this.infoText.text = "Hit Object: " + this.tp.hitInfo3D.collider.gameObject.name;
		}
	}

	private void Launch()
	{
		if (!this.launchObjParent)
		{
			this.launchObjParent = new GameObject();
			this.launchObjParent.name = "Launched Objects";
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.objToLaunch);
		gameObject.name = "Ball";
		gameObject.transform.SetParent(this.launchObjParent.transform);
		Rigidbody component = gameObject.GetComponent<Rigidbody>();
		gameObject.transform.position = this.launchPoint.position;
		gameObject.transform.rotation = this.launchPoint.rotation;
		component.velocity = this.launchPoint.forward * this.force;
		Renderer component2 = gameObject.GetComponent<Renderer>();
		component2.material = UnityEngine.Object.Instantiate<Material>(component2.material);
		component2.material.color = this.RandomColor();
	}

	private Color RandomColor()
	{
		float r = UnityEngine.Random.Range(0f, 1f);
		float g = UnityEngine.Random.Range(0f, 1f);
		float b = UnityEngine.Random.Range(0f, 1f);
		return new Color(r, g, b);
	}

	public GameObject objToLaunch;

	public Transform launchPoint;

	public Text infoText;

	public bool launch;

	public float force = 150f;

	public float moveSpeed = 1f;

	private TrajectoryPredictor tp;

	private GameObject launchObjParent;
}
