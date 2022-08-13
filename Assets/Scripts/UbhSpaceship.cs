using System;
using UnityEngine;

public class UbhSpaceship : UbhMonoBehaviour
{
	private void Start()
	{
		this._Animator = base.GetComponent<Animator>();
	}

	public void Explosion()
	{
		if (this._ExplosionPrefab != null)
		{
			UnityEngine.Object.Instantiate(this._ExplosionPrefab, base.transform.position, base.transform.rotation);
		}
	}

	public Animator GetAnimator()
	{
		return this._Animator;
	}

	public float _Speed;

	[SerializeField]
	private GameObject _ExplosionPrefab;

	private Animator _Animator;
}
