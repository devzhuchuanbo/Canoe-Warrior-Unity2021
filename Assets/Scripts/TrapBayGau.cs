using System;
using System.Collections;
using UnityEngine;

public class TrapBayGau : MonoBehaviour
{
	private void Start()
	{
		this.anim = base.GetComponent<Animator>();
		this.actived = false;
	}

	private void Update()
	{
		if (this.actived)
		{
			if (!this.ninjaScript.isDieing)
			{
				base.gameObject.transform.position = new Vector3(this.player.position.x, base.gameObject.transform.position.y, base.gameObject.transform.position.z);
			}
			else if (!this.br)
			{
				this.anim.SetTrigger("Break");
				GameObject.FindGameObjectWithTag("Effect").GetComponent<EffectController>().DamBayGau();
				this.br = true;
				this.actived = false;
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D Coll)
	{
		if (Coll.tag == "Player")
		{
			base.GetComponent<Collider2D>().enabled = false;
			this.player = Coll.gameObject.transform;
			this.ninjaScript = this.player.GetComponent<NinjaMovementScript>();
			this.anim.SetTrigger("Hit");
			GameObject.FindGameObjectWithTag("Effect").GetComponent<EffectController>().DamBayGau();
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
						base.StartCoroutine(this.TrapBreak());
						this.actived = true;
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
			else
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
		}
	}

	private IEnumerator TrapBreak()
	{
		yield return new WaitForSeconds(this.timeHold);
		if (!this.br)
		{
			this.anim.SetTrigger("Break");
			GameObject.FindGameObjectWithTag("Effect").GetComponent<EffectController>().DamBayGau();
			this.br = true;
			this.actived = false;
		}
		UnityEngine.Object.Destroy(base.gameObject, 3f);
		yield break;
	}

	public float timeHold;

	private Animator anim;

	private bool actived;

	private Transform player;

	private NinjaMovementScript ninjaScript;

	private bool br;
}
