using System;
using UnityEngine;

public class HPFly : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.tag == "Player")
		{
			base.GetComponent<Collider2D>().enabled = false;
			this.Dragon.gameObject.SetActive(true);
		}
	}

	public Transform Dragon;
}
