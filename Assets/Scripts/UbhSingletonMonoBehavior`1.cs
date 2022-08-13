using System;
using UnityEngine;

public class UbhSingletonMonoBehavior<T> : MonoBehaviour where T : MonoBehaviour
{
	public static T Instance
	{
		get
		{
			if (UbhSingletonMonoBehavior<T>._Instance == null)
			{
				UbhSingletonMonoBehavior<T>._Instance = UnityEngine.Object.FindObjectOfType<T>();
				if (UbhSingletonMonoBehavior<T>._Instance == null)
				{
					UbhSingletonMonoBehavior<T>._Instance = new GameObject(typeof(T).Name).AddComponent<T>();
				}
			}
			return UbhSingletonMonoBehavior<T>._Instance;
		}
	}

	protected GameObject _GameObject
	{
		get
		{
			if (this._MyGameObject == null)
			{
				this._MyGameObject = base.gameObject;
			}
			return this._MyGameObject;
		}
	}

	protected Transform _Transform
	{
		get
		{
			if (this._MyTransform == null)
			{
				this._MyTransform = base.transform;
			}
			return this._MyTransform;
		}
	}

	protected virtual void Awake()
	{
		if (this != UbhSingletonMonoBehavior<T>.Instance)
		{
			GameObject gameObject = base.gameObject;
			UnityEngine.Object.Destroy(this);
			UnityEngine.Object.Destroy(gameObject);
			return;
		}
	}

	private static T _Instance;

	private GameObject _MyGameObject;

	private Transform _MyTransform;
}
