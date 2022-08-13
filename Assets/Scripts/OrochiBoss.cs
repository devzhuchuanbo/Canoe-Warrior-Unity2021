using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OrochiBoss : MonoBehaviour
{
	private void Start()
	{
		this.player = GameObject.FindGameObjectWithTag("Player").transform;
		this.PlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<NinjaMovementScript>();
		this.mainEvent = GameObject.FindGameObjectWithTag("MainEventLog").GetComponent<MainEventsLog>();
		this.target = this.boderInL;
		this.MySpriteOriginalScale = this.MySpriteOBJ.transform.localScale;
		this.MySpriteOBJ.transform.localScale = new Vector3(this.MySpriteOriginalScale.x, this.MySpriteOBJ.transform.localScale.y, 1f);
		this.dir = Vector2.right;
		this.timeToBeDash = 0f;
		this.maxHP = this.hp;
		this.hpcu = this.hp;
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

	private void FixedUpdate()
	{
		if (!this.EnemyDead)
		{
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

	private void DiXien()
	{
		this.target = this.player;
		if (this.startTrans && Time.time > this.maxTimeWait + this.timeWait)
		{
			this.attackStyle = 2;
		}
		float maxDistanceDelta = this.speedFollow * Time.deltaTime;
		Vector3 vector = new Vector3(this.target.position.x, base.transform.position.y, base.transform.position.z);
		base.transform.position = Vector3.MoveTowards(base.transform.position, vector, maxDistanceDelta);
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
		RaycastHit2D hit = Physics2D.Raycast(this.startPoint.position, this.dir, this.attackDistance[this.attackStyle] - 1f, this.layer);
		if (hit && hit.collider.tag == "Player")
		{
			if (this.attackStyle == 0)
			{
				this.attacking = true;
				this.anim.SetInteger("AttackStyle", 0);
				this.anim.SetTrigger("Attack");
				base.StartCoroutine(this.AttackCoolDown(this.attackingTime[0]));
			}
			else if (this.attackStyle == 1)
			{
				this.attacking = true;
				this.anim.SetInteger("AttackStyle", 1);
				this.anim.SetTrigger("Attack");
				if (this.dir == Vector2.left)
				{
					this.jumpDir.transform.rotation = Quaternion.Euler(0f, 0f, 180f - this.gocBan);
				}
				else
				{
					this.jumpDir.transform.rotation = Quaternion.Euler(0f, 0f, this.gocBan);
				}
				base.GetComponent<Rigidbody2D>().velocity = this.jumpDir.right * this.force;
				base.StartCoroutine(this.AttackCoolDown(this.attackingTime[1]));
				this.skill2nhay.Play();
			}
			else if (this.attackStyle == 2)
			{
				this.attacking = true;
				if (UnityEngine.Random.Range(0, 2) == 0)
				{
					this.newpos = new Vector3(this.player.transform.position.x + 2.5f, base.transform.position.y, base.transform.position.z);
					if (this.newpos.x < this.boderOutL.position.x || this.newpos.x > this.boderOutR.position.x)
					{
						this.newpos = new Vector3(this.player.transform.position.x, base.transform.position.y, base.transform.position.z);
					}
				}
				else
				{
					this.newpos = new Vector3(this.player.transform.position.x - 2.5f, base.transform.position.y, base.transform.position.z);
					if (this.newpos.x < this.boderOutL.position.x || this.newpos.x > this.boderOutR.position.x)
					{
						this.newpos = new Vector3(this.player.transform.position.x, base.transform.position.y, base.transform.position.z);
					}
				}
				this.TeleOut.transform.position = base.transform.position;
				this.TeleOut.Play();
				this.skill3out.Play();
				base.Invoke("TransOut", 0.2f);
			}
			else if (this.attackStyle == 3)
			{
				this.skillEff.SetActive(false);
				this.attacking = true;
				this.ol = base.gameObject.transform.position;
				base.gameObject.transform.position += new Vector3(0f, 100f, 0f);
				this.explBlack.transform.position = this.ol;
				this.explBlack.Play();
				this.bl.transform.position = this.ol;
				this.trainBlack.Play();
				this.targetPosition = new Vector3(this.player.transform.position.x, 0f, 0f);
				this.skillEff.transform.position = this.targetPosition + new Vector3(0f, 0f, -3f);
				if (this.dir == Vector2.left)
				{
					this.skillEff.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
				}
				else
				{
					this.skillEff.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
				}
				this.skillEff.SetActive(true);
				base.StartCoroutine(this.Skill3());
				iTween.MoveBy(this.trainBlack.gameObject, iTween.Hash(new object[]
				{
					"y",
					this.lobHeight,
					"time",
					this.lobTime / 3f * 2f,
					"easeType",
					iTween.EaseType.easeOutQuad
				}));
				iTween.MoveBy(this.trainBlack.gameObject, iTween.Hash(new object[]
				{
					"y",
					-this.lobHeight,
					"time",
					this.lobTime / 3f * 1f,
					"delay",
					this.lobTime / 3f * 2f,
					"easeType",
					iTween.EaseType.easeOutQuad
				}));
				iTween.MoveTo(this.bl.gameObject, iTween.Hash(new object[]
				{
					"position",
					this.targetPosition,
					"time",
					this.lobTime,
					"onComplete",
					"CleanUp",
					"easeType",
					iTween.EaseType.linear
				}));
				base.StartCoroutine(this.AttackCoolDown(this.attackingTime[3]));
			}
		}
	}

	private void CleanUp()
	{
		this.trainBlack.Stop();
	}

	private IEnumerator Skill3()
	{
		yield return new WaitForSeconds(1.4f);
		this.golemCol.gameObject.SetActive(true);
		yield return new WaitForSeconds(1.3f);
		this.rockCol.gameObject.SetActive(true);
		yield return new WaitForSeconds(1.5f);
		this.golemCol.gameObject.SetActive(false);
		this.rockCol.gameObject.SetActive(false);
		yield return new WaitForSeconds(3f);
		this.skillEff.SetActive(false);
		yield break;
	}

	private void TransOut()
	{
		base.transform.position += new Vector3(0f, 30f, 0f);
		this.TeleIn.transform.position = this.newpos;
		this.TeleIn.Play();
		this.skill3out.Play();
		base.Invoke("TransIn", 0.2f);
	}

	private void TransIn()
	{
		base.transform.position = this.newpos;
		base.StartCoroutine(this.AttackCoolDown(this.attackingTime[2]));
	}

	public void Xien()
	{
		RaycastHit2D hit = Physics2D.Raycast(this.startPoint.position, this.dir, this.attackDistance[this.attackStyle] + 1.5f, this.layer);
		this.skill1.clip = this.skill1Clip[UnityEngine.Random.Range(0, this.skill1Clip.Length)];
		this.skill1.Play();
		if (hit && hit.collider.tag == "Player")
		{
			if (!this.PlayerScript.BVed)
			{
				GameObject.FindGameObjectWithTag("Effect").GetComponent<EffectController>().ToeMau(hit.point);
				this.PlayerScript.NinjaBiXien();
			}
			else
			{
				GameObject.FindGameObjectWithTag("Effect").GetComponent<EffectController>().ToeLua(hit.point);
			}
		}
	}

	public void XienFx()
	{
		this.ChemFxObj.position = base.transform.position;
		if (this.dir == Vector2.left)
		{
			this.ChemFx2.Play();
		}
		else
		{
			this.ChemFx1.Play();
		}
	}

	public void RoiXuong()
	{
		base.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
		this.RoiFx.transform.position = base.gameObject.transform.position;
		this.RoiFx.Play();
		this.skill2td.Play();
		Collider2D[] array = Physics2D.OverlapCircleAll(base.transform.position, this.ExplorRadius);
		foreach (Collider2D collider2D in array)
		{
			collider2D.gameObject.SendMessage("Exp", SendMessageOptions.DontRequireReceiver);
		}
	}

	private IEnumerator AttackCoolDown(float t)
	{
		yield return new WaitForSeconds(t);
		this.attacking = false;
		base.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
		if (this.attackStyle == 0)
		{
			if (Time.time >= this.timeSkill3)
			{
				this.attackStyle = UnityEngine.Random.Range(0, this.attackDistance.Length);
			}
			else
			{
				this.attackStyle = UnityEngine.Random.Range(0, this.attackDistance.Length - 1);
			}
		}
		else if (this.attackStyle == 1)
		{
			this.attackStyle = 0;
		}
		else if (this.attackStyle == 2)
		{
			this.attackStyle = 0;
		}
		else if (this.attackStyle == 3)
		{
			this.attackStyle = 0;
			base.gameObject.transform.position = this.targetPosition + new Vector3(0f, 1f, 0f);
			this.timeSkill3 = Time.time + 10f;
		}
		this.maxTimeWait = Time.time;
		this.startTrans = true;
		yield break;
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

	public void Active()
	{
		if (!PlayerPrefs.HasKey("S1"))
		{
			PlayerPrefs.SetInt("S1", 1);
			PlayerPrefs.Save();
			this.story = true;
		}
		else
		{
			this.story = false;
		}
		if (this.story)
		{
			this.mainEvent.RemoveControl();
			this.PlayerScript.Btn_Left_bool = false;
			this.PlayerScript.Btn_Right_bool = false;
			this.storyAnimationUI.SetTrigger("Action");
		}
		else
		{
			foreach (Transform transform in this.HpImage)
			{
				transform.gameObject.SetActive(true);
			}
			base.Invoke("ChienLuon", 1f);
		}
	}

	public void ChienLuon()
	{
		this.mainEvent.GetControl();
		foreach (Transform transform in this.HpImage)
		{
			transform.gameObject.SetActive(true);
		}
		this.EnemyDead = false;
		this.anim.SetBool("Active", true);
		this.Summon();
	}

	private void iDied()
	{
		this.EnemyDead = true;
		this.anim.SetTrigger("Die");
		this.soul.iDied();
		base.GetComponent<Rigidbody2D>().gravityScale = 0f;
		Collider2D[] components = base.GetComponents<Collider2D>();
		foreach (Collider2D collider2D in components)
		{
			collider2D.enabled = false;
		}
		base.Invoke("loadEnd", 5f);
	}

	private void loadEnd()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene("cotend");
	}

	private void Hit(Vector2 p)
	{
		if (!this.EnemyDead)
		{
			if (this.hp > 1)
			{
				this.hp--;
				this.HPUpdate();
				if (this.target == this.boderInL)
				{
					this.target = this.boderInR;
				}
				else if (this.target == this.boderInR)
				{
					this.target = this.boderInL;
				}
			}
			else
			{
				this.EnemyDead = true;
				this.hp = 0;
				this.HPUpdate();
				this.iDied();
			}
		}
	}

	private void DashOver()
	{
		if (!this.EnemyDead && Time.time > this.timeToBeDash)
		{
			if (this.hp > 1)
			{
				this.hp -= 5;
				this.HPUpdate();
				GameObject.FindGameObjectWithTag("Effect").GetComponent<EffectController>().ToeMau(base.gameObject.transform.position);
			}
			else
			{
				this.EnemyDead = true;
				GameObject.FindGameObjectWithTag("Effect").GetComponent<EffectController>().ToeMau(base.gameObject.transform.position);
				this.hp = 0;
				this.HPUpdate();
				this.iDied();
			}
			this.timeToBeDash = Time.time + 1f;
		}
	}

	private void HPUpdate()
	{
		iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
		{
			"from",
			this.hpcu,
			"to",
			this.hp,
			"time",
			0.3f,
			"onUpdate",
			"HPValueUpdateImage"
		}));
		this.hpcu = this.hp;
	}

	private void HPValueUpdateImage(float t)
	{
		this.hpOrochiFill.fillAmount = t / (float)this.maxHP;
	}

	public void Summon()
	{
		if (!this.EnemyDead && !this.attacking)
		{
			this.attacking = true;
			this.anim.SetTrigger("Summon");
		}
		else
		{
			base.Invoke("Summon", 1f);
		}
	}

	public void SummonBee()
	{
		if (!this.EnemyDead)
		{
			this.attacking = false;
			this.soul.Summon(this.SummonPoint.position);
		}
	}

	public void Summon2()
	{
		if (!this.EnemyDead && !this.attacking)
		{
			this.attacking = true;
			this.explBlack.transform.position = base.gameObject.transform.position;
			this.explBlack.Play();
			base.StartCoroutine(this.Sm2());
		}
		else
		{
			base.Invoke("Summon2", 1f);
		}
	}

	private IEnumerator Sm2()
	{
		yield return new WaitForSeconds(2f);
		this.attacking = false;
		yield break;
	}

	public Transform boderInL;

	public Transform boderInR;

	public Transform boderOutL;

	public Transform boderOutR;

	public float speed;

	public float speedFollow;

	public int hp;

	public float watchDistance;

	public float[] attackDistance;

	private NinjaMovementScript PlayerScript;

	private MainEventsLog mainEvent;

	public bool EnemyDead;

	private bool canSeePlayer;

	private bool attacking;

	public Transform startPoint;

	public Transform jumpDir;

	public float force;

	public float gocBan;

	public float ExplorRadius;

	public Animator anim;

	public GameObject MySpriteOBJ;

	private Vector3 MySpriteOriginalScale;

	private Transform player;

	public AudioSource EnemyDiesAudio;

	private Transform target;

	private Vector2 dir;

	public LayerMask layer;

	public ParticleSystem ChemFx1;

	public ParticleSystem ChemFx2;

	public ParticleSystem RoiFx;

	public ParticleSystem TeleOut;

	public ParticleSystem TeleIn;

	public Transform ChemFxObj;

	public float[] attackingTime;

	private float timeToBeDash;

	private int attackStyle;

	private Vector3 newpos;

	private float xMax;

	private bool startTrans;

	private float maxTimeWait;

	public float timeWait;

	private int maxHP;

	private int hpcu;

	public OrochiSoul soul;

	public Transform SummonPoint;

	public Image hpOrochiFill;

	public ParticleSystem explBlack;

	public ParticleSystem trainBlack;

	public GameObject skillEff;

	public AudioSource skill1;

	public AudioSource skill2nhay;

	public AudioSource skill2td;

	public AudioSource skill3out;

	public AudioSource skill3in;

	public AudioClip[] skill1Clip;

	private bool story;

	private Vector3 ol;

	public float lobHeight = 4f;

	private float lobTime = 1f;

	private Vector3 targetPosition;

	public iTween.EaseType easeType1;

	public iTween.EaseType easeType2;

	public GameObject bl;

	public Transform golemCol;

	public Transform rockCol;

	private float timeSkill3;

	public Transform[] HpImage;

	public Animator storyAnimationUI;
}
