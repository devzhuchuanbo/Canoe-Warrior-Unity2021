// dnSpy decompiler from Assembly-UnityScript.dll class: Red_UVScroller
using System;
using UnityEngine;

[Serializable]
public class Red_UVScroller : MonoBehaviour
{
	public Red_UVScroller()
	{
		this.speedY = 0.5f;
	}

	public virtual void Start()
	{
	}

	public virtual void Update()
	{
		this.timeWentY += Time.deltaTime * this.speedY;
		this.timeWentX += Time.deltaTime * this.speedX;
		this.GetComponent<Renderer>().materials[this.targetMaterialSlot].SetTextureOffset("_MainTex", new Vector2(this.timeWentX, this.timeWentY));
	}

	public virtual void OnEnable()
	{
		this.GetComponent<Renderer>().materials[this.targetMaterialSlot].SetTextureOffset("_MainTex", new Vector2((float)0, (float)0));
		this.timeWentX = (float)0;
		this.timeWentY = (float)0;
	}

	public virtual void Main()
	{
	}

	public int targetMaterialSlot;

	public float speedY;

	public float speedX;

	private float timeWentX;

	private float timeWentY;
}
