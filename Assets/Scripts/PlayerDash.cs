using System;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
	private void Start()
	{
	}

	public void ShowGhost(int i)
	{
		if (this.playerScript.PlayerLooksRight)
		{
			this.DGhost[i].localScale = new Vector3(1f, 1f, 1f);
		}
		else
		{
			this.DGhost[i].localScale = new Vector3(-1f, 1f, 1f);
		}
		Animator component = this.DGhost[i].GetComponent<Animator>();
		component.SetTrigger("Show");
		this.DGhost[i].position = base.transform.position;
	}

	public void Step()
	{
		this.playerScript.FootStep();
	}

	public Transform[] DGhost;

	public NinjaMovementScript playerScript;

	private Animator anim;
}
