using System;
using UnityEngine;

[AddComponentMenu("UniBulletHell/Shot Pattern/Hole Circle Shot")]
public class UbhHoleCircleShot : UbhBaseShot
{
	protected override void Awake()
	{
		base.Awake();
	}

	public override void Shot()
	{
		if (this._BulletNum <= 0 || this._BulletSpeed <= 0f)
		{
			UnityEngine.Debug.LogWarning("Cannot shot because BulletNum or BulletSpeed is not set.");
			return;
		}
		this._HoleCenterAngle = UbhUtil.Get360Angle(this._HoleCenterAngle);
		float num = this._HoleCenterAngle - this._HoleSize / 2f;
		float num2 = this._HoleCenterAngle + this._HoleSize / 2f;
		float num3 = 360f / (float)this._BulletNum;
		for (int i = 0; i < this._BulletNum; i++)
		{
			float num4 = num3 * (float)i;
			if (num > num4 || num4 > num2)
			{
				UbhBullet bullet = base.GetBullet(base.transform.position, base.transform.rotation, false);
				if (bullet == null)
				{
					break;
				}
				base.ShotBullet(bullet, this._BulletSpeed, num4, false, null, 0f, false, 0f, 0f);
				base.AutoReleaseBulletGameObject(bullet.gameObject);
			}
		}
		base.FinishedShot();
	}

	[Range(0f, 360f)]
	public float _HoleCenterAngle = 180f;

	[Range(0f, 360f)]
	public float _HoleSize = 20f;
}
