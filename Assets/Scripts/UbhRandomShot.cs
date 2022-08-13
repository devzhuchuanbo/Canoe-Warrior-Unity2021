using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("UniBulletHell/Shot Pattern/Random Shot")]
public class UbhRandomShot : UbhBaseShot
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
		List<int> numList = new List<int>();
		for (int i = 0; i < this._BulletNum; i++)
		{
			numList.Add(i);
		}
		while (0 < numList.Count)
		{
			int index = UnityEngine.Random.Range(0, numList.Count);
			UbhBullet bullet = base.GetBullet(base.transform.position, base.transform.rotation, false);
			if (bullet == null)
			{
				break;
			}
			float bulletSpeed = UnityEngine.Random.Range(this._RandomSpeedMin, this._RandomSpeedMax);
			float minAngle = this._RandomCenterAngle - this._RandomRangeSize / 2f;
			float maxAngle = this._RandomCenterAngle + this._RandomRangeSize / 2f;
			float angle = 0f;
			if (this._EvenlyDistribute)
			{
				float oneDirectionNum = Mathf.Floor((float)this._BulletNum / 4f);
				float quarterIndex = Mathf.Floor((float)numList[index] / oneDirectionNum);
				float quarterAngle = Mathf.Abs(maxAngle - minAngle) / 4f;
				angle = UnityEngine.Random.Range(minAngle + quarterAngle * quarterIndex, minAngle + quarterAngle * (quarterIndex + 1f));
			}
			else
			{
				angle = UnityEngine.Random.Range(minAngle, maxAngle);
			}
			base.ShotBullet(bullet, bulletSpeed, angle, false, null, 0f, false, 0f, 0f);
			base.AutoReleaseBulletGameObject(bullet.gameObject);
			numList.RemoveAt(index);
			if (0 < numList.Count && 0f <= this._RandomDelayMin && 0f < this._RandomDelayMax)
			{
				float waitTime = UnityEngine.Random.Range(this._RandomDelayMin, this._RandomDelayMax);
				yield return base.StartCoroutine(UbhUtil.WaitForSeconds(waitTime));
			}
		}
		base.FinishedShot();
		yield break;
	}

	[Range(0f, 360f)]
	public float _RandomCenterAngle = 180f;

	[Range(0f, 360f)]
	public float _RandomRangeSize = 360f;

	public float _RandomSpeedMin = 1f;

	public float _RandomSpeedMax = 3f;

	public float _RandomDelayMin = 0.01f;

	public float _RandomDelayMax = 0.1f;

	public bool _EvenlyDistribute = true;
}
