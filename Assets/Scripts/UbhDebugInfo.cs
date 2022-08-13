using System;
using UnityEngine;
using UnityEngine.UI;

public class UbhDebugInfo : UbhMonoBehaviour
{
	private void Start()
	{
		if (!Debug.isDebugBuild)
		{
			base.gameObject.SetActive(false);
			return;
		}
		this._LastUpdateTime = Time.realtimeSinceStartup;
	}

	private void Update()
	{
		if (this._FpsGUIText == null || this._BulletNumGUIText == null)
		{
			return;
		}
		this._Frame++;
		float num = Time.realtimeSinceStartup - this._LastUpdateTime;
		if (1f <= num)
		{
			float num2 = (float)this._Frame / num;
			this._FpsGUIText.text = "FPS : " + ((int)num2).ToString();
			this._LastUpdateTime = Time.realtimeSinceStartup;
			this._Frame = 0;
			if (this.objectPool == null)
			{
				this.objectPool = UnityEngine.Object.FindObjectOfType<UbhObjectPool>();
			}
			if (this.objectPool != null)
			{
				int activePooledObjectCount = this.objectPool.GetActivePooledObjectCount();
				this._BulletNumGUIText.text = "Bullet Num : " + activePooledObjectCount.ToString();
			}
		}
	}

	private const float INTERVAL_SEC = 1f;

	[SerializeField]
	private Text _FpsGUIText;

	[SerializeField]
	private Text _BulletNumGUIText;

	private UbhObjectPool objectPool;

	private float _LastUpdateTime;

	private int _Frame;
}
