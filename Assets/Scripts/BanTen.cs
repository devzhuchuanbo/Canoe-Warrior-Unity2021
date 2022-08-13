using System;
using System.Collections;
using UnityEngine;

public class BanTen : MonoBehaviour
{
	private void Start()
	{
		this.player = GameObject.FindGameObjectWithTag("Player").transform;
		this.waiting = true;
		this.alive = true;
		this.anim = base.gameObject.GetComponent<Animator>();
		Vector3 vector = this.endPoint.position - this.startPoint.position;
		this.dir = new Vector2(vector.x, vector.y);
	}

	private void Update()
	{
		if (this.alive && this.waiting && this.waiting)
		{
			RaycastHit2D hit = Physics2D.Raycast(this.startPoint.position, this.dir, this.maxDistance, this.layer);
			if (hit && hit.collider.tag == "Player")
			{
				this.waiting = false;
				this.anim.SetTrigger("ban");
			}
		}
	}

	public void ReadyToFire()
	{
		this.audioS.clip = this.readyA;
		this.audioS.Play();
	}

	public void FireOneShot()
	{
		this.audioS.clip = this.fireA;
		this.audioS.Play();
		RaycastHit2D hit = Physics2D.Raycast(this.startPoint.position, this.dir, this.maxDistance, this.layer);
		if (hit && hit.collider.tag == "Player")
		{
			hit.collider.gameObject.SendMessage("NinjaDies", SendMessageOptions.DontRequireReceiver);
		}
		base.StartCoroutine(this.WaitCoolDown());
	}

	private IEnumerator WaitCoolDown()
	{
		yield return new WaitForSeconds(this.cooldown);
		this.waiting = true;
		yield break;
	}

	private void die(Vector2 p)
	{
		if (this.alive)
		{
			this.alive = false;
			this.audioS.clip = this.dieA;
			this.audioS.Play();
			this.maufun.gameObject.SetActive(true);
			this.anim.SetTrigger("die");
		}
	}

	private void Hit(Vector2 point)
	{
		this.die(point);
	}

	public GameObject muiTen;

	public Transform startPoint;

	public Transform endPoint;

	public float maxDistance;

	public float cooldown;

	public AudioSource audioS;

	public AudioClip fireA;

	public AudioClip readyA;

	public AudioClip dieA;

	private Vector2 dir;

	private bool waiting;

	private bool alive;

	private Animator anim;

	public GameObject mauFi;

	public LayerMask layer;

	public Transform maufun;

	private Transform player;
}
