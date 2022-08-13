using System;
using System.Collections;
using UnityEngine;

[AddComponentMenu("UniBulletHell/Shot Pattern/Linear Shot")]
public class UbhLinearShot : UbhBaseShot
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
		if (this._BulletNum <= 0 || this._BulletSpeed <= 0f)
		{
			UnityEngine.Debug.LogWarning("Cannot shot because BulletNum or BulletSpeed is not set.");
			yield break;
		}
		if (this._Shooting)
		{
			yield break;
		}
		this._Shooting = true;
		for (int i = 0; i < this._BulletNum; i++)
		{
			if (0 < i && 0f < this._BetweenDelay)
			{
				yield return base.StartCoroutine(UbhUtil.WaitForSeconds(this._BetweenDelay));
			}
			UbhBullet bullet = base.GetBullet(base.transform.position, base.transform.rotation, false);
			if (bullet == null)
			{
				break;
			}
			base.ShotBullet(bullet, this._BulletSpeed, this._Angle, false, null, 0f, false, 0f, 0f);
			base.AutoReleaseBulletGameObject(bullet.gameObject);
		}
		base.FinishedShot();
		yield break;
	}

	[Range(0f, 360f)]
	public float _Angle = 180f;

	public float _BetweenDelay = 0.1f;
}
