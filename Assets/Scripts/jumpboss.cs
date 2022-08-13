using System;
using UnityEngine;

public class jumpboss : MonoBehaviour
{
	private void Start()
	{
		this.tp = base.gameObject.AddComponent<TrajectoryPredictor>();
		this.tp.predictionType = TrajectoryPredictor.predictionMode.Prediction2D;
		this.tp.drawDebugOnPrediction = false;
		this.tp.accuracy = 0.99f;
		this.tp.lineWidth = 0.025f;
		this.tp.iterationLimit = 300;
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.T))
		{
			this.Launch();
		}
	}

	private void Launch()
	{
		Rigidbody2D component = base.gameObject.GetComponent<Rigidbody2D>();
		component.velocity = Vector3.zero;
		if (this.target.transform.position.x < base.transform.position.x)
		{
			this.dir.transform.rotation = Quaternion.Euler(0f, 0f, 180f - this.gocban);
		}
		else
		{
			this.dir.transform.rotation = Quaternion.Euler(0f, 0f, this.gocban);
		}
		component.velocity = this.dir.right * this.force;
	}

	public Transform target;

	public float gocban;

	public Transform dir;

	public float force;

	public float minfoce;

	public float maxforce;

	public float minstep;

	public float maxstep;

	private TrajectoryPredictor tp;
}
