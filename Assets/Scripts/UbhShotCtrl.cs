using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("UniBulletHell/Controller/Shot Controller")]
public class UbhShotCtrl : UbhMonoBehaviour
{
	private IEnumerator Start()
	{
		if (this._StartOnAwake)
		{
			if (0f < this._StartOnAwakeDelay)
			{
				yield return base.StartCoroutine(UbhUtil.WaitForSeconds(this._StartOnAwakeDelay));
			}
			this.StartShotRoutine();
		}
		yield break;
	}

	private void OnDisable()
	{
		this._Shooting = false;
	}

	public void StartShotRoutine()
	{
		base.StartCoroutine(this.ShotCoroutine());
	}

	private IEnumerator ShotCoroutine()
	{
		if (this._ShotList == null || this._ShotList.Count <= 0)
		{
			UnityEngine.Debug.LogWarning("Cannot shot because ShotList is not set.");
			yield break;
		}
		bool enableShot = false;
		for (int i = 0; i < this._ShotList.Count; i++)
		{
			if (this._ShotList[i]._ShotObj != null)
			{
				enableShot = true;
				break;
			}
		}
		bool enableDelay = false;
		for (int j = 0; j < this._ShotList.Count; j++)
		{
			if (0f < this._ShotList[j]._AfterDelay)
			{
				enableDelay = true;
				break;
			}
		}
		if (!enableShot || !enableDelay)
		{
			if (!enableShot)
			{
				UnityEngine.Debug.LogWarning("Cannot shot because all ShotObj of ShotList is not set.");
			}
			if (!enableDelay)
			{
				UnityEngine.Debug.LogWarning("Cannot shot because all AfterDelay of ShotList is zero.");
			}
			yield break;
		}
		if (this._Shooting)
		{
			yield break;
		}
		this._Shooting = true;
		List<UbhShotCtrl.ShotInfo> tmpShotInfoList = new List<UbhShotCtrl.ShotInfo>(this._ShotList);
		int nowIndex = 0;
		for (;;)
		{
			if (this._AtRandom)
			{
				nowIndex = UnityEngine.Random.Range(0, tmpShotInfoList.Count);
			}
			if (tmpShotInfoList[nowIndex]._ShotObj != null)
			{
				tmpShotInfoList[nowIndex]._ShotObj.SetShotCtrl(this);
				tmpShotInfoList[nowIndex]._ShotObj.Shot();
			}
			if (0f < tmpShotInfoList[nowIndex]._AfterDelay)
			{
				yield return base.StartCoroutine(UbhUtil.WaitForSeconds(tmpShotInfoList[nowIndex]._AfterDelay));
			}
			if (this._AtRandom)
			{
				tmpShotInfoList.RemoveAt(nowIndex);
				if (tmpShotInfoList.Count <= 0)
				{
					if (!this._Loop)
					{
						break;
					}
					tmpShotInfoList = new List<UbhShotCtrl.ShotInfo>(this._ShotList);
				}
			}
			else
			{
				if (!this._Loop && tmpShotInfoList.Count - 1 <= nowIndex)
				{
					break;
				}
				nowIndex = (int)Mathf.Repeat((float)nowIndex + 1f, (float)tmpShotInfoList.Count);
			}
		}
		this._Shooting = false;
		yield break;
	}

	public void StopShotRoutine()
	{
		base.StopAllCoroutines();
		this._Shooting = false;
	}

	public UbhUtil.AXIS _AxisMove;

	public bool _StartOnAwake = true;

	public float _StartOnAwakeDelay = 1f;

	public bool _Loop = true;

	public bool _AtRandom;

	public List<UbhShotCtrl.ShotInfo> _ShotList = new List<UbhShotCtrl.ShotInfo>();

	private bool _Shooting;

	[Serializable]
	public class ShotInfo
	{
		public UbhBaseShot _ShotObj;

		public float _AfterDelay;
	}
}
