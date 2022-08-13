using System;
using System.Collections;
using UnityEngine;

public class UbhCoroutine : UbhSingletonMonoBehavior<UbhCoroutine>
{
	protected override void Awake()
	{
		base.Awake();
	}

	public static Coroutine StartIE(IEnumerator routine)
	{
		return UbhSingletonMonoBehavior<UbhCoroutine>.Instance.StartCoroutine(routine);
	}
}
