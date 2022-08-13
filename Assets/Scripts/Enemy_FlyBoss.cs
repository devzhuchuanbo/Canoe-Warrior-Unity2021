using System;
using UnityEngine;

public class Enemy_FlyBoss : MonoBehaviour
{
	private void Start()
	{
		this.player = GameObject.FindGameObjectWithTag("Player");
		this.PlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<NinjaMovementScript>();
		this.mainEvent = GameObject.FindGameObjectWithTag("MainEventLog").GetComponent<MainEventsLog>();
		base.InvokeRepeating("CheckPlayerDistance", 0.5f, 0.5f);
		this.MySpriteOriginalScale = this.MySpriteOBJ.transform.localScale;
		this.MySpriteOBJ.transform.localScale = new Vector3(-this.MySpriteOriginalScale.x, this.MySpriteOBJ.transform.localScale.y, 1f);
		this.ParticleTrail.emissionRate = 0f;
	}

	private void FixedUpdate()
	{
		base.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		if (this.EnemyAwake && !this.EnemyDead)
		{
			float maxDistanceDelta = this.speed * Time.deltaTime;
			base.transform.position = Vector3.MoveTowards(base.transform.position, this.PlayerScript.transform.position, maxDistanceDelta);
			if (this.MySpriteOBJ.transform.localScale.x > 0f && base.transform.position.x > this.PlayerScript.transform.position.x)
			{
				this.MySpriteOBJ.transform.localScale = new Vector3(-this.MySpriteOriginalScale.x, this.MySpriteOBJ.transform.localScale.y, 1f);
			}
			if (this.MySpriteOBJ.transform.localScale.x < 0f && base.transform.position.x < this.PlayerScript.transform.position.x)
			{
				this.MySpriteOBJ.transform.localScale = new Vector3(this.MySpriteOriginalScale.x, this.MySpriteOBJ.transform.localScale.y, 1f);
			}
		}
		this.AnimatorController.SetBool("Awake", this.EnemyAwake);
		this.AnimatorController.SetBool("Dead", this.EnemyDead);
	}

	private void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Player" && !this.EnemyDead)
		{
			if (coll.contacts[0].normal.x > -1f && coll.contacts[0].normal.x < 1f && coll.contacts[0].normal.y < -0.35f)
			{
				if (this.EnemyDiesAudio != null)
				{
					this.EnemyDiesAudio.Play();
				}
				this.ParticleTrail.emissionRate = 0f;
				coll.rigidbody.AddForce(new Vector2(0f, 1500f));
				base.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, -200f));
				this.EnemyDead = true;
				UnityEngine.Debug.Log("Monster died");
				base.Invoke("iDied", 0.15f);
			}
			else
			{
				this.PlayerScript.NinjaBiMuoiDot();
			}
		}
	}

	private void iDied()
	{
		this.PlayerScript.NinjaKilledEnemy(base.transform.position);
		UnityEngine.Object.Instantiate(this.EnemyChildrent, this.player.transform.position + new Vector3(-2f, -2f, 0f), Quaternion.identity);
		UnityEngine.Object.Instantiate(this.EnemyChildrent, this.player.transform.position + new Vector3(2f, -2f, 0f), Quaternion.identity);
		UnityEngine.Object.Instantiate(this.EnemyChildrent, this.player.transform.position + new Vector3(-2f, 2f, 0f), Quaternion.identity);
		UnityEngine.Object.Instantiate(this.EnemyChildrent, this.player.transform.position + new Vector3(2f, 2f, 0f), Quaternion.identity);
		UnityEngine.Object.Destroy(base.gameObject);
	}

	private void CheckPlayerDistance()
	{
		if (Vector3.Distance(base.transform.position, this.PlayerScript.transform.position) <= this.AwakeDistance && !this.EnemyAwake)
		{
			this.EnemyAwake = true;
			this.ParticleTrail.emissionRate = 15f;
		}
		if (Vector3.Distance(base.transform.position, this.PlayerScript.transform.position) > this.AwakeDistance && this.EnemyAwake)
		{
			this.EnemyAwake = false;
			this.ParticleTrail.emissionRate = 0f;
		}
	}

	public float speed;

	private NinjaMovementScript PlayerScript;

	private MainEventsLog mainEvent;

	private bool EnemyAwake;

	private bool EnemyDead;

	public GameObject EnemyChildrent;

	private float AwakeDistance = 13f;

	public Animator AnimatorController;

	public GameObject MySpriteOBJ;

	private Vector3 MySpriteOriginalScale;

	private GameObject player;

	public ParticleSystem ParticleTrail;

	public AudioSource EnemyDiesAudio;
}
