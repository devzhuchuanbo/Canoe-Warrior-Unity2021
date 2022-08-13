using System;
using System.Collections;
using UnityEngine;

public class UbhBullet : UbhMonoBehaviour
{
	public bool _Shooting { get; private set; }

	private void OnDisable()
	{
		base.StopAllCoroutines();
		base.transform.ResetPosition(false);
		base.transform.ResetRotation(false);
		this._Shooting = false;
	}

	public void Shot(float speed, float angle, float accelSpeed, float accelTurn, bool homing, Transform homingTarget, float homingAngleSpeed, bool wave, float waveSpeed, float waveRangeSize, bool pauseAndResume, float pauseTime, float resumeTime, UbhUtil.AXIS axisMove)
	{
		if (this._Shooting)
		{
			return;
		}
		this._Shooting = true;
		base.StartCoroutine(this.MoveCoroutine(speed, angle, accelSpeed, accelTurn, homing, homingTarget, homingAngleSpeed, wave, waveSpeed, waveRangeSize, pauseAndResume, pauseTime, resumeTime, axisMove));
	}

	private IEnumerator MoveCoroutine(float speed, float angle, float accelSpeed, float accelTurn, bool homing, Transform homingTarget, float homingAngleSpeed, bool wave, float waveSpeed, float waveRangeSize, bool pauseAndResume, float pauseTime, float resumeTime, UbhUtil.AXIS axisMove)
	{
		if (axisMove == UbhUtil.AXIS.X_AND_Z)
		{
			base.transform.SetEulerAnglesY(-angle);
		}
		else
		{
			base.transform.SetEulerAnglesZ(angle);
		}
		float selfFrameCnt = 0f;
		float selfTimeCount = 0f;
		for (;;)
		{
			if (homing)
			{
				if (homingTarget != null && 0f < homingAngleSpeed)
				{
					float rotAngle = UbhUtil.GetAngleFromTwoPosition(base.transform, homingTarget, axisMove);
					float myAngle = 0f;
					if (axisMove == UbhUtil.AXIS.X_AND_Z)
					{
						myAngle = -base.transform.eulerAngles.y;
					}
					else
					{
						myAngle = base.transform.eulerAngles.z;
					}
					float toAngle = Mathf.MoveTowardsAngle(myAngle, rotAngle, UbhSingletonMonoBehavior<UbhTimer>.Instance.DeltaTime * homingAngleSpeed);
					if (axisMove == UbhUtil.AXIS.X_AND_Z)
					{
						base.transform.SetEulerAnglesY(-toAngle);
					}
					else
					{
						base.transform.SetEulerAnglesZ(toAngle);
					}
				}
			}
			else if (wave)
			{
				angle += accelTurn * UbhSingletonMonoBehavior<UbhTimer>.Instance.DeltaTime;
				if (0f < waveSpeed && 0f < waveRangeSize)
				{
					float waveAngle = angle + waveRangeSize / 2f * Mathf.Sin(selfFrameCnt * waveSpeed / 100f);
					if (axisMove == UbhUtil.AXIS.X_AND_Z)
					{
						base.transform.SetEulerAnglesY(-waveAngle);
					}
					else
					{
						base.transform.SetEulerAnglesZ(waveAngle);
					}
				}
				selfFrameCnt += 1f;
			}
			else
			{
				float addAngle = accelTurn * UbhSingletonMonoBehavior<UbhTimer>.Instance.DeltaTime;
				if (axisMove == UbhUtil.AXIS.X_AND_Z)
				{
					base.transform.AddEulerAnglesY(-addAngle);
				}
				else
				{
					base.transform.AddEulerAnglesZ(addAngle);
				}
			}
			speed += accelSpeed * UbhSingletonMonoBehavior<UbhTimer>.Instance.DeltaTime;
			if (axisMove == UbhUtil.AXIS.X_AND_Z)
			{
				base.transform.position += base.transform.forward.normalized * speed * UbhSingletonMonoBehavior<UbhTimer>.Instance.DeltaTime;
			}
			else
			{
				base.transform.position += base.transform.up.normalized * speed * UbhSingletonMonoBehavior<UbhTimer>.Instance.DeltaTime;
			}
			yield return 0;
			selfTimeCount += UbhSingletonMonoBehavior<UbhTimer>.Instance.DeltaTime;
			if (pauseAndResume && pauseTime >= 0f && resumeTime > pauseTime)
			{
				while (pauseTime <= selfTimeCount && selfTimeCount < resumeTime)
				{
					yield return 0;
					selfTimeCount += UbhSingletonMonoBehavior<UbhTimer>.Instance.DeltaTime;
				}
			}
		}
		yield break;
	}
}
