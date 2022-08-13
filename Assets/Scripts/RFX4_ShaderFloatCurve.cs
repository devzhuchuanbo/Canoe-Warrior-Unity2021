using System;
using UnityEngine;

public class RFX4_ShaderFloatCurve : MonoBehaviour
{
	private void Awake()
	{
		Renderer component = base.GetComponent<Renderer>();
		if (component == null)
		{
			Projector component2 = base.GetComponent<Projector>();
			if (component2 != null)
			{
				if (!this.UseSharedMaterial)
				{
					if (!component2.material.name.EndsWith("(Instance)"))
					{
						component2.material = new Material(component2.material)
						{
							name = component2.material.name + " (Instance)"
						};
					}
					this.mat = component2.material;
				}
				else
				{
					this.mat = component2.material;
				}
			}
		}
		else if (!this.UseSharedMaterial)
		{
			this.mat = component.material;
		}
		else
		{
			this.mat = component.sharedMaterial;
		}
		this.shaderProperty = this.ShaderFloatProperty.ToString();
		if (this.mat.HasProperty(this.shaderProperty))
		{
			this.propertyID = Shader.PropertyToID(this.shaderProperty);
		}
		this.startFloat = this.mat.GetFloat(this.propertyID);
		float value = this.FloatCurve.Evaluate(0f) * this.GraphIntensityMultiplier;
		this.mat.SetFloat(this.propertyID, value);
		this.isInitialized = true;
	}

	private void OnEnable()
	{
		this.startTime = Time.time;
		this.canUpdate = true;
		if (this.isInitialized)
		{
			float value = this.FloatCurve.Evaluate(0f) * this.GraphIntensityMultiplier;
			this.mat.SetFloat(this.propertyID, value);
		}
	}

	private void Update()
	{
		float num = Time.time - this.startTime;
		if (this.canUpdate)
		{
			float value = this.FloatCurve.Evaluate(num / this.GraphTimeMultiplier) * this.GraphIntensityMultiplier;
			this.mat.SetFloat(this.propertyID, value);
		}
		if (num >= this.GraphTimeMultiplier)
		{
			if (this.IsLoop)
			{
				this.startTime = Time.time;
			}
			else
			{
				this.canUpdate = false;
			}
		}
	}

	private void OnDisable()
	{
		if (this.UseSharedMaterial)
		{
			this.mat.SetFloat(this.propertyID, this.startFloat);
		}
	}

	private void OnDestroy()
	{
		if (!this.UseSharedMaterial)
		{
			if (this.mat != null)
			{
				UnityEngine.Object.DestroyImmediate(this.mat);
			}
			this.mat = null;
		}
	}

	public RFX4_ShaderProperties ShaderFloatProperty = RFX4_ShaderProperties._Cutoff;

	public AnimationCurve FloatCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

	public float GraphTimeMultiplier = 1f;

	public float GraphIntensityMultiplier = 1f;

	public bool IsLoop;

	public bool UseSharedMaterial;

	private bool canUpdate;

	private float startTime;

	private Material mat;

	private float startFloat;

	private int propertyID;

	private string shaderProperty;

	private bool isInitialized;
}
