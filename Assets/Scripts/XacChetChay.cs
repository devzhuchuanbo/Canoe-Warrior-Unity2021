using System;
using UnityEngine;

public class XacChetChay : MonoBehaviour
{
	private void Start()
	{
		this.startPos = base.gameObject.transform.position;
		this.anim = this.MySpriteOBJ.GetComponent<Animator>();
		base.gameObject.SetActive(false);
		this.forc = this.ForceMove;
	}

	private void Update()
	{
		if (this.startRun)
		{
			if (this.playerScript.PlayerLooksRight)
			{
				base.GetComponent<Rigidbody2D>().AddForce(new Vector2(this.ForceMove, 0f) * Time.deltaTime);
			}
			else
			{
				base.GetComponent<Rigidbody2D>().AddForce(new Vector2(-this.ForceMove, 0f) * Time.deltaTime);
			}
			this.ForceMove = Mathf.Lerp(this.ForceMove, 0f, this.timeToMove * Time.deltaTime);
		}
	}

	public void DocAction(Vector2 p)
	{
		base.transform.position = p;
		base.gameObject.SetActive(true);
		if (this.playerScript.PlayerLooksRight)
		{
			this.MySpriteOBJ.transform.localScale = new Vector3(1f, 1f, 1f);
		}
		else
		{
			this.MySpriteOBJ.transform.localScale = new Vector3(-1f, 1f, 1f);
		}
		this.anim.SetTrigger("Doc");
		this.MauXanh.Play();
	}

	public void XienAction(Vector2 p)
	{
		base.transform.position = p;
		base.gameObject.SetActive(true);
		if (this.playerScript.PlayerLooksRight)
		{
			this.MySpriteOBJ.transform.localScale = new Vector3(1f, 1f, 1f);
		}
		else
		{
			this.MySpriteOBJ.transform.localScale = new Vector3(-1f, 1f, 1f);
		}
		int num = UnityEngine.Random.Range(0, 100);
		if (num < 50)
		{
			this.anim.SetTrigger("Die");
		}
		else
		{
			this.anim.SetTrigger("Doc");
		}
		this.MauDo.Play();
	}

	public void BanAction(Vector2 p)
	{
		base.transform.position = p;
		base.gameObject.SetActive(true);
		if (this.playerScript.PlayerLooksRight)
		{
			this.MySpriteOBJ.transform.localScale = new Vector3(1f, 1f, 1f);
		}
		else
		{
			this.MySpriteOBJ.transform.localScale = new Vector3(-1f, 1f, 1f);
		}
		int num = UnityEngine.Random.Range(0, 100);
		if (num < 50)
		{
			this.anim.SetTrigger("Die");
		}
		else
		{
			this.anim.SetTrigger("Doc");
		}
		this.MauDo.Play();
	}

	public void ChayAction(Vector2 p)
	{
		base.transform.position = p;
		base.gameObject.SetActive(true);
		this.startRun = true;
		base.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
		this.ForceMove = this.forc;
		if (this.playerScript.PlayerLooksRight)
		{
			this.MySpriteOBJ.transform.localScale = new Vector3(-1f, 1f, 1f);
		}
		else
		{
			this.MySpriteOBJ.transform.localScale = new Vector3(1f, 1f, 1f);
		}
		this.anim.SetTrigger("Chay");
		this.Lua.Play();
	}

	public void GiatAction(Vector2 p)
	{
		base.transform.position = p;
		base.gameObject.SetActive(true);
		if (this.playerScript.PlayerLooksRight)
		{
			this.MySpriteOBJ.transform.localScale = new Vector3(1f, 1f, 1f);
		}
		else
		{
			this.MySpriteOBJ.transform.localScale = new Vector3(-1f, 1f, 1f);
		}
		this.anim.SetTrigger("Giat");
		this.SetGiat.Play();
	}

	public void HideNow()
	{
		this.startRun = false;
		base.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
		base.gameObject.transform.position = this.startPos;
		this.MauXanh.Stop();
		this.MauDo.Stop();
		this.SetGiat.Stop();
		this.Lua.Stop();
		base.gameObject.SetActive(false);
	}

	public GameObject MySpriteOBJ;

	public ParticleSystem MauXanh;

	public ParticleSystem MauDo;

	public ParticleSystem SetGiat;

	public ParticleSystem Lua;

	public float timeToMove;

	public float ForceMove;

	private bool startRun;

	public NinjaMovementScript playerScript;

	private Animator anim;

	private Vector2 startPos;

	private float forc;
}
