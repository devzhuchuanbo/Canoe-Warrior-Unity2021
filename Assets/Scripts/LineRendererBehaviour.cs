using System;
using UnityEngine;

public class LineRendererBehaviour : MonoBehaviour
{
	private void Start()
	{
		this.tRoot = this.StartPoint;
		this.line = base.GetComponent<LineRenderer>();
		this.InitializeDefault();
		this.isInitializedOnStart = true;
	}

	private void InitializeDefault()
	{
		base.GetComponent<Renderer>().material.SetFloat("_Chanel", (float)this.currentShaderIndex);
		this.currentShaderIndex++;
		if (this.currentShaderIndex == 3)
		{
			this.currentShaderIndex = 0;
		}
		this.line.SetPosition(0, this.tRoot.position);
		if (this.IsVertical)
		{
			if (Physics.Raycast(this.tRoot.position, Vector3.down, out this.hit))
			{
				this.line.SetPosition(1, this.hit.point);
				if (this.StartGlow != null)
				{
					this.StartGlow.transform.position = this.tRoot.position;
				}
				if (this.HitGlow != null)
				{
					this.HitGlow.transform.position = this.hit.point;
				}
				if (this.GoLight != null)
				{
					this.GoLight.transform.position = this.hit.point + new Vector3(0f, this.LightHeightOffset, 0f);
				}
				if (this.Particles != null)
				{
					this.Particles.transform.position = this.hit.point + new Vector3(0f, this.ParticlesHeightOffset, 0f);
				}
				if (this.Explosion != null)
				{
					this.Explosion.transform.position = this.hit.point + new Vector3(0f, this.ParticlesHeightOffset, 0f);
				}
			}
			else
			{
				Vector3 position = this.tRoot.position + new Vector3(0f, -10f, 0f);
				this.line.SetPosition(1, position);
			}
		}
		else
		{
			Vector3 direction = this.tTarget.position - this.tRoot.position;
			if (Physics.Raycast(this.tRoot.position, direction, out this.hit))
			{
				this.line.SetPosition(1, this.hit.point);
				if (this.StartGlow != null)
				{
					this.StartGlow.transform.position = this.tRoot.position;
				}
				if (this.HitGlow != null)
				{
					this.HitGlow.transform.position = this.hit.point;
				}
				if (this.GoLight != null)
				{
					this.GoLight.transform.position = this.hit.point + new Vector3(0f, this.LightHeightOffset, 0f);
				}
				if (this.Particles != null)
				{
					this.Particles.transform.position = this.hit.point + new Vector3(0f, this.ParticlesHeightOffset, 0f);
				}
				if (this.Explosion != null)
				{
					this.Explosion.transform.position = this.hit.point + new Vector3(0f, this.ParticlesHeightOffset, 0f);
				}
			}
		}
		CollisionInfo e = new CollisionInfo
		{
			Hit = this.hit
		};
		if (this.hit.transform != null)
		{
			ShieldCollisionBehaviour component = this.hit.transform.GetComponent<ShieldCollisionBehaviour>();
			if (component != null)
			{
				component.ShieldCollisionEnter(e);
			}
		}
	}

	private void OnEnable()
	{
		if (this.isInitializedOnStart)
		{
			this.InitializeDefault();
		}
	}

	public Transform StartPoint;

	public Transform tTarget;

	public bool IsVertical;

	public float LightHeightOffset = 0.3f;

	public float ParticlesHeightOffset = 0.2f;

	public float TimeDestroyLightAfterCollision = 4f;

	public float TimeDestroyThisAfterCollision = 4f;

	public float TimeDestroyRootAfterCollision = 4f;

	public GameObject EffectOnHitObject;

	public GameObject Explosion;

	public GameObject StartGlow;

	public GameObject HitGlow;

	public GameObject Particles;

	public GameObject GoLight;

	public float MoveDistance = 20f;

	public float ColliderRadius = 0.2f;

	private Transform tRoot;

	private bool isInitializedOnStart;

	private LineRenderer line;

	private int currentShaderIndex;

	private RaycastHit hit;
}
