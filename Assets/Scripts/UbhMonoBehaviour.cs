using System;
using UnityEngine;

public abstract class UbhMonoBehaviour : MonoBehaviour
{
	public new Transform transform
	{
		get
		{
			if (this._Transform == null)
			{
				this._Transform = base.GetComponent<Transform>();
			}
			return this._Transform;
		}
	}

	public Renderer renderer
	{
		get
		{
			if (this._Renderer == null)
			{
				this._Renderer = base.GetComponent<Renderer>();
			}
			return this._Renderer;
		}
	}

	public Rigidbody rigidbody
	{
		get
		{
			if (this._Rigidbody == null)
			{
				this._Rigidbody = base.GetComponent<Rigidbody>();
			}
			return this._Rigidbody;
		}
	}

	public Rigidbody2D rigidbody2D
	{
		get
		{
			if (this._Rigidbody2D == null)
			{
				this._Rigidbody2D = base.GetComponent<Rigidbody2D>();
			}
			return this._Rigidbody2D;
		}
	}

	private Transform _Transform;

	private Renderer _Renderer;

	private Rigidbody _Rigidbody;

	private Rigidbody2D _Rigidbody2D;
}
