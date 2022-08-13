using System;
using UnityEngine;

[ExecuteInEditMode]
public class ParticleScaler : MonoBehaviour
{
	private void Start()
	{
		this.prevScale = this.particleScale;
	}

	private void Update()
	{
	}

	private void ScaleShurikenSystems(float scaleFactor)
	{
	}

	private void ScaleLegacySystems(float scaleFactor)
	{
	}

	private void ScaleTrailRenderers(float scaleFactor)
	{
		TrailRenderer[] componentsInChildren = base.GetComponentsInChildren<TrailRenderer>();
		foreach (TrailRenderer trailRenderer in componentsInChildren)
		{
			trailRenderer.startWidth *= scaleFactor;
			trailRenderer.endWidth *= scaleFactor;
		}
	}

	public float particleScale = 1f;

	public bool alsoScaleGameobject = true;

	private float prevScale;
}
