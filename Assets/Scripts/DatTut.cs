using System;
using UnityEngine;

public class DatTut : MonoBehaviour
{
	private void Start()
	{
		this.tg = false;
		this.falling = false;
		this.uping = false;
		this.startPos = base.transform.position;
	}

	private void FixedUpdate()
	{
		if (this.falling)
		{
			float maxDistanceDelta = this.speed * Time.deltaTime;
			base.transform.position = Vector3.MoveTowards(base.transform.position, this.EndPos.position, maxDistanceDelta);
		}
		else if (this.uping)
		{
			float maxDistanceDelta2 = this.speed * Time.deltaTime;
			base.transform.position = Vector3.MoveTowards(base.transform.position, this.startPos, maxDistanceDelta2);
		}
	}

	private void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Player" && !this.tg)
		{
			this.tg = true;
			this.uping = false;
			base.Invoke("FallDown", this.TimeToFall);
			base.Invoke("Up", 3f);
		}
	}

	private void Up()
	{
		this.falling = false;
		this.uping = true;
		this.tg = false;
	}

	private void FallDown()
	{
		this.falling = true;
	}

	public float speed;

	public float TimeToFall;

	private bool tg;

	private bool falling;

	private bool uping;

	public Transform EndPos;

	private Vector3 startPos;

	private float step;
}
