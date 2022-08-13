using System;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
	private void Start()
	{
		this.StartPosition = base.transform.position;
		this.EndPosition = this.MovePosition.position;
	}

	private void FixedUpdate()
	{
		float maxDistanceDelta = this.speed * Time.deltaTime;
		if (!this.OnTheMove)
		{
			base.transform.position = Vector3.MoveTowards(base.transform.position, this.EndPosition, maxDistanceDelta);
		}
		else
		{
			base.transform.position = Vector3.MoveTowards(base.transform.position, this.StartPosition, maxDistanceDelta);
		}
		if (base.transform.position.x == this.EndPosition.x && base.transform.position.y == this.EndPosition.y && !this.OnTheMove)
		{
			this.OnTheMove = true;
		}
		else if (base.transform.position.x == this.StartPosition.x && base.transform.position.y == this.StartPosition.y && this.OnTheMove)
		{
			this.OnTheMove = false;
		}
	}

	public float speed;

	public Transform MovePosition;

	private Vector3 StartPosition;

	private Vector3 EndPosition;

	private bool OnTheMove;
}
