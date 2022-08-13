using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class AutoDestruct : MonoBehaviour
{
	private void OnEnable()
	{
		base.StartCoroutine("CheckAlive");
	}

	private IEnumerator CheckAlive()
	{
		ParticleSystem tg = base.GetComponent<ParticleSystem>();
		while (true && tg != null)
		{
			yield return new WaitForSeconds(0.5f);
			if (!tg.IsAlive(true))
			{
				if (this.Deactive)
				{
					base.gameObject.SetActive(false);
				}
				else
				{
					UnityEngine.Object.Destroy(base.gameObject);
				}
				break;
			}
		}
		yield break;
	}

	public bool Deactive;
}
