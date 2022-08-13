using System;
using UnityEngine;

public class CongChao : MonoBehaviour
{
	private void Start()
	{
	}

	private void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Player" && !this.sended)
		{
			if (this.kieu == CongChao.kieucong.baibth)
			{
				coll.gameObject.SendMessage("CongFinish", base.gameObject);
				this.sended = true;
				this.AudioFinish.Play();
			}
			else if (this.kieu == CongChao.kieucong.bai1)
			{
				coll.gameObject.SendMessage("CongBai1", base.gameObject);
				this.sended = true;
				this.AudioFinish.Play();
			}
			else
			{
				this.portal.gameObject.SetActive(true);
				coll.gameObject.SendMessage("CongPortal", base.gameObject);
				this.sended = true;
				this.AudioFinish.Play();
			}
		}
	}

	public AudioSource AudioFinish;

	public CongChao.kieucong kieu;

	public Transform portal;

	private bool sended;

	public enum kieucong
	{
		baibth,
		bai1,
		portal
	}
}
