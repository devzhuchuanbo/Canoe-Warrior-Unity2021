using System;
using UnityEngine;

public class Boom : MonoBehaviour
{
	private void Start()
	{
		base.Invoke("Explor", this.timeExplor);
		GameObject.FindGameObjectWithTag("Effect").GetComponent<EffectController>().BoomXi();
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

	public float ExplorRadius;

	public float timeExplor;
}
