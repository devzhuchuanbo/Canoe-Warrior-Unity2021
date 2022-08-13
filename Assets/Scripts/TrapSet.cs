using System;
using UnityEngine;

public class TrapSet : MonoBehaviour
{
	private void Start()
	{
		this.danger = false;
		this.player = GameObject.FindGameObjectWithTag("Player");
		this.anim = base.GetComponent<Animator>();
	}

	private void Update()
	{
		if (!this.player)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		else if (this.player.transform.position.x - 23f > base.transform.position.x || this.player.transform.position.x + 23f < base.transform.position.x)
		{
			if (this.active)
			{
				this.active = false;
				this.anim.enabled = false;
			}
		}
		else
		{
			if (!this.active)
			{
				this.active = true;
				this.anim.enabled = true;
			}
			if (this.danger)
			{
				RaycastHit2D hit = Physics2D.Linecast(this.tiaSet.transform.position, this.target.position, this.layer);
				if (hit && hit.collider.tag == "Player")
				{
					hit.collider.gameObject.SendMessage("NinjaBiSetGiat", SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	}

	public void Giat()
	{
		this.danger = true;
		GameObject.FindGameObjectWithTag("MainEventLog").GetComponent<MainEventsLog>().SetGiat(base.transform.position);
	}

	public void StopGiat()
	{
		this.danger = false;
	}

	public Transform tiaSet;

	public Transform target;

	public LayerMask layer;

	private bool danger;

	private GameObject player;

	private bool active = true;

	private Animator anim;
}
