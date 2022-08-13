using System;
using UnityEngine;

public class OrochiSoul : MonoBehaviour
{
	private void Start()
	{
		this.player = GameObject.FindGameObjectWithTag("Player");
		this.PlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<NinjaMovementScript>();
		this.mainEvent = GameObject.FindGameObjectWithTag("MainEventLog").GetComponent<MainEventsLog>();
		base.InvokeRepeating("CheckPlayerDistance", 0.5f, 0.5f);
		this.maxHp = this.hp;
	}

	private void FixedUpdate()
	{
		base.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		if (!this.PlayerScript.isDieing)
		{
			if (this.EnemyAwake && !this.EnemyDead)
			{
				float maxDistanceDelta = this.speed * Time.deltaTime;
				base.transform.position = Vector3.MoveTowards(base.transform.position, this.PlayerScript.transform.position, maxDistanceDelta);
			}
		}
		else
		{
			float maxDistanceDelta2 = this.speed * Time.deltaTime * 2f;
			base.transform.position = Vector3.MoveTowards(base.transform.position, this.diePos, maxDistanceDelta2);
		}
	}

	private void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Player" && !this.EnemyDead && !this.PlayerScript.dashing)
		{
			this.PlayerScript.NinjaBiMuoiDot();
		}
	}

	public void iDied()
	{
		base.Invoke("Respaw", this.timeToRespaw);
		this.EnemyDead = true;
		this.eff.gameObject.SetActive(false);
		base.transform.position = new Vector3(100f, 100f, 0f);
	}

	private void Respaw()
	{
		this.boss.Summon();
	}

	private void Hit()
	{
		if (this.hp > 1)
		{
			this.hp--;
		}
		else
		{
			this.EnemyDead = true;
			this.iDied();
		}
	}

	public void Summon(Vector3 p)
	{
		base.transform.position = p;
		this.EnemyDead = false;
		this.hp = this.maxHp;
		this.eff.gameObject.SetActive(true);
	}

	private void DashOver()
	{
		if (!this.EnemyDead)
		{
			this.EnemyDead = true;
			GameObject.FindGameObjectWithTag("Effect").GetComponent<EffectController>().ToeMauXanh(base.gameObject.transform.position);
			this.iDied();
		}
	}

	private void CheckPlayerDistance()
	{
		if (Vector3.Distance(base.transform.position, this.PlayerScript.transform.position) <= this.AwakeDistance && !this.EnemyAwake)
		{
			this.EnemyAwake = true;
		}
		if (Vector3.Distance(base.transform.position, this.PlayerScript.transform.position) > this.AwakeDistance && this.EnemyAwake)
		{
			this.EnemyAwake = false;
		}
	}

	public float speed;

	public float timeToRespaw;

	private NinjaMovementScript PlayerScript;

	private MainEventsLog mainEvent;

	private bool EnemyAwake;

	private bool EnemyDead = true;

	private float AwakeDistance = 20f;

	private Vector3 MySpriteOriginalScale;

	private GameObject player;

	public int hp = 10;

	private int maxHp;

	public Transform eff;

	private Vector3 diePos = new Vector3(3f, 4f, 0f);

	public OrochiBoss boss;
}
