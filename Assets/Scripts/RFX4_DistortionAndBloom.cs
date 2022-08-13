using System;
using UnityEngine;

[AddComponentMenu("KriptoFX/RFX4_BloomAndDistortion")]
[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class RFX4_DistortionAndBloom : MonoBehaviour
{
	public Material mat
	{
		get
		{
			if (this.m_Material == null)
			{
				this.m_Material = RFX4_DistortionAndBloom.CheckShaderAndCreateMaterial(Shader.Find("Hidden/KriptoFX/PostEffects/RFX4_Bloom"));
			}
			return this.m_Material;
		}
	}

	public Material matAdditive
	{
		get
		{
			if (this.m_MaterialAdditive == null)
			{
				this.m_MaterialAdditive = RFX4_DistortionAndBloom.CheckShaderAndCreateMaterial(Shader.Find("Hidden/KriptoFX/PostEffects/RFX4_BloomAdditive"));
				this.m_MaterialAdditive.renderQueue = 3900;
			}
			return this.m_MaterialAdditive;
		}
	}

	public static Material CheckShaderAndCreateMaterial(Shader s)
	{
		if (s == null || !s.isSupported)
		{
			return null;
		}
		return new Material(s)
		{
			hideFlags = HideFlags.DontSave
		};
	}

	private void OnDisable()
	{
		if (this.m_Material != null)
		{
			UnityEngine.Object.DestroyImmediate(this.m_Material);
		}
		this.m_Material = null;
		if (this.m_MaterialAdditive != null)
		{
			UnityEngine.Object.DestroyImmediate(this.m_MaterialAdditive);
		}
		this.m_MaterialAdditive = null;
		if (this._cameraInstance != null)
		{
			this._cameraInstance.gameObject.SetActive(false);
		}
	}

	private void OnDestroy()
	{
		if (this._cameraInstance != null)
		{
			UnityEngine.Object.DestroyImmediate(this._cameraInstance.gameObject);
		}
	}

	private void OnGUI()
	{
		if (Event.current.type.Equals(EventType.Repaint) && this.UseBloom && this.destination != null)
		{
			Graphics.DrawTexture(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), this.destination, this.matAdditive);
		}
	}

	private void Start()
	{
		this.InitializeRenderTarget();
	}

	private void LateUpdate()
	{
		if (this.previuosFrameWidth != Screen.width || this.previuosFrameHeight != Screen.height || Mathf.Abs(this.previousScale - this.RenderTextureResolutoinFactor) > 0.01f)
		{
			this.InitializeRenderTarget();
			this.previuosFrameWidth = Screen.width;
			this.previuosFrameHeight = Screen.height;
			this.previousScale = this.RenderTextureResolutoinFactor;
		}
		Shader.EnableKeyword("DISTORT_OFF");
		this.UpdateCameraCopy();
		if (this.UseBloom)
		{
			this.UpdateBloom();
		}
		Shader.SetGlobalTexture("_GrabTextureMobile", this.source);
		Shader.SetGlobalFloat("_GrabTextureMobileScale", this.RenderTextureResolutoinFactor);
		Shader.DisableKeyword("DISTORT_OFF");
	}

	private void InitializeRenderTarget()
	{
		int num = (int)((float)Screen.width * this.RenderTextureResolutoinFactor);
		int num2 = (int)((float)Screen.height * this.RenderTextureResolutoinFactor);
		this.source = new RenderTexture(num, num2, 16, RenderTextureFormat.DefaultHDR);
		if (this.UseBloom)
		{
			this.destination = new RenderTexture(((double)this.RenderTextureResolutoinFactor <= 0.99) ? (num / 2) : num, ((double)this.RenderTextureResolutoinFactor <= 0.99) ? (num2 / 2) : num2, 0, RenderTextureFormat.ARGB32);
		}
	}

	private void UpdateBloom()
	{
		bool isMobilePlatform = Application.isMobilePlatform;
		if (this.source == null)
		{
			return;
		}
		int num = this.source.width;
		int num2 = this.source.height;
		if (!this.HighQuality)
		{
			num /= 2;
			num2 /= 2;
		}
		RenderTextureFormat format = (!isMobilePlatform) ? RenderTextureFormat.DefaultHDR : RenderTextureFormat.Default;
		float num3 = Mathf.Log((float)num2, 2f) + this.Radius - 8f;
		int num4 = (int)num3;
		int num5 = Mathf.Clamp(num4, 1, 16);
		float num6 = Mathf.GammaToLinearSpace(this.Threshold);
		this.mat.SetFloat("_Threshold", num6);
		float num7 = num6 * this.SoftKnee + 1E-05f;
		Vector3 v = new Vector3(num6 - num7, num7 * 2f, 0.25f / num7);
		this.mat.SetVector("_Curve", v);
		bool flag = !this.HighQuality && this.AntiFlicker;
		this.mat.SetFloat("_PrefilterOffs", (!flag) ? 0f : -0.5f);
		this.mat.SetFloat("_SampleScale", 0.5f + num3 - (float)num4);
		this.mat.SetFloat("_Intensity", Mathf.Max(0f, this.Intensity));
		RenderTexture temporary = RenderTexture.GetTemporary(num, num2, 0, format);
		Graphics.Blit(this.source, temporary, this.mat, (!this.AntiFlicker) ? 0 : 1);
		RenderTexture renderTexture = temporary;
		for (int i = 0; i < num5; i++)
		{
			this.m_blurBuffer1[i] = RenderTexture.GetTemporary(renderTexture.width / 2, renderTexture.height / 2, 0, format);
			Graphics.Blit(renderTexture, this.m_blurBuffer1[i], this.mat, (i != 0) ? 4 : ((!this.AntiFlicker) ? 2 : 3));
			renderTexture = this.m_blurBuffer1[i];
		}
		for (int j = num5 - 2; j >= 0; j--)
		{
			RenderTexture renderTexture2 = this.m_blurBuffer1[j];
			this.mat.SetTexture("_BaseTex", renderTexture2);
			this.m_blurBuffer2[j] = RenderTexture.GetTemporary(renderTexture2.width, renderTexture2.height, 0, format);
			Graphics.Blit(renderTexture, this.m_blurBuffer2[j], this.mat, (!this.HighQuality) ? 5 : 6);
			renderTexture = this.m_blurBuffer2[j];
		}
		this.destination.DiscardContents();
		Graphics.Blit(renderTexture, this.destination, this.mat, (!this.HighQuality) ? 7 : 8);
		for (int k = 0; k < 16; k++)
		{
			if (this.m_blurBuffer1[k] != null)
			{
				RenderTexture.ReleaseTemporary(this.m_blurBuffer1[k]);
			}
			if (this.m_blurBuffer2[k] != null)
			{
				RenderTexture.ReleaseTemporary(this.m_blurBuffer2[k]);
			}
			this.m_blurBuffer1[k] = null;
			this.m_blurBuffer2[k] = null;
		}
		RenderTexture.ReleaseTemporary(temporary);
	}

	private void InitializeCameraCopy()
	{
		if (this._cameraInstance != null)
		{
			this._cameraInstance.gameObject.SetActive(true);
		}
		GameObject gameObject = GameObject.Find("RenderTextureCamera");
		if (gameObject == null)
		{
			this._cameraInstance = new GameObject("RenderTextureCamera")
			{
				transform = 
				{
					parent = Camera.main.transform
				}
			}.AddComponent<Camera>();
			this._cameraInstance.CopyFrom(Camera.main);
			this._cameraInstance.clearFlags = Camera.main.clearFlags;
			this._cameraInstance.depth -= 1f;
			this._cameraInstance.allowHDR = true;
			this._cameraInstance.targetTexture = this.source;
			Shader.SetGlobalTexture("_GrabTextureMobile", this.source);
			Shader.SetGlobalFloat("_GrabTextureMobileScale", this.RenderTextureResolutoinFactor);
			this._cameraInstance.Render();
		}
		else
		{
			this._cameraInstance = gameObject.GetComponent<Camera>();
		}
	}

	private void UpdateCameraCopy()
	{
		Camera camera = Camera.current;
		if (camera != null && camera.name == "SceneCamera")
		{
			if (this.source != null)
			{
				this.source.DiscardContents();
			}
			camera.targetTexture = this.source;
			camera.Render();
			camera.targetTexture = null;
			return;
		}
		camera = Camera.main;
		bool hdr = camera.allowHDR;
		if (this.source != null)
		{
			this.source.DiscardContents();
		}
		camera.allowHDR = true;
		camera.targetTexture = this.source;
		camera.Render();
		camera.allowHDR = hdr;
		camera.targetTexture = null;
	}

	private const string shaderName = "Hidden/KriptoFX/PostEffects/RFX4_Bloom";

	private const string shaderAdditiveName = "Hidden/KriptoFX/PostEffects/RFX4_BloomAdditive";

	private const int kMaxIterations = 16;

	[Range(0.05f, 1f)]
	[Tooltip("Camera render texture resolution")]
	public float RenderTextureResolutoinFactor = 0.25f;

	public bool UseBloom = true;

	[Tooltip("Filters out pixels under this level of brightness.")]
	[Range(0.1f, 3f)]
	public float Threshold = 2f;

	[SerializeField]
	[Range(0f, 1f)]
	[Tooltip("Makes transition between under/over-threshold gradual.")]
	public float SoftKnee;

	[Tooltip("Changes extent of veiling effects in A screen resolution-independent fashion.")]
	[Range(1f, 7f)]
	public float Radius = 7f;

	[Tooltip("Blend factor of the result image.")]
	public float Intensity = 1f;

	[Tooltip("Controls filter quality and buffer resolution.")]
	public bool HighQuality;

	[Tooltip("Reduces flashing noise with an additional filter.")]
	public bool AntiFlicker;

	private RenderTexture source;

	private RenderTexture destination;

	private int previuosFrameWidth;

	private int previuosFrameHeight;

	private float previousScale;

	private Camera _cameraInstance;

	private Material m_Material;

	private Material m_MaterialAdditive;

	private readonly RenderTexture[] m_blurBuffer1 = new RenderTexture[16];

	private readonly RenderTexture[] m_blurBuffer2 = new RenderTexture[16];
}
