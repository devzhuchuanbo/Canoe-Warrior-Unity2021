using System;
using UnityEngine;

public class Finish : MonoBehaviour
{
	private void Start()
	{
		this.mainEventLogScript = GameObject.FindGameObjectWithTag("MainEventLog").GetComponent<MainEventsLog>();
	}

	private void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Player" && !this.tg)
		{
			this.tg = true;
			if (this.mainEventLogScript == null)
			{
				this.mainEventLogScript = GameObject.FindGameObjectWithTag("MainEventLog").GetComponent<MainEventsLog>();
			}
			this.mainEventLogScript.Finish(0);
		}
	}

	private MainEventsLog mainEventLogScript;

	private bool tg;
}
