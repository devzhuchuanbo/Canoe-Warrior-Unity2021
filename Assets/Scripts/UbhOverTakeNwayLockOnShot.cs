using System;
using UnityEngine;

[AddComponentMenu("UniBulletHell/Shot Pattern/Over Take nWay Shot (Lock On)")]
public class UbhOverTakeNwayLockOnShot : UbhOverTakeNwayShot
{
	protected override void Awake()
	{
		base.Awake();
	}

	public override void Shot()
	{
		if (this._TargetTransform == null && this._SetTargetFromTag)
		{
			this._TargetTransform = UbhUtil.GetTransformFromTagName(this._TargetTagName);
		}
		if (this._TargetTransform == null)
		{
			UnityEngine.Debug.LogWarning("Cannot shot because TargetTransform is not set.");
			return;
		}
		this._CenterAngle = UbhUtil.GetAngleFromTwoPosition(base.transform, this._TargetTransform, base.ShotCtrl._AxisMove);
		base.Shot();
	}

	public bool _SetTargetFromTag = true;

	public string _TargetTagName = "Player";

	public Transform _TargetTransform;
}
