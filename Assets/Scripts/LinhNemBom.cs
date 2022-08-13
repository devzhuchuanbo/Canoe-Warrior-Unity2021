using System;
using UnityEngine;

public class LinhNemBom : MonoBehaviour
{
	private void Start()
	{
		this.player = GameObject.FindGameObjectWithTag("Player").transform;
		this.PlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<NinjaMovementScript>();
		this.mainEvent = GameObject.FindGameObjectWithTag("MainEventLog").GetComponent<MainEventsLog>();
		this.crit = this.mainEvent.crit;
		this.tp = base.gameObject.AddComponent<TrajectoryPredictor>();
		this.tp.predictionType = TrajectoryPredictor.predictionMode.Prediction2D;
		this.tp.drawDebugOnPrediction = false;
		this.tp.accuracy = 0.95f;
		this.tp.lineWidth = 0.025f;
		this.tp.iterationLimit = 150;
		this.xMax = this.outL.position.x;
		if (this.nhinTrai)
		{
			this.startPoint.transform.rotation = Quaternion.Euler(0f, 0f, 180f - this.gocban);
		}
		else
		{
			this.startPoint.transform.rotation = Quaternion.Euler(0f, 0f, this.gocban);
		}
	}

	private void Update()
	{
		if (!this.EnemyDead)
		{
			if (this.player.transform.position.x - 35f > base.transform.position.x || this.player.transform.position.x + 35f < base.transform.position.x)
			{
				if (this.active)
				{
					this.active = false;
					this.anim.enabled = false;
					this.anim.gameObject.SetActive(false);
				}
			}
			else
			{
				if (!this.active)
				{
					this.active = true;
					this.anim.gameObject.SetActive(true);
					this.anim.enabled = true;
				}
				if (this.nhinTrai)
				{
					if (this.player.position.x > this.xMax && this.player.position.x < this.startPoint.position.x - 1f && this.player.position.y < 15f && Time.time > this.nemTime + this.nemDelay)
					{
						this.anim.SetTrigger("Nem");
						this.nemTime = Time.time;
					}
				}
				else if (this.player.position.x < this.xMax && this.player.position.x > this.startPoint.position.x - 1f && this.player.position.y < 15f && Time.time > this.nemTime + this.nemDelay)
				{
					this.anim.SetTrigger("Nem");
					this.nemTime = Time.time;
				}
			}
		}
	}

	public void NemBom()
	{
		this.Nem();
	}

	public void Nem()
	{
		this.force = 0f;
		float x = this.startPoint.position.x;
		if (this.nhinTrai)
		{
			while (x > this.xMax && x > this.player.position.x && this.force < 12f)
			{
				this.force += 0.5f;
				this.tp.debugLineDuration = Time.unscaledDeltaTime;
				this.tp.Predict2D(this.startPoint.position, this.startPoint.right * this.force, Physics2D.gravity / 11f, 0f);
				x = this.tp.hitInfo2D.point.x;
			}
			GameObject gameObject = UnityEngine.Object.Instantiate(this.quaBomTrai, this.startPoint.position, Quaternion.identity) as GameObject;
			gameObject.GetComponent<Rigidbody2D>().velocity = this.startPoint.right * this.force;
		}
		else
		{
			while (x < this.xMax && x < this.player.position.x && this.force < 12f)
			{
				this.force += 0.5f;
				this.tp.debugLineDuration = Time.unscaledDeltaTime;
				this.tp.Predict2D(this.startPoint.position, this.startPoint.right * this.force, Physics2D.gravity / 9f, 0f);
				x = this.tp.hitInfo2D.point.x;
			}
			GameObject gameObject2 = UnityEngine.Object.Instantiate(this.quaBomPhai, this.startPoint.position, Quaternion.identity) as GameObject;
			gameObject2.GetComponent<Rigidbody2D>().velocity = this.startPoint.right * this.force;
		}
	}

	private void iDied()
	{
		this.PlayerScript.NinjaKilledEnemy(base.transform.position);
		this.anim.SetTrigger("Die");
		Collider2D[] components = base.GetComponents<Collider2D>();
		foreach (Collider2D collider2D in components)
		{
			collider2D.enabled = false;
		}
		this.maufun.gameObject.SetActive(true);
		UnityEngine.Object.Destroy(this.maufun.gameObject, 7f);
		UnityEngine.Object.Destroy(base.gameObject, 15f);
	}

	private void Hit(Vector2 p)
	{
		if (!this.EnemyDead)
		{
			int num = UnityEngine.Random.Range(0, 99);
			if (num <= this.crit)
			{
				this.EnemyDead = true;
				this.iDied();
				this.mainEvent.CritHUD(base.transform.position);
			}
			else if (this.hp > 1)
			{
				this.hp--;
			}
			else
			{
				this.EnemyDead = true;
				this.iDied();
			}
		}
	}

	private void Hit2(Vector2 p)
	{
		if (!this.EnemyDead)
		{
			this.EnemyDead = true;
			this.iDied();
		}
	}

	private void DashOver()
	{
		if (!this.EnemyDead)
		{
			this.EnemyDead = true;
			GameObject.FindGameObjectWithTag("Effect").GetComponent<EffectController>().ToeMau(base.gameObject.transform.position);
			this.iDied();
		}
	}

	private void Exp()
	{
		if (!this.EnemyDead)
		{
			this.EnemyDead = true;
			GameObject.FindGameObjectWithTag("Effect").GetComponent<EffectController>().ToeMau(base.gameObject.transform.position);
			this.iDied();
		}
	}

	public int hp;

	public Transform startPoint;

	public Transform outL;

	public Animator anim;

	public Transform maufun;

	public bool nhinTrai = true;

	public float nemDelay;

	public GameObject quaBomTrai;

	public GameObject quaBomPhai;

	public float gocban;

	public float force;

	private Transform player;

	private float nemTime;

	private TrajectoryPredictor tp;

	private float xMax;

	private bool active = true;

	private NinjaMovementScript PlayerScript;

	private MainEventsLog mainEvent;

	private int crit;

	private bool EnemyDead;
}
