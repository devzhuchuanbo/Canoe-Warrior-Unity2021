using System;
using UnityEngine;

public class LuuDan : MonoBehaviour
{
	private void Start()
	{
		base.Invoke("Explor", 5f);
	}

	private void OnCollisionEnter2D(Collision2D coll)
	{
		this.Explor();
	}

	public void Explor()
	{
		Collider2D[] array = Physics2D.OverlapCircleAll(base.transform.position, this.ExplorRadius);
		foreach (Collider2D collider2D in array)
		{
			collider2D.gameObject.SendMessage("Exp", SendMessageOptions.DontRequireReceiver);
		}
		GameObject.FindGameObjectWithTag("Effect").GetComponent<EffectController>().BoomNo(base.gameObject.transform.position);
		UnityEngine.Object.Destroy(base.gameObject);
	}

	private float ExplorRadius = 3f;
}
