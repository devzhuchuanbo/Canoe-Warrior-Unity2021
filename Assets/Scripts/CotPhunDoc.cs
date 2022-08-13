using System;
using UnityEngine;

public class CotPhunDoc : MonoBehaviour
{
	private void Start()
	{
		this.dir = Vector2.up;
		this.bichan = false;
		if (this.DocTran)
		{
			this.DocTran.gameObject.SetActive(false);
		}
	}

	private void Update()
	{
		if (!this.bichan)
		{
			RaycastHit2D hit = Physics2D.Raycast(this.Trigger.position, this.dir, 2f, this.layer);
			if (hit)
			{
				this.bichan = true;
				this.DocRoi.gameObject.SetActive(false);
				if (this.DocTran)
				{
					this.DocTran.gameObject.SetActive(true);
				}
			}
		}
		else
		{
			RaycastHit2D hit2 = Physics2D.Raycast(this.Trigger.position, this.dir, 2f, this.layer);
			if (!hit2)
			{
				this.bichan = false;
				this.DocRoi.gameObject.SetActive(true);
				if (this.DocTran)
				{
					this.DocTran.gameObject.SetActive(false);
				}
			}
		}
	}

	public Transform DocRoi;

	public Transform DocTran;

	public Transform Trigger;

	public LayerMask layer;

	private Vector2 dir;

	private bool bichan;
}
