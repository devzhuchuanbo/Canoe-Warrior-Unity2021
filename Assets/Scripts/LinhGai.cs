using System;
using UnityEngine;

public class LinhGai : MonoBehaviour
{
	private void Start()
	{
		this.player = GameObject.FindGameObjectWithTag("Player").transform;
		this.PlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<NinjaMovementScript>();
		this.mainEvent = GameObject.FindGameObjectWithTag("MainEventLog").GetComponent<MainEventsLog>();
		this.crit = this.mainEvent.crit - 30;
		this.target = this.boderInL;
		this.MySpriteOriginalScale = this.MySpriteOBJ.transform.localScale;
		this.MySpriteOBJ.transform.localScale = new Vector3(this.MySpriteOriginalScale.x, this.MySpriteOBJ.transform.localScale.y, 1f);
		this.dir = Vector2.right;
		this.anim.SetBool("Up", true);
		this.speedtg = this.speed;
		this.speedFollowtg = this.speedFollow;
	}

	private void FixedUpdate()
	{
		if (!this.EnemyDead)
		{
			if (this.player.transform.position.x - 35f > base.transform.position.x || this.player.transform.position.x + 35f < base.transform.position.x)
			{
				if (this.active)
				{
					this.active = false;
					base.GetComponent<Rigidbody2D>().isKinematic = true;
					this.up = true;
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
					if (this.up)
					{
						this.anim.SetBool("Up", true);
					}
					else
					{
						this.anim.SetBool("Up", false);
					}
					base.GetComponent<Rigidbody2D>().isKinematic = false;
				}
				base.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				if (!this.PlayerScript.isTransing)
				{
					if (!this.attacking)
					{
						if (this.player.position.x > this.boderOutL.position.x && this.player.position.x < this.boderOutR.position.x && this.player.position.y < 30f)
						{
							if (!this.canSeePlayer)
							{
								RaycastHit2D hit = Physics2D.Raycast(this.startPoint.position, this.dir, this.watchDistance, this.layer);
								if (hit && hit.collider.tag == "Player")
								{
									this.canSeePlayer = true;
									this.anim.SetBool("Follow", true);
								}
								this.DiTuan();
							}
							else
							{
								this.DiXien();
								if (Physics2D.Linecast(this.startPoint.position, this.player.transform.position, this.layer).collider.tag != "Player")
								{
									this.canSeePlayer = false;
									this.anim.SetBool("Follow", false);
								}
							}
						}
						else if (this.canSeePlayer)
						{
							this.anim.SetBool("Follow", false);
							this.canSeePlayer = false;
							this.DiTuan();
						}
						else
						{
							this.DiTuan();
						}
					}
				}
				else if (this.canSeePlayer)
				{
					this.anim.SetBool("Follow", false);
					this.canSeePlayer = false;
					this.DiTuan();
				}
				else
				{
					this.DiTuan();
				}
			}
		}
	}

	private void HaKhien()
	{
		if (this.up)
		{
			this.speed = 0f;
			this.speedFollow = 0f;
			this.anim.SetBool("Up", false);
			this.anim.SetTrigger("Action");
			this.up = false;
			base.Invoke("DiTiep", 1f);
		}
	}

	private void NangKhien()
	{
		base.Invoke("NangSauTime", 1f);
	}

	private void NangSauTime()
	{
		if (!this.up)
		{
			this.speed = 0f;
			this.speedFollow = 0f;
			this.anim.SetBool("Up", true);
			this.anim.SetTrigger("Action");
			this.up = true;
			base.Invoke("DiTiep", 1.2f);
		}
	}

	private void DiTiep()
	{
		this.speed = this.speedtg;
		this.speedFollow = this.speedFollowtg;
	}

	private void DiXien()
	{
		if (this.up)
		{
			this.HaKhien();
		}
		if (this.speak)
		{
			this.mainEvent.EnemyXienKK();
			this.speak = false;
		}
		float maxDistanceDelta = this.speedFollow * Time.deltaTime;
		Vector3 vector = new Vector3(this.target.position.x, base.transform.position.y, base.transform.position.z);
		base.transform.position = Vector3.MoveTowards(base.transform.position, vector, maxDistanceDelta);
		if (this.target == this.boderOutL)
		{
			if (base.transform.position.x <= this.boderOutL.position.x)
			{
				this.target = this.boderOutR;
			}
		}
		else if (this.target == this.boderOutR)
		{
			if (base.transform.position.x >= this.boderOutR.position.x)
			{
				this.target = this.boderOutL;
			}
		}
		else if (base.transform.position.x <= this.boderOutL.position.x)
		{
			this.target = this.boderOutR;
		}
		else if (base.transform.position.x >= this.boderOutR.position.x)
		{
			this.target = this.boderOutL;
		}
		else if (this.MySpriteOBJ.transform.localScale.x > 0f)
		{
			this.target = this.boderOutR;
		}
		else
		{
			this.target = this.boderOutL;
		}
		if (this.MySpriteOBJ.transform.localScale.x > 0f && base.transform.position.x > this.target.position.x)
		{
			this.MySpriteOBJ.transform.localScale = new Vector3(-this.MySpriteOriginalScale.x, this.MySpriteOBJ.transform.localScale.y, 1f);
			this.dir = Vector2.left;
		}
		if (this.MySpriteOBJ.transform.localScale.x < 0f && base.transform.position.x < this.target.position.x)
		{
			this.MySpriteOBJ.transform.localScale = new Vector3(this.MySpriteOriginalScale.x, this.MySpriteOBJ.transform.localScale.y, 1f);
			this.dir = Vector2.right;
		}
		RaycastHit2D hit = Physics2D.Raycast(this.startPoint.position, this.dir, this.attackDistance, this.layer);
		if (hit && hit.collider.tag == "Player" && !this.PlayerScript.BVed)
		{
			GameObject.FindGameObjectWithTag("Effect").GetComponent<EffectController>().ToeMau(hit.point);
			this.PlayerScript.NinjaBiXien();
		}
	}

	private void DiTuan()
	{
		float maxDistanceDelta = this.speed * Time.deltaTime;
		Vector3 vector = new Vector3(this.target.position.x, base.transform.position.y, base.transform.position.z);
		base.transform.position = Vector3.MoveTowards(base.transform.position, vector, maxDistanceDelta);
		if (this.target == this.boderInL)
		{
			if (base.transform.position.x <= this.boderInL.position.x)
			{
				this.target = this.boderInR;
			}
		}
		else if (this.target == this.boderInR)
		{
			if (base.transform.position.x >= this.boderInR.position.x)
			{
				this.target = this.boderInL;
			}
		}
		else if (base.transform.position.x <= this.boderInL.position.x)
		{
			this.target = this.boderInR;
		}
		else if (base.transform.position.x >= this.boderInR.position.x)
		{
			this.target = this.boderInL;
		}
		else if (this.MySpriteOBJ.transform.localScale.x > 0f)
		{
			this.target = this.boderInR;
		}
		else
		{
			this.target = this.boderInL;
		}
		if (this.MySpriteOBJ.transform.localScale.x > 0f && base.transform.position.x > this.target.position.x)
		{
			this.MySpriteOBJ.transform.localScale = new Vector3(-this.MySpriteOriginalScale.x, this.MySpriteOBJ.transform.localScale.y, 1f);
			this.dir = Vector2.left;
		}
		if (this.MySpriteOBJ.transform.localScale.x < 0f && base.transform.position.x < this.target.position.x)
		{
			this.MySpriteOBJ.transform.localScale = new Vector3(this.MySpriteOriginalScale.x, this.MySpriteOBJ.transform.localScale.y, 1f);
			this.dir = Vector2.right;
		}
	}

	private void iDied()
	{
		this.PlayerScript.NinjaKilledEnemyNoRw(base.transform.position);
		this.anim.SetBool("Died", true);
		this.anim.SetTrigger("Die");
		base.GetComponent<Rigidbody2D>().gravityScale = 0f;
		Collider2D[] components = base.GetComponents<Collider2D>();
		foreach (Collider2D collider2D in components)
		{
			collider2D.enabled = false;
		}
		base.Invoke("StopAni", 2.5f);
		foreach (Transform transform in this.bodyDestroy)
		{
			UnityEngine.Object.Destroy(transform.gameObject, 3f);
		}
	}

	private void StopAni()
	{
		this.anim.enabled = false;
		base.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
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
				if (this.up)
				{
					this.HaKhien();
				}
				if (this.player.transform.position.x < base.transform.position.x)
				{
					if (!this.canSeePlayer)
					{
						this.target = this.boderInL;
					}
					else
					{
						this.target = this.boderOutL;
					}
				}
				else if (!this.canSeePlayer)
				{
					this.target = this.boderInR;
				}
				else
				{
					this.target = this.boderOutR;
				}
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

	public Transform boderInL;

	public Transform boderInR;

	public Transform boderOutL;

	public Transform boderOutR;

	public float speed;

	public float speedFollow;

	public int hp;

	public float watchDistance;

	public float attackDistance;

	private NinjaMovementScript PlayerScript;

	private MainEventsLog mainEvent;

	private bool EnemyDead;

	private bool canSeePlayer;

	private bool attacking;

	public Transform startPoint;

	private float speedtg;

	private float speedFollowtg;

	public Animator anim;

	public GameObject MySpriteOBJ;

	public Transform[] bodyDestroy;

	private Vector3 MySpriteOriginalScale;

	private Transform player;

	private Transform target;

	private Vector2 dir;

	public LayerMask layer;

	private int crit;

	private bool speak = true;

	private bool active = true;

	private bool up = true;
}
