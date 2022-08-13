using System;
using System.Collections;
using UnityEngine;

[AddComponentMenu("UniBulletHell/Shot Pattern/Linear Shot (Lock On)")]
public class UbhLinearLockOnShot : UbhLinearShot
{
	protected override void Awake()
	{
		base.Awake();
	}

	public override void Shot()
	{
		if (this._Shooting)
		{
			return;
		}
		this.AimTarget();
		if (this._TargetTransform == null)
		{
			UnityEngine.Debug.LogWarning("Cannot shot because TargetTransform is not set.");
			return;
		}
		base.Shot();
		if (this._Aiming)
		{
			base.StartCoroutine(this.AimingCoroutine());
		}
	}

	private void AimTarget()
	{
		if (this._TargetTransform == null && this._SetTargetFromTag)
		{
			this._TargetTransform = UbhUtil.GetTransformFromTagName(this._TargetTagName);
		}
		if (this._TargetTransform != null)
		{
			this._Angle = UbhUtil.GetAngleFromTwoPosition(base.transform, this._TargetTransform, base.ShotCtrl._AxisMove);
		}
	}

	private IEnumerator AimingCoroutine()
	{
		while (this._Aiming)
		{
			if (!this._Shooting)
			{
				yield break;
			}
			this.AimTarget();
			yield return 0;
		}
		yield break;
	}

	public bool _SetTargetFromTag = true;

	public string _TargetTagName = "Player";

	public Transform _TargetTransform;

	public bool _Aiming;
}
