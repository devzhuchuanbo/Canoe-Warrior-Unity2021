using System;
using System.Collections;
using UnityEngine;

[SelectionBase]
public class SpineboyBeginnerModel : MonoBehaviour
{
	public event Action ShootEvent;

	public void TryJump()
	{
		base.StartCoroutine(this.JumpRoutine());
	}

	public void TryShoot()
	{
		float time = Time.time;
		if (time - this.lastShootTime > this.shootInterval)
		{
			this.lastShootTime = time;
			if (this.ShootEvent != null)
			{
				this.ShootEvent();
			}
		}
	}

	public void TryMove(float speed)
	{
		this.currentSpeed = speed;
		if (speed != 0f)
		{
			bool flag = speed < 0f;
			this.facingLeft = flag;
		}
		if (this.state != SpineBeginnerBodyState.Jumping)
		{
			this.state = ((speed != 0f) ? SpineBeginnerBodyState.Running : SpineBeginnerBodyState.Idle);
		}
	}

	private IEnumerator JumpRoutine()
	{
		if (this.state == SpineBeginnerBodyState.Jumping)
		{
			yield break;
		}
		this.state = SpineBeginnerBodyState.Jumping;
		Vector3 pos = base.transform.localPosition;
		for (float t = 0f; t < 0.6f; t += Time.deltaTime)
		{
			float d = 20f * (0.6f - t);
			base.transform.Translate(d * Time.deltaTime * Vector3.up);
			yield return null;
		}
		for (float t2 = 0f; t2 < 0.6f; t2 += Time.deltaTime)
		{
			float d2 = 20f * t2;
			base.transform.Translate(d2 * Time.deltaTime * Vector3.down);
			yield return null;
		}
		base.transform.localPosition = pos;
		this.state = SpineBeginnerBodyState.Idle;
		yield break;
	}

	[Header("Current State")]
	public SpineBeginnerBodyState state;

	public bool facingLeft;

	[Range(-1f, 1f)]
	public float currentSpeed;

	[Header("Balance")]
	public float shootInterval = 0.12f;

	private float lastShootTime;
}
