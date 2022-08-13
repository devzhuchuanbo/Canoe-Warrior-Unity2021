using System;
using UnityEngine;

public class TrapLong : MonoBehaviour
{
	private void Start()
	{
		this.anim = base.GetComponent<Animator>();
		this.actived = false;
		this.used = false;
	}

	private void Update()
	{
		if (this.actived)
		{
			if (!this.ninjaScript.isDieing)
			{
				base.gameObject.transform.position = new Vector3(this.player.position.x, base.gameObject.transform.position.y, base.gameObject.transform.position.z);
			}
			else
			{
				if (!this.br)
				{
					this.anim.SetTrigger("Break");
					GameObject.FindGameObjectWithTag("Effect").GetComponent<EffectController>().DamBayGau();
					this.br = true;
					this.actived = false;
				}
				base.Invoke("TrapBreak", 0.5f);
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D Coll)
	{
		if (Coll.tag == "Player" && !this.used)
		{
			this.used = true;
			base.GetComponent<Collider2D>().enabled = false;
			this.player = Coll.gameObject.transform;
			GameObject.FindGameObjectWithTag("Effect").GetComponent<EffectController>().DamBayGau();
			this.ninjaScript = this.player.GetComponent<NinjaMovementScript>();
			this.anim.SetTrigger("Hit");
			if (this.ninjaScript)
			{
				if (!this.ninjaScript.BVed)
				{
					if (this.ninjaScript.MaxSkillDash && this.ninjaScript.dashing)
					{
						if (!this.br)
						{
							this.anim.SetTrigger("Break");
							GameObject.FindGameObjectWithTag("Effect").GetComponent<EffectController>().DamBayGau();
							this.br = true;
							this.actived = false;
						}
						base.gameObject.transform.position = new Vector3(this.player.position.x, base.gameObject.transform.position.y, base.gameObject.transform.position.z);
						this.actived = false;
						UnityEngine.Object.Destroy(base.gameObject, 3f);
					}
					else
					{
						Coll.gameObject.SendMessage("TrapHold", this.timeHold, SendMessageOptions.DontRequireReceiver);
						this.actived = true;
						this.CaiLong.SetActive(true);
					}
				}
				else
				{
					if (!this.br)
					{
						this.anim.SetTrigger("Break");
						GameObject.FindGameObjectWithTag("Effect").GetComponent<EffectController>().DamBayGau();
						this.br = true;
						this.actived = false;
					}
					base.gameObject.transform.position = new Vector3(this.player.position.x, base.gameObject.transform.position.y, base.gameObject.transform.position.z);
					this.actived = false;
					UnityEngine.Object.Destroy(base.gameObject, 3f);
				}
			}
		}
	}

	private void TrapBreak()
	{
		if (!this.br)
		{
			this.anim.SetTrigger("Break");
			GameObject.FindGameObjectWithTag("Effect").GetComponent<EffectController>().DamBayGau();
			this.br = true;
			this.actived = false;
		}
		UnityEngine.Object.Destroy(base.gameObject, 3f);
	}

	public float timeHold;

	public GameObject CaiLong;

	private Animator anim;

	private bool actived;

	private Transform player;

	private NinjaMovementScript ninjaScript;

	private bool used;

	private bool br;
}
