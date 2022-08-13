using System;
using UnityEngine;

[AddComponentMenu("UniBulletHell/Shot Pattern/Circle Shot")]
public class UbhCircleShot : UbhBaseShot
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
		float num = 360f / (float)this._BulletNum;
		for (int i = 0; i < this._BulletNum; i++)
		{
			UbhBullet bullet = base.GetBullet(base.transform.position, base.transform.rotation, false);
			if (bullet == null)
			{
				break;
			}
			float angle = num * (float)i;
			base.ShotBullet(bullet, this._BulletSpeed, angle, false, null, 0f, false, 0f, 0f);
			base.AutoReleaseBulletGameObject(bullet.gameObject);
		}
		base.FinishedShot();
	}
}
