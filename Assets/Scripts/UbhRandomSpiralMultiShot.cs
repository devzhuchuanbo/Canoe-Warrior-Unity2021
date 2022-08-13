using System;
using System.Collections;
using UnityEngine;

[AddComponentMenu("UniBulletHell/Shot Pattern/Random Spiral Multi Shot")]
public class UbhRandomSpiralMultiShot : UbhBaseShot
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
		if (this._BulletNum <= 0 || this._RandomSpeedMin <= 0f || this._RandomSpeedMax <= 0f || this._SpiralWayNum <= 0)
		{
			UnityEngine.Debug.LogWarning("Cannot shot because BulletNum or RandomSpeedMin or RandomSpeedMax or SpiralWayNum is not set.");
			yield break;
		}
		if (this._Shooting)
		{
			yield break;
		}
		this._Shooting = true;
		float wayAngle = 360f / (float)this._SpiralWayNum;
		int wayIndex = 0;
		for (int i = 0; i < this._BulletNum; i++)
		{
			if (this._SpiralWayNum <= wayIndex)
			{
				wayIndex = 0;
				if (0f <= this._RandomDelayMin && 0f < this._RandomDelayMax)
				{
					float waitTime = UnityEngine.Random.Range(this._RandomDelayMin, this._RandomDelayMax);
					yield return base.StartCoroutine(UbhUtil.WaitForSeconds(waitTime));
				}
			}
			UbhBullet bullet = base.GetBullet(base.transform.position, base.transform.rotation, false);
			if (bullet == null)
			{
				break;
			}
			float bulletSpeed = UnityEngine.Random.Range(this._RandomSpeedMin, this._RandomSpeedMax);
			float centerAngle = this._StartAngle + wayAngle * (float)wayIndex + this._ShiftAngle * Mathf.Floor((float)(i / this._SpiralWayNum));
			float minAngle = centerAngle - this._RandomRangeSize / 2f;
			float maxAngle = centerAngle + this._RandomRangeSize / 2f;
			float angle = UnityEngine.Random.Range(minAngle, maxAngle);
			base.ShotBullet(bullet, bulletSpeed, angle, false, null, 0f, false, 0f, 0f);
			base.AutoReleaseBulletGameObject(bullet.gameObject);
			wayIndex++;
		}
		base.FinishedShot();
		yield break;
	}

	public int _SpiralWayNum = 4;

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
