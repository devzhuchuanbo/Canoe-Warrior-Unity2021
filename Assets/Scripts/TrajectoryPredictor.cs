using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[AddComponentMenu("Physics/Trajectory Predictor")]
public class TrajectoryPredictor : MonoBehaviour
{
	private void Start()
	{
		if (this.predictionType == TrajectoryPredictor.predictionMode.Prediction2D)
		{
			this.rb2 = base.GetComponent<Rigidbody2D>();
		}
		else
		{
			this.rb = base.GetComponent<Rigidbody>();
		}
	}

	private void Update()
	{
		if (!this.started)
		{
			this.started = true;
			if (this.drawDebugOnStart)
			{
				if (this.rb || this.rb2)
				{
					bool flag = this.drawDebugOnPrediction;
					this.drawDebugOnPrediction = true;
					if (this.predictionType == TrajectoryPredictor.predictionMode.Prediction2D)
					{
						this.Predict2D(this.rb2);
					}
					else
					{
						this.Predict3D(this.rb);
					}
					this.drawDebugOnPrediction = flag;
				}
				else
				{
					UnityEngine.Debug.LogWarning("Debug on object start requires a rigibody on the object.");
				}
			}
			else if (this.drawDebugOnUpdate && !this.rb && !this.rb2)
			{
				UnityEngine.Debug.LogWarning("Debug on object update requires a rigibody on the object.");
				this.drawDebugOnUpdate = false;
			}
		}
		if (this.drawDebugOnUpdate && (this.rb || this.rb2))
		{
			this.frameCount++;
			if (this.frameCount % (this.debugLineUpdateRate + 1) == 0)
			{
				this.frameCount = 0;
				this.debugLineDuration = Time.unscaledDeltaTime * ((float)this.debugLineUpdateRate + 1f);
				bool flag2 = this.drawDebugOnPrediction;
				this.drawDebugOnPrediction = true;
				if (this.predictionType == TrajectoryPredictor.predictionMode.Prediction2D)
				{
					if (this.rb2.velocity.magnitude > 0.1f)
					{
						this.Predict2D(this.rb2);
					}
				}
				else if (this.rb.velocity.magnitude > 0.1f)
				{
					this.Predict3D(this.rb);
				}
				this.drawDebugOnPrediction = flag2;
			}
		}
	}

	private void LineDebug(List<Vector3> pointList)
	{
		base.StopAllCoroutines();
		if (this.debugLineObj)
		{
			UnityEngine.Object.Destroy(this.debugLineObj);
		}
		this.debugLineObj = new GameObject();
		this.debugLineObj.name = "Debug Line";
		if (base.transform)
		{
			this.debugLineObj.transform.SetParent(base.transform);
		}
		LineRenderer lineRenderer = this.debugLineObj.AddComponent<LineRenderer>();
		lineRenderer.SetColors(this.lineStartColor, this.lineEndColor);
		lineRenderer.SetWidth(this.lineWidth, this.lineWidth);
		lineRenderer.useWorldSpace = true;
		lineRenderer.receiveShadows = false;
		lineRenderer.shadowCastingMode = ShadowCastingMode.Off;
		if (lineRenderer.sortingLayerID != 0 || lineRenderer.sortingOrder != 0)
		{
			UnityEngine.Debug.Log("sl = " + lineRenderer.sortingLayerID);
			UnityEngine.Debug.Log("so = " + lineRenderer.sortingOrder);
		}
		Shader shader;
		if (this.lineShader)
		{
			shader = this.lineShader;
		}
		else
		{
			shader = Shader.Find("Particles/Alpha Blended");
		}
		if (shader)
		{
			lineRenderer.sharedMaterial = new Material(shader);
		}
		if (this.lineTexture)
		{
			lineRenderer.sharedMaterial.mainTexture = this.lineTexture;
			lineRenderer.sharedMaterial.mainTextureScale = new Vector2(this.lineDistance / this.lineWidth / 2f * this.textureTilingMult, 1f);
		}
		lineRenderer.SetVertexCount(pointList.Count);
		base.StartCoroutine(this.KillLineDelay(this.debugLineDuration));
	}

	private void OnDestroy()
	{
		if (this.debugLineObj)
		{
			UnityEngine.Object.Destroy(this.debugLineObj);
		}
	}

	private IEnumerator KillLineDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
		if (this.debugLineObj)
		{
			UnityEngine.Object.Destroy(this.debugLineObj);
		}
		yield break;
	}

	public void Predict3D(Vector3 startPos, Vector3 velocity, Vector3 gravity, float linearDrag = 0f)
	{
		this.drag = linearDrag;
		this.vel = velocity;
		this.pos = startPos;
		this.grav = gravity;
		this.PerformPrediction();
	}

	public void Predict2D(Vector3 startPos, Vector2 velocity, Vector2 gravity, float linearDrag = 0f)
	{
		this.drag = linearDrag;
		this.vel = velocity;
		this.pos = startPos;
		this.grav = gravity;
		this.PerformPrediction();
	}

	public void Predict3D(Rigidbody rb)
	{
		this.drag = rb.drag;
		this.vel = rb.velocity;
		this.pos = rb.position;
		this.grav = Physics.gravity;
		this.PerformPrediction();
	}

	public void Predict2D(Rigidbody2D rb)
	{
		this.drag = rb.drag;
		this.vel = rb.velocity;
		this.pos = rb.position;
		this.grav = Physics.gravity;
		this.PerformPrediction();
	}

	public static Vector3[] GetPoints3D(Vector3 startPos, Vector3 velocity, Vector3 gravity, float linearDrag = 0f, float accuracy = 0.985f, int iterationLimit = 150, bool stopOnCollision = true)
	{
		GameObject gameObject = new GameObject();
		gameObject.name = "TrajectoryPredictionObj";
		TrajectoryPredictor trajectoryPredictor = gameObject.AddComponent<TrajectoryPredictor>();
		trajectoryPredictor.accuracy = accuracy;
		trajectoryPredictor.iterationLimit = iterationLimit;
		trajectoryPredictor.stopOnCollision = stopOnCollision;
		trajectoryPredictor.Predict3D(startPos, velocity, gravity, linearDrag);
		UnityEngine.Object.Destroy(gameObject);
		return trajectoryPredictor.predictionPoints.ToArray();
	}

	public static Vector3[] GetPoints2D(Vector3 startPos, Vector2 velocity, Vector2 gravity, float linearDrag = 0f, float accuracy = 0.985f, int iterationLimit = 150, bool stopOnCollision = true)
	{
		GameObject gameObject = new GameObject();
		gameObject.name = "TrajectoryPredictionObj";
		TrajectoryPredictor trajectoryPredictor = gameObject.AddComponent<TrajectoryPredictor>();
		trajectoryPredictor.accuracy = accuracy;
		trajectoryPredictor.iterationLimit = iterationLimit;
		trajectoryPredictor.stopOnCollision = stopOnCollision;
		trajectoryPredictor.predictionType = TrajectoryPredictor.predictionMode.Prediction2D;
		trajectoryPredictor.Predict2D(startPos, velocity, gravity, linearDrag);
		UnityEngine.Object.Destroy(gameObject);
		return trajectoryPredictor.predictionPoints.ToArray();
	}

	public static Vector3[] GetPoints3D(Rigidbody rb, float accuracy = 0.985f, int iterationLimit = 150, bool stopOnCollision = true)
	{
		GameObject gameObject = new GameObject();
		gameObject.name = "TrajectoryPredictionObj";
		TrajectoryPredictor trajectoryPredictor = gameObject.AddComponent<TrajectoryPredictor>();
		trajectoryPredictor.accuracy = accuracy;
		trajectoryPredictor.iterationLimit = iterationLimit;
		trajectoryPredictor.stopOnCollision = stopOnCollision;
		trajectoryPredictor.Predict3D(rb);
		UnityEngine.Object.Destroy(gameObject);
		return trajectoryPredictor.predictionPoints.ToArray();
	}

	public static Vector3[] GetPoints2D(Rigidbody2D rb, float accuracy = 0.985f, int iterationLimit = 150, bool stopOnCollision = true)
	{
		GameObject gameObject = new GameObject();
		gameObject.name = "TrajectoryPredictionObj";
		TrajectoryPredictor trajectoryPredictor = gameObject.AddComponent<TrajectoryPredictor>();
		trajectoryPredictor.accuracy = accuracy;
		trajectoryPredictor.iterationLimit = iterationLimit;
		trajectoryPredictor.stopOnCollision = stopOnCollision;
		trajectoryPredictor.predictionType = TrajectoryPredictor.predictionMode.Prediction2D;
		trajectoryPredictor.Predict2D(rb);
		UnityEngine.Object.Destroy(gameObject);
		return trajectoryPredictor.predictionPoints.ToArray();
	}

	public static RaycastHit GetHitInfo3D(Vector3 startPos, Vector3 velocity, Vector3 gravity, float linearDrag = 0f, float accuracy = 0.985f, int iterationLimit = 150, bool stopOnCollision = true)
	{
		GameObject gameObject = new GameObject();
		gameObject.name = "TrajectoryPredictionObj";
		TrajectoryPredictor trajectoryPredictor = gameObject.AddComponent<TrajectoryPredictor>();
		trajectoryPredictor.accuracy = accuracy;
		trajectoryPredictor.iterationLimit = iterationLimit;
		trajectoryPredictor.stopOnCollision = stopOnCollision;
		trajectoryPredictor.Predict3D(startPos, velocity, gravity, linearDrag);
		UnityEngine.Object.Destroy(gameObject);
		return trajectoryPredictor.hitInfo3D;
	}

	public static RaycastHit GetHitInfo3D(Rigidbody rb, float accuracy = 0.985f, int iterationLimit = 150, bool stopOnCollision = true)
	{
		GameObject gameObject = new GameObject();
		gameObject.name = "TrajectoryPredictionObj";
		TrajectoryPredictor trajectoryPredictor = gameObject.AddComponent<TrajectoryPredictor>();
		trajectoryPredictor.accuracy = accuracy;
		trajectoryPredictor.iterationLimit = iterationLimit;
		trajectoryPredictor.stopOnCollision = stopOnCollision;
		trajectoryPredictor.Predict3D(rb);
		UnityEngine.Object.Destroy(gameObject);
		return trajectoryPredictor.hitInfo3D;
	}

	public static RaycastHit2D GetHitInfo2D(Vector3 startPos, Vector2 velocity, Vector2 gravity, float linearDrag = 0f, float accuracy = 0.985f, int iterationLimit = 150, bool stopOnCollision = true)
	{
		GameObject gameObject = new GameObject();
		gameObject.name = "TrajectoryPredictionObj";
		TrajectoryPredictor trajectoryPredictor = gameObject.AddComponent<TrajectoryPredictor>();
		trajectoryPredictor.accuracy = accuracy;
		trajectoryPredictor.iterationLimit = iterationLimit;
		trajectoryPredictor.stopOnCollision = stopOnCollision;
		trajectoryPredictor.predictionType = TrajectoryPredictor.predictionMode.Prediction2D;
		trajectoryPredictor.Predict2D(startPos, velocity, gravity, linearDrag);
		UnityEngine.Object.Destroy(gameObject);
		return trajectoryPredictor.hitInfo2D;
	}

	public static RaycastHit2D GetHitInfo2D(Rigidbody2D rb, float accuracy = 0.985f, int iterationLimit = 150, bool stopOnCollision = true)
	{
		GameObject gameObject = new GameObject();
		gameObject.name = "TrajectoryPredictionObj";
		TrajectoryPredictor trajectoryPredictor = gameObject.AddComponent<TrajectoryPredictor>();
		trajectoryPredictor.accuracy = accuracy;
		trajectoryPredictor.iterationLimit = iterationLimit;
		trajectoryPredictor.stopOnCollision = stopOnCollision;
		trajectoryPredictor.predictionType = TrajectoryPredictor.predictionMode.Prediction2D;
		trajectoryPredictor.Predict2D(rb);
		UnityEngine.Object.Destroy(gameObject);
		return trajectoryPredictor.hitInfo2D;
	}

	private void PerformPrediction()
	{
		Vector3 vector = Vector3.zero;
		bool flag = false;
		int num = 0;
		this.lineDistance = 0f;
		float num2 = 1f - this.accuracy;
		Vector3 b = this.grav * num2;
		float d = Mathf.Clamp01(1f - this.drag * num2);
		this.predictionPoints.Clear();
		while (!flag && num < this.iterationLimit)
		{
			this.vel += b;
			this.vel *= d;
			Vector3 vector2 = this.pos + this.vel * num2;
			vector = vector2 - this.pos;
			this.predictionPoints.Add(this.pos);
			float num3 = Vector3.Distance(this.pos, vector2);
			this.lineDistance += num3;
			if (this.stopOnCollision)
			{
				if (this.predictionType == TrajectoryPredictor.predictionMode.Prediction2D)
				{
					RaycastHit2D hit = Physics2D.Raycast(this.pos, vector, num3);
					if (hit && hit.collider.transform && hit.collider.transform != base.transform)
					{
						this.hitInfo2D = hit;
						flag = true;
						this.predictionPoints.Add(hit.point);
					}
				}
				else
				{
					Ray ray = new Ray(this.pos, vector);
					RaycastHit raycastHit;
					if (Physics.Raycast(ray, out raycastHit, num3))
					{
						this.hitInfo3D = raycastHit;
						flag = true;
						this.predictionPoints.Add(raycastHit.point);
					}
				}
			}
			if (this.drawDebugOnPrediction && this.debugLineMode == TrajectoryPredictor.lineMode.DrawRayEditorOnly)
			{
				UnityEngine.Debug.DrawRay(this.pos, vector, this.lineStartColor, this.debugLineDuration);
			}
			this.pos = vector2;
			num++;
		}
		if (this.drawDebugOnPrediction && this.debugLineMode == TrajectoryPredictor.lineMode.LineRendererBoth)
		{
			this.LineDebug(this.predictionPoints);
		}
	}

	[Header("Prediction Settings")]
	[Tooltip("The accuracy of the prediction. This controls the distance between steps in calculation.")]
	[Range(0.7f, 0.9999f)]
	public float accuracy = 0.98f;

	[Tooltip("Limit on how many steps the prediction can take before stopping.")]
	public int iterationLimit = 150;

	[Tooltip("Whether the prediction should be a 2D or 3D line.")]
	public TrajectoryPredictor.predictionMode predictionType = TrajectoryPredictor.predictionMode.Prediction3D;

	[Tooltip("Stop the prediction where the line hits an object? Objects can be set to ignore this collision by putting them on the Ignore Raycast layer.")]
	public bool stopOnCollision = true;

	[Header("Line Settings")]
	[Tooltip("The type of line to draw for debug stuff. DrawRay: uses built in UnityEngine.Debug.DrawRay only visible in editor. LineRenderer: uses a line renderer on a separate created GameObject to draw the line, is visble in editor and play mode")]
	public TrajectoryPredictor.lineMode debugLineMode = TrajectoryPredictor.lineMode.LineRendererBoth;

	[Tooltip("Draw a debug line on object start? (Requires a rigidbody or rigidbody2D)")]
	public bool drawDebugOnStart;

	[Tooltip("Draw a debug line on object update? (Requires a rigidbody or rigidbody2D)")]
	public bool drawDebugOnUpdate;

	[Tooltip("Draw a debug line when predicting the trajectory")]
	public bool drawDebugOnPrediction;

	[Tooltip("Duration the prediction line lasts for. When predicting every frame its a good idea to update this value to Time.unscaledDeltaTime every frame.(This is done automatically if you use the drawDebugOnUpdate option)")]
	public float debugLineDuration = 4f;

	[Tooltip("Number of frames that pass before the line is refreshed. Increasing this number could significantly improve performance with a large amount of lines being predicted at once.(Only used if drawDebugOnUpdate is enabled.)")]
	[Range(0f, 10f)]
	public int debugLineUpdateRate;

	[Tooltip("The layer the line is drawn on.")]
	public int lineSortingLayer;

	[Tooltip("The order in the sorting layer the line is drawn.")]
	public int lineSortingOrder;

	[Tooltip("Thickness of the debug line when using the line renderer mode.")]
	[Header("Line Appearance")]
	public float lineWidth = 0.05f;

	[Tooltip("Start color of the debug line")]
	public Color lineStartColor = Color.white;

	[Tooltip("End color of the debug line")]
	public Color lineEndColor = Color.white;

	[Tooltip("If provided, this shader will be used on the LineRenderer. (Recommended is the particles section of shaders)")]
	public Shader lineShader;

	[Tooltip("If provided, this texture will be added to the material of the LineRenderer. (A couple textures come packaged with the script)")]
	public Texture lineTexture;

	[Tooltip("Value to scale the tiling of the line texture by.")]
	public float textureTilingMult = 1f;

	[HideInInspector]
	public RaycastHit hitInfo3D;

	[HideInInspector]
	public RaycastHit2D hitInfo2D;

	[HideInInspector]
	public List<Vector3> predictionPoints = new List<Vector3>();

	private Rigidbody rb;

	private Rigidbody2D rb2;

	private bool started;

	private int frameCount;

	private GameObject debugLineObj;

	private float drag;

	private Vector3 vel;

	private Vector3 pos;

	private Vector3 grav;

	private float lineDistance;

	public enum lineMode
	{
		DrawRayEditorOnly = 1,
		LineRendererBoth
	}

	public enum predictionMode
	{
		Prediction2D = 2,
		Prediction3D
	}
}
