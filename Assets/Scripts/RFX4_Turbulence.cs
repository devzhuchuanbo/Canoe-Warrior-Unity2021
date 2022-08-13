using System;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(ParticleSystem))]
public class RFX4_Turbulence : MonoBehaviour
{
	private void Start()
	{
		this.t = base.transform;
		this.particleSys = base.GetComponent<ParticleSystem>();
		if (this.particleArray == null || this.particleArray.Length < this.particleSys.maxParticles)
		{
			this.particleArray = new ParticleSystem.Particle[this.particleSys.maxParticles];
		}
		this.perfomanceOldSettings = this.Perfomance;
		this.UpdatePerfomanceSettings();
	}

	private void OnEnable()
	{
		this.currentDelay = 0f;
	}

	private void Update()
	{
		if (!Application.isPlaying)
		{
			this.deltaTime = Time.realtimeSinceStartup - this.lastStopTime;
			this.lastStopTime = Time.realtimeSinceStartup;
		}
		else
		{
			this.deltaTime = Time.deltaTime;
		}
		this.currentDelay += this.deltaTime;
		if (this.currentDelay < this.TimeDelay)
		{
			return;
		}
		if (!this.UseGlobalOffset)
		{
			this.currentOffset += this.OffsetSpeed * this.deltaTime;
		}
		else if (Application.isPlaying)
		{
			this.currentOffset = this.OffsetSpeed * Time.time;
		}
		else
		{
			this.currentOffset = this.OffsetSpeed * Time.realtimeSinceStartup;
		}
		if (this.Perfomance != this.perfomanceOldSettings)
		{
			this.perfomanceOldSettings = this.Perfomance;
			this.UpdatePerfomanceSettings();
		}
		this.time += this.deltaTime;
		if (QualitySettings.vSyncCount == 2)
		{
			this.UpdateTurbulence();
		}
		else if (QualitySettings.vSyncCount == 1)
		{
			if (this.Perfomance == RFX4_Turbulence.PerfomanceEnum.Low)
			{
				if (this.skipFrame)
				{
					this.UpdateTurbulence();
				}
				this.skipFrame = !this.skipFrame;
			}
			if (this.Perfomance == RFX4_Turbulence.PerfomanceEnum.High)
			{
				this.UpdateTurbulence();
			}
		}
		else if (QualitySettings.vSyncCount == 0)
		{
			if (this.time >= this.fpsTime)
			{
				this.time = 0f;
				this.UpdateTurbulence();
				this.deltaTimeLastUpdateOffset = 0f;
			}
			else
			{
				this.deltaTimeLastUpdateOffset += this.deltaTime;
			}
		}
	}

	private void UpdatePerfomanceSettings()
	{
		if (this.Perfomance == RFX4_Turbulence.PerfomanceEnum.High)
		{
			this.FPS = 80;
			this.splitUpdate = 2;
		}
		if (this.Perfomance == RFX4_Turbulence.PerfomanceEnum.Low)
		{
			this.FPS = 40;
			this.splitUpdate = 2;
		}
		this.fpsTime = 1f / (float)this.FPS;
	}

	private void UpdateTurbulence()
	{
		int particles = this.particleSys.GetParticles(this.particleArray);
		int num = 1;
		int num2;
		int num3;
		if (this.splitUpdate > 1)
		{
			num2 = particles / this.splitUpdate * this.currentSplit;
			num3 = Mathf.CeilToInt((float)particles * 1f / (float)this.splitUpdate * ((float)this.currentSplit + 1f));
			num = this.splitUpdate;
		}
		else
		{
			num2 = 0;
			num3 = particles;
		}
		for (int i = num2; i < num3; i++)
		{
			ParticleSystem.Particle particle = this.particleArray[i];
			float num4 = 1f;
			if (this.TurbulenceByTime)
			{
				num4 = this.TurbulenceStrengthByTime.Evaluate(1f - particle.remainingLifetime / particle.startLifetime);
			}
			if (this.ThreshholdSpeed > 1E-07f && num4 < this.ThreshholdSpeed)
			{
				return;
			}
			Vector3 position = particle.position;
			position.x /= this.Frequency.x + 1E-07f;
			position.y /= this.Frequency.y + 1E-07f;
			position.z /= this.Frequency.z + 1E-07f;
			Vector3 a = default(Vector3);
			float num5 = this.deltaTime + this.deltaTimeLastUpdateOffset;
			a.x = (Mathf.PerlinNoise(position.z - this.currentOffset.z, position.y - this.currentOffset.y) * 2f - 1f) * this.Amplitude.x * num5;
			a.y = (Mathf.PerlinNoise(position.x - this.currentOffset.x, position.z - this.currentOffset.z) * 2f - 1f) * this.Amplitude.y * num5;
			a.z = (Mathf.PerlinNoise(position.y - this.currentOffset.y, position.x - this.currentOffset.x) * 2f - 1f) * this.Amplitude.z * num5;
			float d = this.TurbulenceStrenght * num4 * (float)num;
			float d2 = 1f;
			float num6 = Mathf.Abs((particle.position - this.t.position).magnitude);
			if (this.AproximatedFlyDistance > 0f)
			{
				d2 = this.VelocityByDistance.Evaluate(Mathf.Clamp01(num6 / this.AproximatedFlyDistance));
			}
			a *= d;
			if (this.MoveMethod == RFX4_Turbulence.MoveMethodEnum.Position)
			{
				ParticleSystem.Particle[] array = this.particleArray;
				int num7 = i;
				array[num7].position = array[num7].position + a * d2;
			}
			if (this.MoveMethod == RFX4_Turbulence.MoveMethodEnum.Velocity)
			{
				ParticleSystem.Particle[] array2 = this.particleArray;
				int num8 = i;
				array2[num8].velocity = array2[num8].velocity + a * d2;
			}
			if (this.MoveMethod == RFX4_Turbulence.MoveMethodEnum.RelativePosition)
			{
				ParticleSystem.Particle[] array3 = this.particleArray;
				int num9 = i;
				array3[num9].position = array3[num9].position + a * this.particleArray[i].velocity.magnitude;
				this.particleArray[i].velocity = this.particleArray[i].velocity * 0.85f + a.normalized * 0.15f * d2 + this.GlobalForce * d2;
			}
		}
		this.particleSys.SetParticles(this.particleArray, particles);
		this.currentSplit++;
		if (this.currentSplit >= this.splitUpdate)
		{
			this.currentSplit = 0;
		}
	}

	public float TurbulenceStrenght = 1f;

	public bool TurbulenceByTime;

	public float TimeDelay;

	public AnimationCurve TurbulenceStrengthByTime = AnimationCurve.EaseInOut(1f, 1f, 1f, 1f);

	public Vector3 Frequency = new Vector3(1f, 1f, 1f);

	public Vector3 OffsetSpeed = new Vector3(0.5f, 0.5f, 0.5f);

	public Vector3 Amplitude = new Vector3(5f, 5f, 5f);

	public Vector3 GlobalForce;

	public bool UseGlobalOffset = true;

	public RFX4_Turbulence.MoveMethodEnum MoveMethod;

	public RFX4_Turbulence.PerfomanceEnum Perfomance;

	public float ThreshholdSpeed;

	public AnimationCurve VelocityByDistance = AnimationCurve.EaseInOut(0f, 1f, 1f, 1f);

	public float AproximatedFlyDistance = -1f;

	private float lastStopTime;

	private Vector3 currentOffset;

	private float deltaTime;

	private float deltaTimeLastUpdateOffset;

	private ParticleSystem.Particle[] particleArray;

	private ParticleSystem particleSys;

	private float time;

	private int currentSplit;

	private float fpsTime;

	private int FPS;

	private int splitUpdate = 2;

	private RFX4_Turbulence.PerfomanceEnum perfomanceOldSettings;

	private bool skipFrame;

	private Transform t;

	private float currentDelay;

	public enum MoveMethodEnum
	{
		Position,
		Velocity,
		RelativePosition
	}

	public enum PerfomanceEnum
	{
		High,
		Low
	}
}
