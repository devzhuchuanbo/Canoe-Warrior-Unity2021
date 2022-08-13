using System;
using UnityEngine;

public class OngDan : MonoBehaviour
{
	private void Start()
	{
		this.PlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<NinjaMovementScript>();
		this.AnimatorController = base.GetComponent<Animator>();
		base.InvokeRepeating("CheckPlayerDistance", 0.5f, 0.5f);
	}

	private void FixedUpdate()
	{
		if (this.EnemyAwake && !this.EnemyDead)
		{
			float maxDistanceDelta = this.speed * Time.deltaTime;
			base.transform.position = Vector3.MoveTowards(base.transform.position, this.PlayerScript.transform.position, maxDistanceDelta);
		}
		this.AnimatorController.SetBool("Attack", this.EnemyAwake);
	}

	private void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Player" && !this.EnemyDead && this.PlayerScript)
		{
			this.PlayerScript.NinjaBiMuoiDot();
		}
	}

	private void OngDatChet()
	{
		if (this.EnemyDead)
		{
			return;
		}
		if (this.EnemyDiesAudio != null)
		{
			this.EnemyDiesAudio.Play();
		}
		this.EnemyDead = true;
		this.PlayerScript.NinjaKilledEnemy(base.transform.position);
		this.AnimatorController.SetBool("Dead", true);
		UnityEngine.Object.Destroy(base.gameObject, 2f);
	}

	private void Hit(Vector2 p)
	{
		if (!this.EnemyDead)
		{
			this.OngDatChet();
		}
	}

	private void Hit2(Vector2 p)
	{
		if (!this.EnemyDead)
		{
			this.OngDatChet();
		}
	}

	private void DashOver()
	{
		if (!this.EnemyDead)
		{
			GameObject.FindGameObjectWithTag("Effect").GetComponent<EffectController>().ToeMauXanh(base.gameObject.transform.position);
			this.OngDatChet();
		}
	}

	private void Exp()
	{
		if (!this.EnemyDead)
		{
			GameObject.FindGameObjectWithTag("Effect").GetComponent<EffectController>().ToeMauXanh(base.gameObject.transform.position);
			this.OngDatChet();
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

	private NinjaMovementScript PlayerScript;

	private MainEventsLog mainEvent;

	private bool EnemyAwake;

	private bool EnemyDead;

	public float AwakeDistance = 10f;

	private Animator AnimatorController;

	public AudioSource EnemyDiesAudio;
}
