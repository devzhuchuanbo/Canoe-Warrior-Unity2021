using System;
using UnityEngine;

public class UbhBackground : UbhMonoBehaviour
{
	private void Start()
	{
		UbhManager ubhManager = UnityEngine.Object.FindObjectOfType<UbhManager>();
		if (ubhManager != null && ubhManager._ScaleToFit)
		{
			Vector2 a = Camera.main.ViewportToWorldPoint(new Vector2(1f, 1f));
			Vector2 v = a * 2f;
			base.transform.localScale = v;
		}
	}

	private void Update()
	{
		float y = Mathf.Repeat(Time.time * this._Speed, 1f);
		this._Offset.x = 0f;
		this._Offset.y = y;
		base.renderer.sharedMaterial.SetTextureOffset("_MainTex", this._Offset);
	}

	private const string TEX_OFFSET_PROPERTY = "_MainTex";

	[SerializeField]
	private float _Speed = 0.1f;

	private Vector2 _Offset = Vector2.zero;
}
