using System;
using System.Collections;
using UnityEngine;

public class UbhEmitter : UbhMonoBehaviour
{
	private IEnumerator Start()
	{
		if (this._Waves.Length == 0)
		{
			yield break;
		}
		this._Manager = UnityEngine.Object.FindObjectOfType<UbhManager>();
		for (;;)
		{
			while (!this._Manager.IsPlaying())
			{
				yield return 0;
			}
			GameObject wave = (GameObject)UnityEngine.Object.Instantiate(this._Waves[this._CurrentWave], base.transform.position, Quaternion.identity);
			wave.transform.parent = base.transform;
			while (0 < wave.transform.childCount)
			{
				yield return 0;
			}
			UnityEngine.Object.Destroy(wave);
			this._CurrentWave = (int)Mathf.Repeat((float)this._CurrentWave + 1f, (float)this._Waves.Length);
		}
		yield break;
	}

	[SerializeField]
	private GameObject[] _Waves;

	private int _CurrentWave;

	private UbhManager _Manager;
}
