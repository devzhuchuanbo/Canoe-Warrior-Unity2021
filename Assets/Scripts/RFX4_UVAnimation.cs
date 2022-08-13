using System;
using UnityEngine;

public class RFX4_UVAnimation : MonoBehaviour
{
	private void OnEnable()
	{
		if (this.isInitialized)
		{
			this.InitDefaultVariables();
		}
	}

	private void Start()
	{
		this.InitDefaultVariables();
		this.isInitialized = true;
	}

	private void Update()
	{
		if (!this.canUpdate)
		{
			return;
		}
		this.UpdateMaterial();
		this.SetSpriteAnimation();
		if (this.IsInterpolateFrames)
		{
			this.SetSpriteAnimationIterpolated();
		}
	}

	private void InitDefaultVariables()
	{
		this.InitializeMaterial();
		this.totalFrames = this.TilesX * this.TilesY;
		this.previousIndex = 0;
		this.canUpdate = true;
		this.count = this.TilesY * this.TilesX;
		Vector3 zero = Vector3.zero;
		this.StartFrameOffset -= this.StartFrameOffset / this.count * this.count;
		this.size = new Vector2(1f / (float)this.TilesX, 1f / (float)this.TilesY);
		this.animationStartTime = Time.time;
		if (this.instanceMaterial != null)
		{
			foreach (RFX4_TextureShaderProperties rfx4_TextureShaderProperties in this.TextureNames)
			{
				this.instanceMaterial.SetTextureScale(rfx4_TextureShaderProperties.ToString(), this.size);
				this.instanceMaterial.SetTextureOffset(rfx4_TextureShaderProperties.ToString(), zero);
			}
		}
	}

	private void InitializeMaterial()
	{
		this.currentRenderer = base.GetComponent<Renderer>();
		if (this.currentRenderer == null)
		{
			this.projector = base.GetComponent<Projector>();
			if (this.projector != null)
			{
				if (!this.projector.material.name.EndsWith("(Instance)"))
				{
					this.projector.material = new Material(this.projector.material)
					{
						name = this.projector.material.name + " (Instance)"
					};
				}
				this.instanceMaterial = this.projector.material;
			}
		}
		else
		{
			this.instanceMaterial = this.currentRenderer.material;
		}
	}

	private void UpdateMaterial()
	{
		if (this.currentRenderer == null)
		{
			if (this.projector != null)
			{
				if (!this.projector.material.name.EndsWith("(Instance)"))
				{
					this.projector.material = new Material(this.projector.material)
					{
						name = this.projector.material.name + " (Instance)"
					};
				}
				this.instanceMaterial = this.projector.material;
			}
		}
		else
		{
			this.instanceMaterial = this.currentRenderer.material;
		}
	}

	private void SetSpriteAnimation()
	{
		int num = (int)((Time.time - this.animationStartTime) * (float)this.FPS);
		num %= this.totalFrames;
		if (!this.IsLoop && num < this.previousIndex)
		{
			this.canUpdate = false;
			return;
		}
		if (this.IsInterpolateFrames && num != this.previousIndex)
		{
			this.currentInterpolatedTime = 0f;
		}
		this.previousIndex = num;
		if (this.IsReverse)
		{
			num = this.totalFrames - num - 1;
		}
		int num2 = num % this.TilesX;
		int num3 = num / this.TilesX;
		float x = (float)num2 * this.size.x;
		float y = 1f - this.size.y - (float)num3 * this.size.y;
		Vector2 offset = new Vector2(x, y);
		if (this.instanceMaterial != null)
		{
			foreach (RFX4_TextureShaderProperties rfx4_TextureShaderProperties in this.TextureNames)
			{
				this.instanceMaterial.SetTextureScale(rfx4_TextureShaderProperties.ToString(), this.size);
				this.instanceMaterial.SetTextureOffset(rfx4_TextureShaderProperties.ToString(), offset);
			}
		}
	}

	private void SetSpriteAnimationIterpolated()
	{
		this.currentInterpolatedTime += Time.deltaTime;
		int num = this.previousIndex + 1;
		if (num == this.totalFrames)
		{
			num = this.previousIndex;
		}
		if (this.IsReverse)
		{
			num = this.totalFrames - num - 1;
		}
		int num2 = num % this.TilesX;
		int num3 = num / this.TilesX;
		float x = (float)num2 * this.size.x;
		float y = 1f - this.size.y - (float)num3 * this.size.y;
		Vector2 vector = new Vector2(x, y);
		if (this.instanceMaterial != null)
		{
			this.instanceMaterial.SetVector("_Tex_NextFrame", new Vector4(this.size.x, this.size.y, vector.x, vector.y));
			this.instanceMaterial.SetFloat("InterpolationValue", Mathf.Clamp01(this.currentInterpolatedTime * (float)this.FPS));
		}
	}

	public int TilesX = 4;

	public int TilesY = 4;

	public int FPS = 30;

	public int StartFrameOffset;

	public bool IsLoop = true;

	public float StartDelay;

	public bool IsReverse;

	public bool IsInterpolateFrames;

	public RFX4_TextureShaderProperties[] TextureNames = new RFX4_TextureShaderProperties[1];

	public AnimationCurve FrameOverTime = AnimationCurve.Linear(0f, 1f, 1f, 1f);

	private int count;

	private Renderer currentRenderer;

	private Projector projector;

	private Material instanceMaterial;

	private float animationStartTime;

	private bool canUpdate;

	private int previousIndex;

	private int totalFrames;

	private float currentInterpolatedTime;

	private int currentIndex;

	private Vector2 size;

	private bool isInitialized;
}
