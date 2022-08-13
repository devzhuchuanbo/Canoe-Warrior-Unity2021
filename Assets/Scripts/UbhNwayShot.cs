using System;
using System.Collections;
using UnityEngine;

[AddComponentMenu("UniBulletHell/Shot Pattern/nWay Shot")]
public class UbhNwayShot : UbhBaseShot
{
	protected override void Awake()
	{
		base.Awake();
	}

	public override void Shot()
	{
		base.StartCoroutine(this.ShotCoroutine());
	}

	private IEnumerator ShotCoroutine()
	{
		if (this._BulletNum <= 0 || this._BulletSpeed <= 0f || this._WayNum <= 0)
		{
			UnityEngine.Debug.LogWarning("Cannot shot because BulletNum or BulletSpeed or WayNum is not set.");
			yield break;
		}
		if (this._Shooting)
		{
			yield break;
		}
		this._Shooting = true;
		int wayIndex = 0;
		for (int i = 0; i < this._BulletNum; i++)
		{
			if (this._WayNum <= wayIndex)
			{
				wayIndex = 0;
				if (0f < this._NextLineDelay)
				{
					yield return base.StartCoroutine(UbhUtil.WaitForSeconds(this._NextLineDelay));
				}
			}
			UbhBullet bullet = base.GetBullet(base.transform.position, base.transform.rotation, false);
			if (bullet == null)
			{
				break;
			}
			float baseAngle = (this._WayNum % 2 != 0) ? this._CenterAngle : (this._CenterAngle - this._BetweenAngle / 2f);
			float angle = UbhUtil.GetShiftedAngle(wayIndex, baseAngle, this._BetweenAngle);
			base.ShotBullet(bullet, this._BulletSpeed, angle, false, null, 0f, false, 0f, 0f);
			base.AutoReleaseBulletGameObject(bullet.gameObject);
			wayIndex++;
		}
		base.FinishedShot();
		yield break;
	}

	public int _WayNum = 5;

	[Range(0f, 360f)]
	public float _CenterAngle = 180f;

	[Range(0f, 360f)]
	public float _BetweenAngle = 10f;

	public float _NextLineDelay = 0.1f;
}
