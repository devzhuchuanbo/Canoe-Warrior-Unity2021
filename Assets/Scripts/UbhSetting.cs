using System;
using UnityEngine;

public class UbhSetting : UbhMonoBehaviour
{
	private void Start()
	{
		this.SetValue();
	}

	private void OnValidate()
	{
		this.SetValue();
	}

	private void Update()
	{
		if (UbhUtil.IsMobilePlatform() && UnityEngine.Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	private void SetValue()
	{
		this._VsyncCount = Mathf.Clamp(this._VsyncCount, 0, 2);
		QualitySettings.vSyncCount = this._VsyncCount;
		this._FrameRate = Mathf.Clamp(this._FrameRate, 1, 120);
		Application.targetFrameRate = this._FrameRate;
	}

	[Range(0f, 2f)]
	public int _VsyncCount = 1;

	[Range(0f, 60f)]
	public int _FrameRate = 60;
}
