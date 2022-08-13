using System;
using UnityEngine;

public class HUDTextChild : MonoBehaviour
{
	private void Start()
	{
		this.t.GetComponent<MeshRenderer>().sortingOrder = 50;
	}

	public void ShowOut(int textShow, Color c, Transform p)
	{
		if (textShow > 0)
		{
			this.t.text = "+" + textShow.ToString();
		}
		else
		{
			this.t.text = textShow.ToString();
		}
		this.t.color = c;
		base.transform.position = p.position;
		this.anim = base.GetComponent<Animator>();
		this.anim.SetTrigger("Show");
		Vector2 vector = new Vector2(UnityEngine.Random.Range(-0.5f, 0.5f), 1f);
		Vector2 normalized = vector.normalized;
		base.GetComponent<Rigidbody2D>().AddForce(normalized * 1500f);
	}

	public void Show(int textShow, Color c)
	{
		if (textShow > 0)
		{
			this.t.text = "+" + textShow.ToString();
		}
		else
		{
			this.t.text = textShow.ToString();
		}
		this.t.color = c;
		this.anim = base.GetComponent<Animator>();
		this.anim.SetTrigger("Show");
	}

	public void ShowPos(string textShow, Color c, Vector3 p)
	{
		this.t.text = textShow;
		this.t.color = c;
		base.transform.position = p;
		this.anim = base.GetComponent<Animator>();
		this.anim.SetTrigger("Show");
	}

	public TextMesh t;

	private Animator anim;
}
