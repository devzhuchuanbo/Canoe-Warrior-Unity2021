using System;
using System.Collections;
using UnityEngine;

[AddComponentMenu("UniBulletHell/Shot Pattern/Spiral Multi Shot")]
public class UbhSpiralMultiShot : UbhBaseShot
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
		if (this._BulletNum <= 0 || this._BulletSpeed <= 0f || this._SpiralWayNum <= 0)
		{
			UnityEngine.Debug.LogWarning("Cannot shot because BulletNum or BulletSpeed or SpiralWayNum is not set.");
			yield break;
		}
		if (this._Shooting)
		{
			yield break;
		}
		this._Shooting = true;
		float spiralWayShiftAngle = 360f / (float)this._SpiralWayNum;
		int spiralWayIndex = 0;
		for (int i = 0; i < this._BulletNum; i++)
		{
			if (this._SpiralWayNum <= spiralWayIndex)
			{
				spiralWayIndex = 0;
				if (0f < this._BetweenDelay)
				{
					yield return base.StartCoroutine(UbhUtil.WaitForSeconds(this._BetweenDelay));
				}
			}
			UbhBullet bullet = base.GetBullet(base.transform.position, base.transform.rotation, false);
			if (bullet == null)
			{
				break;
			}
			float angle = this._StartAngle + spiralWayShiftAngle * (float)spiralWayIndex + this._ShiftAngle * Mathf.Floor((float)(i / this._SpiralWayNum));
			base.ShotBullet(bullet, this._BulletSpeed, angle, false, null, 0f, false, 0f, 0f);
			base.AutoReleaseBulletGameObject(bullet.gameObject);
			spiralWayIndex++;
		}
		base.FinishedShot();
		yield break;
	}

	public int _SpiralWayNum = 4;

	[Range(0f, 360f)]
	public float _StartAngle = 180f;

	[Range(-360f, 360f)]
	public float _ShiftAngle = 5f;

	public float _BetweenDelay = 0.2f;
}
