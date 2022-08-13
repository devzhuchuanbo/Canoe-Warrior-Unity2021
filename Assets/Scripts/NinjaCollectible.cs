using System;
using UnityEngine;

public class NinjaCollectible : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Player" && !this.collected)
		{
			this.collected = true;
			this.SoulSprites.SetActive(false);
			this.SoulParticles.Emit(10);
			base.Invoke("DestroyObject", 1f);
			if (this.MainEventsLog_script == null)
			{
				this.MainEventsLog_script = GameObject.FindGameObjectWithTag("MainEventLog").GetComponent<MainEventsLog>();
			}
		}
	}

	private void DestroyObject()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	private bool collected;

	public GameObject SoulSprites;

	public ParticleSystem SoulParticles;

	private MainEventsLog MainEventsLog_script;
}
