using System;
using UnityEngine;

public class TriggerActive : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			foreach (Transform transform in this.activeOBJ)
			{
				transform.SendMessage("Active", SendMessageOptions.DontRequireReceiver);
			}
			base.GetComponent<Collider2D>().enabled = false;
		}
	}

	public Transform[] activeOBJ;
}
