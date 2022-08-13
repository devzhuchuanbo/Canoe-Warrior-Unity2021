using System;
using UnityEngine;

public class RFX4_ReplaceMaterialByTime : MonoBehaviour
{
	private void Start()
	{
		this.isInitialized = true;
		this.mshRend = base.GetComponent<MeshRenderer>();
		this.mat = this.mshRend.sharedMaterial;
		base.Invoke("ReplaceObject", this.TimeDelay);
	}

	private void OnEnable()
	{
		if (this.isInitialized)
		{
			this.mshRend.sharedMaterial = this.mat;
			base.Invoke("ReplaceObject", this.TimeDelay);
		}
	}

	private void ReplaceObject()
	{
		this.mshRend.sharedMaterial = this.ReplacementMaterial;
	}

	public Material ReplacementMaterial;

	public float TimeDelay = 1f;

	public bool ChangeShadow = true;

	private bool isInitialized;

	private Material mat;

	private MeshRenderer mshRend;
}
