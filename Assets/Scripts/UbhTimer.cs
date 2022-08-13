using System;
using UnityEngine;

public class UbhTimer : UbhSingletonMonoBehavior<UbhTimer>
{
	public float DeltaTime
	{
		get
		{
			return (!this._Pausing) ? this._DeltaTime : 0f;
		}
	}

	public float FrameCount
	{
		get
		{
			return this._FrameCount;
		}
	}

	protected override void Awake()
	{
		this._LastTime = Time.time;
		base.Awake();
	}

	private void Update()
	{
		float time = Time.time;
		this._DeltaTime = time - this._LastTime;
		this._LastTime = time;
		if (!this._Pausing)
		{
			this._FrameCount += 1f;
		}
	}

	public void Pause()
	{
		this._Pausing = true;
	}

	public void Resume()
	{
		this._Pausing = false;
		this._LastTime = Time.time;
	}

	private float _LastTime;

	private float _DeltaTime;

	private float _FrameCount;

	private bool _Pausing;
}
