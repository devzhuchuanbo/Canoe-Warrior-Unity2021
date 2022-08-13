using System;
using UnityEngine;

public class TrapMay : MonoBehaviour
{
	private void Start()
	{
		this.danger = false;
		this.anim = base.GetComponent<Animator>();
		this.anim.enabled = false;
		this.player = GameObject.FindGameObjectWithTag("Player");
		base.Invoke("StartDelay", this.delay);
		if (!this.player)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void Update()
	{
		if (this.danger)
		{
			RaycastHit2D hit = Physics2D.Raycast(this.StartPoint.position, Vector2.down);
			if (hit && hit.collider.tag == "Player")
			{
				hit.collider.gameObject.SendMessage("NinjaBiSetGiat");
			}
		}
	}

	public void Giat()
	{
		if (this.player.transform.position.x - 20f <= base.transform.position.x && this.player.transform.position.x + 20f >= base.transform.position.x)
		{
			this.danger = true;
			GameObject.FindGameObjectWithTag("MainEventLog").GetComponent<MainEventsLog>().SetGiat(base.transform.position);
		}
	}

	public void StopGiat()
	{
		this.danger = false;
	}

	private void StartDelay()
	{
		this.anim.enabled = true;
	}

	public Transform StartPoint;

	public float delay;

	private bool danger;

	private Animator anim;

	private GameObject player;
}
