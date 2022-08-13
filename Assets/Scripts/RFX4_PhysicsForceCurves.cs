using System;
using UnityEngine;

public class RFX4_PhysicsForceCurves : MonoBehaviour
{
	private void Awake()
	{
		this.t = base.transform;
	}

	private void OnEnable()
	{
		this.startTime = Time.time;
		this.canUpdate = true;
		this.forceAdditionalMultiplier = 1f;
	}

	private void FixedUpdate()
	{
		float num = Time.time - this.startTime;
		if (this.canUpdate)
		{
			float d = this.ForceCurve.Evaluate(num / this.GraphTimeMultiplier) * this.GraphIntensityMultiplier;
			Collider[] array = Physics.OverlapSphere(this.t.position, this.ForceRadius);
			foreach (Collider collider in array)
			{
				Rigidbody component = collider.GetComponent<Rigidbody>();
				if (!(component == null))
				{
					if (this.AffectedName.Length <= 0 || collider.name.Contains(this.AffectedName))
					{
						Vector3 vector;
						float num2;
						if (this.UseUPVector)
						{
							vector = Vector3.up;
							num2 = 1f - Mathf.Clamp01(collider.transform.position.y - this.t.position.y);
							num2 *= 1f - (collider.transform.position - this.t.position).magnitude / this.ForceRadius;
						}
						else
						{
							vector = collider.transform.position - this.t.position;
							num2 = 1f - vector.magnitude / this.ForceRadius;
						}
						if (this.UseDistanceScale)
						{
							collider.transform.localScale = this.DistanceScaleCurve.Evaluate(num2) * collider.transform.localScale;
						}
						if (this.DestoryDistance > 0f && vector.magnitude < this.DestoryDistance)
						{
							UnityEngine.Object.Destroy(collider.gameObject);
						}
						component.AddForce(vector.normalized * num2 * this.ForceMultiplier * d * this.forceAdditionalMultiplier, this.ForceMode);
						if (this.DragGraphTimeMultiplier > 0f)
						{
							component.drag = this.DragCurve.Evaluate(num / this.DragGraphTimeMultiplier) * this.DragGraphIntensityMultiplier;
							component.angularDrag = component.drag / 10f;
						}
					}
				}
			}
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

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(base.transform.position, this.ForceRadius);
	}

	public float ForceRadius = 5f;

	public float ForceMultiplier = 1f;

	public AnimationCurve ForceCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

	public ForceMode ForceMode;

	public float GraphTimeMultiplier = 1f;

	public float GraphIntensityMultiplier = 1f;

	public bool IsLoop;

	public float DestoryDistance = -1f;

	public bool UseDistanceScale;

	public AnimationCurve DistanceScaleCurve = AnimationCurve.EaseInOut(1f, 1f, 1f, 1f);

	public bool UseUPVector;

	public AnimationCurve DragCurve = AnimationCurve.EaseInOut(0f, 0f, 0f, 1f);

	public float DragGraphTimeMultiplier = -1f;

	public float DragGraphIntensityMultiplier = -1f;

	public string AffectedName;

	[HideInInspector]
	public float forceAdditionalMultiplier = 1f;

	private bool canUpdate;

	private float startTime;

	private Transform t;
}
