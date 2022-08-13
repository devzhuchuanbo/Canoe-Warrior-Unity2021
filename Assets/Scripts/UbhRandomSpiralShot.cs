using System;
using System.Collections;
using UnityEngine;

[AddComponentMenu("UniBulletHell/Shot Pattern/Random Spiral Shot")]
public class UbhRandomSpiralShot : UbhBaseShot
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
		if (this._BulletNum <= 0 || this._RandomSpeedMin <= 0f || this._RandomSpeedMax <= 0f)
		{
			UnityEngine.Debug.LogWarning("Cannot shot because BulletNum or RandomSpeedMin or RandomSpeedMax is not set.");
			yield break;
		}
		if (this._Shooting)
		{
			yield break;
		}
		this._Shooting = true;
		for (int i = 0; i < this._BulletNum; i++)
		{
			if (0 < i && 0f <= this._RandomDelayMin && 0f < this._RandomDelayMax)
			{
				float waitTime = UnityEngine.Random.Range(this._RandomDelayMin, this._RandomDelayMax);
				yield return base.StartCoroutine(UbhUtil.WaitForSeconds(waitTime));
			}
			UbhBullet bullet = base.GetBullet(base.transform.position, base.transform.rotation, false);
			if (bullet == null)
			{
				break;
			}
			float bulletSpeed = UnityEngine.Random.Range(this._RandomSpeedMin, this._RandomSpeedMax);
			float centerAngle = this._StartAngle + this._ShiftAngle * (float)i;
			float minAngle = centerAngle - this._RandomRangeSize / 2f;
			float maxAngle = centerAngle + this._RandomRangeSize / 2f;
			float angle = UnityEngine.Random.Range(minAngle, maxAngle);
			base.ShotBullet(bullet, bulletSpeed, angle, false, null, 0f, false, 0f, 0f);
			base.AutoReleaseBulletGameObject(bullet.gameObject);
		}
		base.FinishedShot();
		yield break;
	}

	[Range(0f, 360f)]
	public float _StartAngle = 180f;

	[Range(-360f, 360f)]
	public float _ShiftAngle = 5f;

	[Range(0f, 360f)]
	public float _RandomRangeSize = 30f;

	public float _RandomSpeedMin = 1f;

	public float _RandomSpeedMax = 3f;

	public float _RandomDelayMin = 0.01f;

	public float _RandomDelayMax = 0.1f;
}
