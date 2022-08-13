using System;
using UnityEngine;

public class HamMo : MonoBehaviour
{
	private void Start()
	{
		this.hp = 3;
		this.anim = base.GetComponent<Animator>();
	}

	private void Hit(Vector2 p)
	{
		this.hp--;
		if (this.hp == 2)
		{
			this.CuaHam.sprite = this.cuaHam1;
		}
		else
		{
			UnityEngine.Object.Destroy(this.CuaHam.gameObject);
			base.GetComponent<Collider2D>().enabled = false;
			if (this.CuaThong)
			{
				UnityEngine.Object.Destroy(this.CuaThong);
			}
			this.anim.SetTrigger("Hit");
		}
	}

	private void Hit2(Vector2 p)
	{
		UnityEngine.Object.Destroy(this.CuaHam.gameObject);
		base.GetComponent<Collider2D>().enabled = false;
		if (this.CuaThong)
		{
			UnityEngine.Object.Destroy(this.CuaThong);
		}
		this.anim.SetTrigger("Hit");
	}

	private void Exp()
	{
		UnityEngine.Object.Destroy(this.CuaHam.gameObject);
		base.GetComponent<Collider2D>().enabled = false;
		if (this.CuaThong)
		{
			UnityEngine.Object.Destroy(this.CuaThong);
		}
		this.anim.SetTrigger("Hit");
	}

	public Sprite cuaHam1;

	public Sprite cuaHam2;

	public SpriteRenderer CuaHam;

	public GameObject CuaThong;

	private int hp;

	private Animator anim;
}
