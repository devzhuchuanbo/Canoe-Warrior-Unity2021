using System;
using UnityEngine;

public class KillZone : MonoBehaviour
{
	private void Start()
	{
		this.MyMeshrenderer.enabled = false;
	}

	private void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			if (this.NinjaScript == null)
			{
				this.NinjaScript = GameObject.FindGameObjectWithTag("Player").GetComponent<NinjaMovementScript>();
			}
			this.NinjaScript.NinjaDiesKillZone();
		}
	}

	public MeshRenderer MyMeshrenderer;

	private NinjaMovementScript NinjaScript;
}
