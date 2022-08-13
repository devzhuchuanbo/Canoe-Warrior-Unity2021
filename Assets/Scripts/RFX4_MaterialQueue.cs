using System;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
[ExecuteInEditMode]
public class RFX4_MaterialQueue : MonoBehaviour
{
	private void Start()
	{
		Renderer component = base.GetComponent<Renderer>();
		if (!component || !component.sharedMaterial || this.queues == null)
		{
			return;
		}
		component.sharedMaterial.renderQueue = this.queue;
		int num = 0;
		while (num < this.queues.Length && num < component.sharedMaterials.Length)
		{
			component.sharedMaterials[num].renderQueue = this.queues[num];
			num++;
		}
	}

	private void OnValidate()
	{
		this.Start();
	}

	private void Update()
	{
		if (Application.isPlaying)
		{
			return;
		}
		this.Start();
	}

	[Tooltip("Background=1000, Geometry=2000, AlphaTest=2450, Transparent=3000, Overlay=4000")]
	public int queue = 2000;

	public int[] queues;
}
