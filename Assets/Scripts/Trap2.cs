using System;
using UnityEngine;

public class Trap2 : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.tag == "Player")
		{
			if (this.mainTrap)
			{
				this.mainTrap.gameObject.SetActive(true);
			}
			UnityEngine.Object.Destroy(base.gameObject, 5f);
		}
	}

	public Transform mainTrap;
}
