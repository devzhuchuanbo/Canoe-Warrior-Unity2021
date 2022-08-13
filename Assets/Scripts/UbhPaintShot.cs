using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("UniBulletHell/Shot Pattern/Paint Shot")]
public class UbhPaintShot : UbhBaseShot
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
		if (this._BulletSpeed <= 0f || this._PaintDataText == null)
		{
			UnityEngine.Debug.LogWarning("Cannot shot because BulletSpeed or PaintDataText is not set.");
			yield break;
		}
		if (this._Shooting)
		{
			yield break;
		}
		this._Shooting = true;
		List<List<int>> paintData = this.LoadPaintData();
		float paintStartAngle = this._PaintCenterAngle;
		if (0 < paintData.Count)
		{
			paintStartAngle -= ((paintData[0].Count % 2 != 0) ? (this._BetweenAngle * Mathf.Floor((float)paintData[0].Count / 2f)) : (this._BetweenAngle * (float)paintData[0].Count / 2f + this._BetweenAngle / 2f));
		}
		for (int lineCnt = 0; lineCnt < paintData.Count; lineCnt++)
		{
			List<int> line = paintData[lineCnt];
			if (0 < lineCnt && 0f < this._NextLineDelay)
			{
				yield return base.StartCoroutine(UbhUtil.WaitForSeconds(this._NextLineDelay));
			}
			for (int i = 0; i < line.Count; i++)
			{
				if (line[i] == 1)
				{
					UbhBullet bullet = base.GetBullet(base.transform.position, base.transform.rotation, false);
					if (bullet == null)
					{
						break;
					}
					float angle = paintStartAngle + this._BetweenAngle * (float)i;
					base.ShotBullet(bullet, this._BulletSpeed, angle, false, null, 0f, false, 0f, 0f);
					base.AutoReleaseBulletGameObject(bullet.gameObject);
				}
			}
		}
		base.FinishedShot();
		yield break;
	}

	private List<List<int>> LoadPaintData()
	{
		List<List<int>> list = new List<List<int>>();
		if (string.IsNullOrEmpty(this._PaintDataText.text))
		{
			UnityEngine.Debug.LogWarning("Cannot load paint data because PaintDataText file is empty.");
			return list;
		}
		string[] array = this._PaintDataText.text.Split(UbhPaintShot.SPLIT_VAL, StringSplitOptions.RemoveEmptyEntries);
		for (int i = 0; i < array.Length; i++)
		{
			if (!array[i].StartsWith("#"))
			{
				list.Add(new List<int>());
				for (int j = 0; j < array[i].Length; j++)
				{
					list[list.Count - 1].Add((array[i][j] != '*') ? 0 : 1);
				}
			}
		}
		list.Reverse();
		return list;
	}

	private static readonly string[] SPLIT_VAL = new string[]
	{
		"\n",
		"\r",
		"\r\n"
	};

	public TextAsset _PaintDataText;

	[Range(0f, 360f)]
	public float _PaintCenterAngle = 180f;

	[Range(0f, 360f)]
	public float _BetweenAngle = 3f;

	public float _NextLineDelay = 0.1f;
}
