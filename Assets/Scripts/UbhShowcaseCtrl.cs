using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UbhShowcaseCtrl : MonoBehaviour
{
	private void Start()
	{
		if (this._InitialPoolBulletPrefab == null)
		{
			return;
		}
		List<GameObject> list = new List<GameObject>();
		for (int i = 0; i < this._InitialPoolCount; i++)
		{
			GameObject gameObject = UbhSingletonMonoBehavior<UbhObjectPool>.Instance.GetGameObject(this._InitialPoolBulletPrefab, Vector3.zero, Quaternion.identity, true);
			if (gameObject == null)
			{
				break;
			}
			if (gameObject.GetComponent<UbhBullet>() == null)
			{
				gameObject.AddComponent<UbhBullet>();
			}
			list.Add(gameObject);
		}
		for (int j = 0; j < list.Count; j++)
		{
			UbhSingletonMonoBehavior<UbhObjectPool>.Instance.ReleaseGameObject(list[j], false);
		}
		if (this._GoShotCtrlList != null)
		{
			for (int k = 0; k < this._GoShotCtrlList.Length; k++)
			{
				this._GoShotCtrlList[k].SetActive(false);
			}
		}
		this._NowIndex = -1;
		this.ChangeShot(true);
	}

	private void Update()
	{
	}

	private void OnGUI()
	{
		this._RectArea.x = 0f;
		this._RectArea.y = 0f;
		this._RectArea.width = (float)Screen.width;
		this._RectArea.height = (float)Screen.height;
		GUILayout.BeginArea(this._RectArea);
		float num = (float)Screen.width / 600f;
		float num2 = (float)Screen.height / 450f;
		float num3 = (Screen.height >= Screen.width) ? num : num2;
		GUIStyle none = GUIStyle.none;
		none.fontSize = (int)(22f * num3);
		none.normal.textColor = Color.white;
		none.alignment = TextAnchor.MiddleCenter;
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		GUILayout.Label("No." + (this._NowIndex + 1).ToString() + " : " + this._NowGoName, none, new GUILayoutOption[]
		{
			GUILayout.Width((float)Screen.width),
			GUILayout.Height((float)Screen.height * 0.15f)
		});
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		if (GUILayout.Button("<< PREV", new GUILayoutOption[]
		{
			GUILayout.Width((float)Screen.width / 4f),
			GUILayout.Height((float)Screen.height * 0.1f)
		}))
		{
			this.ChangeShot(false);
		}
		GUILayout.FlexibleSpace();
		GUILayout.FlexibleSpace();
		if (GUILayout.Button("NEXT >>", new GUILayoutOption[]
		{
			GUILayout.Width((float)Screen.width / 4f),
			GUILayout.Height((float)Screen.height * 0.1f)
		}))
		{
			this.ChangeShot(true);
		}
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.EndArea();
	}

	private void ChangeShot(bool toNext)
	{
		if (this._GoShotCtrlList == null)
		{
			return;
		}
		base.StopAllCoroutines();
		if (0 <= this._NowIndex && this._NowIndex < this._GoShotCtrlList.Length)
		{
			this._GoShotCtrlList[this._NowIndex].SetActive(false);
		}
		if (toNext)
		{
			this._NowIndex = (int)Mathf.Repeat((float)this._NowIndex + 1f, (float)this._GoShotCtrlList.Length);
		}
		else
		{
			this._NowIndex--;
			if (this._NowIndex < 0)
			{
				this._NowIndex = this._GoShotCtrlList.Length - 1;
			}
		}
		if (0 <= this._NowIndex && this._NowIndex < this._GoShotCtrlList.Length)
		{
			this._GoShotCtrlList[this._NowIndex].SetActive(true);
			this._NowGoName = this._GoShotCtrlList[this._NowIndex].name;
			base.StartCoroutine(this.StartShot());
		}
	}

	private IEnumerator StartShot()
	{
		float cntTimer = 0f;
		while (cntTimer < 1f)
		{
			cntTimer += UbhSingletonMonoBehavior<UbhTimer>.Instance.DeltaTime;
			yield return 0;
		}
		yield return 0;
		UbhShotCtrl shotCtrl = this._GoShotCtrlList[this._NowIndex].GetComponent<UbhShotCtrl>();
		if (shotCtrl != null)
		{
			shotCtrl.StartShotRoutine();
		}
		yield break;
	}

	[SerializeField]
	private GameObject _InitialPoolBulletPrefab;

	[SerializeField]
	private int _InitialPoolCount = 1000;

	[SerializeField]
	private GameObject[] _GoShotCtrlList;

	private Rect _RectArea = new Rect(0f, 0f, 0f, 0f);

	private int _NowIndex;

	private string _NowGoName;
}
