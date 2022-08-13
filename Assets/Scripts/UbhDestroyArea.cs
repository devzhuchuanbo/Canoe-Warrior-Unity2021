using System;
using UnityEngine;

public class UbhDestroyArea : UbhMonoBehaviour
{
	private void Start()
	{
		if (this._ColCenter == null || this._ColTop == null || this._ColBottom == null || this._ColRight == null || this._ColLeft == null)
		{
			return;
		}
		UbhManager ubhManager = UnityEngine.Object.FindObjectOfType<UbhManager>();
		if (ubhManager != null && ubhManager._ScaleToFit)
		{
			Vector2 a = Camera.main.ViewportToWorldPoint(new Vector2(1f, 1f));
			Vector2 size = a * 2f;
			size.x += 0.5f;
			size.y += 0.5f;
			Vector2 zero = Vector2.zero;
			this._ColCenter.size = size;
			this._ColTop.size = size;
			zero.x = this._ColTop.offset.x;
			zero.y = size.y;
			this._ColTop.offset = zero;
			this._ColBottom.size = size;
			zero.x = this._ColBottom.offset.x;
			zero.y = -size.y;
			this._ColBottom.offset = zero;
			Vector2 zero2 = Vector2.zero;
			zero2.x = size.y;
			zero2.y = size.x;
			this._ColRight.size = zero2;
			zero.x = size.x / 2f + zero2.x / 2f;
			zero.y = this._ColRight.offset.y;
			this._ColRight.offset = zero;
			this._ColLeft.size = zero2;
			zero.x = -(size.x / 2f) - zero2.x / 2f;
			zero.y = this._ColLeft.offset.y;
			this._ColLeft.offset = zero;
		}
		this._ColCenter.enabled = this._UseCenterCollider;
		this._ColTop.enabled = !this._UseCenterCollider;
		this._ColBottom.enabled = !this._UseCenterCollider;
		this._ColRight.enabled = !this._UseCenterCollider;
		this._ColLeft.enabled = !this._UseCenterCollider;
	}

	private void OnTriggerEnter2D(Collider2D c)
	{
		if (this._UseCenterCollider)
		{
			return;
		}
		this.HitCheck(c.transform);
	}

	private void OnTriggerExit2D(Collider2D c)
	{
		if (!this._UseCenterCollider)
		{
			return;
		}
		this.HitCheck(c.transform);
	}

	private void OnTriggerEnter(Collider c)
	{
		if (this._UseCenterCollider)
		{
			return;
		}
		this.HitCheck(c.transform);
	}

	private void OnTriggerExit(Collider c)
	{
		if (!this._UseCenterCollider)
		{
			return;
		}
		this.HitCheck(c.transform);
	}

	private void HitCheck(Transform colTrans)
	{
		string name = colTrans.name;
		if (name.Contains("EnemyBullet") || name.Contains("PlayerBullet"))
		{
			UbhSingletonMonoBehavior<UbhObjectPool>.Instance.ReleaseGameObject(colTrans.parent.gameObject, false);
		}
		else if (!name.Contains("Player"))
		{
			UnityEngine.Object.Destroy(colTrans.gameObject);
		}
	}

	[SerializeField]
	private bool _UseCenterCollider;

	[SerializeField]
	private BoxCollider2D _ColCenter;

	[SerializeField]
	private BoxCollider2D _ColTop;

	[SerializeField]
	private BoxCollider2D _ColBottom;

	[SerializeField]
	private BoxCollider2D _ColRight;

	[SerializeField]
	private BoxCollider2D _ColLeft;
}
