using System;
using System.Collections.Generic;
using UnityEngine;

public class UbhObjectPool : UbhSingletonMonoBehavior<UbhObjectPool>
{
	protected override void Awake()
	{
		base.Awake();
	}

	public GameObject GetGameObject(GameObject prefab, Vector3 position, Quaternion rotation, bool forceInstantiate = false)
	{
		if (prefab == null)
		{
			return null;
		}
		int instanceID = prefab.GetInstanceID();
		if (!this._PooledKeyList.Contains(instanceID) && !this._PooledGoDic.ContainsKey(instanceID))
		{
			this._PooledKeyList.Add(instanceID);
			this._PooledGoDic.Add(instanceID, new List<GameObject>());
		}
		List<GameObject> list = this._PooledGoDic[instanceID];
		GameObject gameObject;
		if (!forceInstantiate)
		{
			for (int i = list.Count - 1; i >= 0; i--)
			{
				gameObject = list[i];
				if (gameObject == null)
				{
					list.Remove(gameObject);
				}
				else if (!gameObject.activeSelf)
				{
					Transform transform = gameObject.transform;
					transform.position = position;
					transform.rotation = rotation;
					gameObject.SetActive(true);
					return gameObject;
				}
			}
		}
		gameObject = (GameObject)UnityEngine.Object.Instantiate(prefab, position, rotation);
		gameObject.transform.parent = base._Transform;
		list.Add(gameObject);
		return gameObject;
	}

	public void ReleaseGameObject(GameObject go, bool destroy = false)
	{
		if (destroy)
		{
			UnityEngine.Object.Destroy(go);
			return;
		}
		go.SetActive(false);
	}

	public int GetActivePooledObjectCount()
	{
		int num = 0;
		for (int i = 0; i < this._PooledKeyList.Count; i++)
		{
			int key = this._PooledKeyList[i];
			List<GameObject> list = this._PooledGoDic[key];
			for (int j = 0; j < list.Count; j++)
			{
				GameObject gameObject = list[j];
				if (gameObject != null && gameObject.activeInHierarchy)
				{
					num++;
				}
			}
		}
		return num;
	}

	private List<int> _PooledKeyList = new List<int>();

	private Dictionary<int, List<GameObject>> _PooledGoDic = new Dictionary<int, List<GameObject>>();
}
