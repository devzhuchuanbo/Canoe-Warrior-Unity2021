using System;
using System.Collections;
using UnityEngine;

public class LaunchDelayDestroy : MonoBehaviour
{
	private void Start()
	{
		base.StartCoroutine(this.DestroyCo());
	}

	private IEnumerator DestroyCo()
	{
		yield return new WaitForSeconds(7.5f);
		UnityEngine.Object.Destroy(base.gameObject);
		yield break;
	}
}
