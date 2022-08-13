using System;
using System.Collections;
using UnityEngine;

public static class UbhUtil
{
	public static bool IsMobilePlatform()
	{
		return true;
	}

	public static IEnumerator WaitForSeconds(float waitTime)
	{
		float elapsedTime = 0f;
		while (elapsedTime < waitTime)
		{
			elapsedTime += UbhSingletonMonoBehavior<UbhTimer>.Instance.DeltaTime;
			yield return 0;
		}
		yield break;
	}

	public static Transform GetTransformFromTagName(string tagName)
	{
		if (string.IsNullOrEmpty(tagName))
		{
			return null;
		}
		GameObject gameObject = GameObject.FindWithTag(tagName);
		if (gameObject == null)
		{
			return null;
		}
		return gameObject.transform;
	}

	public static float GetShiftedAngle(int wayIndex, float baseAngle, float betweenAngle)
	{
		return (wayIndex % 2 != 0) ? (baseAngle + betweenAngle * Mathf.Ceil((float)wayIndex / 2f)) : (baseAngle - betweenAngle * (float)wayIndex / 2f);
	}

	public static float Get360Angle(float angle)
	{
		while (angle < 0f)
		{
			angle += 360f;
		}
		while (360f < angle)
		{
			angle -= 360f;
		}
		return angle;
	}

	public static float GetAngleFromTwoPosition(Transform fromTrans, Transform toTrans, UbhUtil.AXIS axisMove)
	{
		if (axisMove == UbhUtil.AXIS.X_AND_Y)
		{
			return UbhUtil.GetZangleFromTwoPosition(fromTrans, toTrans);
		}
		if (axisMove != UbhUtil.AXIS.X_AND_Z)
		{
			return 0f;
		}
		return UbhUtil.GetYangleFromTwoPosition(fromTrans, toTrans);
	}

	private static float GetZangleFromTwoPosition(Transform fromTrans, Transform toTrans)
	{
		if (fromTrans == null || toTrans == null)
		{
			return 0f;
		}
		float y = toTrans.position.x - fromTrans.position.x;
		float x = toTrans.position.y - fromTrans.position.y;
		float angle = Mathf.Atan2(y, x) * 57.29578f;
		return -UbhUtil.Get360Angle(angle);
	}

	private static float GetYangleFromTwoPosition(Transform fromTrans, Transform toTrans)
	{
		if (fromTrans == null || toTrans == null)
		{
			return 0f;
		}
		float y = toTrans.position.x - fromTrans.position.x;
		float x = toTrans.position.z - fromTrans.position.z;
		float angle = Mathf.Atan2(y, x) * 57.29578f;
		return -UbhUtil.Get360Angle(angle);
	}

	public enum AXIS
	{
		X_AND_Y,
		X_AND_Z
	}
}
