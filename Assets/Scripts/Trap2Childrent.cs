using System;
using UnityEngine;

public class Trap2Childrent : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "KillZone")
		{
			base.gameObject.GetComponent<Collider2D>().isTrigger = true;
			this.trap2Sound.Stop();
			UnityEngine.Object.Destroy(base.gameObject, 1f);
		}
	}

	public AudioSource trap2Sound;
}
