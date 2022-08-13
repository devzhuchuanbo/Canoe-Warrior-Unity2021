using System;
using System.Collections;
using UnityEngine;

[AddComponentMenu("UniBulletHell/Shot Pattern/Homing Shot")]
public class UbhHomingShot : UbhBaseShot
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
			if (this._TargetTransform == null && this._SetTargetFromTag)
			{
				this._TargetTransform = UbhUtil.GetTransformFromTagName(this._TargetTagName);
			}
			float angle = UbhUtil.GetAngleFromTwoPosition(base.transform, this._TargetTransform, base.ShotCtrl._AxisMove);
			base.ShotBullet(bullet, this._BulletSpeed, angle, true, this._TargetTransform, this._HomingAngleSpeed, false, 0f, 0f);
			base.AutoReleaseBulletGameObject(bullet.gameObject);
		}
		base.FinishedShot();
		yield break;
	}

	public float _BetweenDelay = 0.1f;

	public float _HomingAngleSpeed = 20f;

	public bool _SetTargetFromTag = true;

	public string _TargetTagName = "Player";

	public Transform _TargetTransform;
}
