using System;
using UnityEngine;
using UnityEngine.UI;

public class Launcher2D : MonoBehaviour
{
	private void Start()
	{
		this.tp = base.gameObject.AddComponent<TrajectoryPredictor>();
		this.tp.predictionType = TrajectoryPredictor.predictionMode.Prediction2D;
		this.tp.drawDebugOnPrediction = true;
		this.tp.accuracy = 0.99f;
		this.tp.lineWidth = 0.025f;
		this.tp.iterationLimit = 300;
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.T))
		{
			this.launch = true;
		}
		if (UnityEngine.Input.GetKey(KeyCode.Y))
		{
			this.launch = true;
		}
		if (UnityEngine.Input.GetKey(KeyCode.A) || UnityEngine.Input.GetKey(KeyCode.LeftArrow))
		{
			base.transform.Rotate(new Vector3(0f, 0f, this.moveSpeed));
		}
		if (UnityEngine.Input.GetKey(KeyCode.D) || UnityEngine.Input.GetKey(KeyCode.RightArrow))
		{
			base.transform.Rotate(new Vector3(0f, 0f, -this.moveSpeed));
		}
		if (UnityEngine.Input.GetKey(KeyCode.W) || UnityEngine.Input.GetKey(KeyCode.UpArrow))
		{
			this.force += this.moveSpeed / 10f;
		}
		if (UnityEngine.Input.GetKey(KeyCode.S) || UnityEngine.Input.GetKey(KeyCode.DownArrow))
		{
			this.force -= this.moveSpeed / 10f;
		}
		this.force = Mathf.Clamp(this.force, 1f, 20f);
		if (this.launch)
		{
			this.launch = false;
			this.Launch();
		}
		this.tp.debugLineDuration = Time.unscaledDeltaTime;
		this.tp.Predict2D(this.launchPoint.position, this.launchPoint.right * this.force, Physics2D.gravity, 0f);
		if (this.infoText && this.tp.hitInfo2D)
		{
			this.infoText.text = "Hit Object: " + this.tp.hitInfo2D.collider.gameObject.name;
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
		Rigidbody2D component = gameObject.GetComponent<Rigidbody2D>();
		gameObject.transform.position = this.launchPoint.position;
		gameObject.transform.rotation = this.launchPoint.rotation;
		component.velocity = this.launchPoint.right * this.force;
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
