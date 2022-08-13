using System;
using UnityEngine;

public class ActiveSelf : MonoBehaviour
{
	private void Awake()
	{
		this.childs = base.GetComponentsInChildren<Transform>();
	}

	private void OnEnable()
	{
		for (int i = 0; i < this.childs.Length; i++)
		{
			this.childs[i].gameObject.SetActive(true);
		}
		MonoBehaviour.print(this.childs.Length + "@@@@@@@");
	}

	private Transform[] childs;
}
