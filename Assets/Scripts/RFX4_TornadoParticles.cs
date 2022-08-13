using System;
using UnityEngine;

public class RFX4_TornadoParticles : MonoBehaviour
{
	private void Start()
	{
		this.particleSys = base.GetComponent<ParticleSystem>();
		this.myLight = base.GetComponent<Light>();
		if (this.particleSys != null)
		{
			this.particleArray = new ParticleSystem.Particle[this.particleSys.maxParticles];
		}
		if (this.TornadoMaterial.HasProperty("_TwistScale"))
		{
			this.materialID = Shader.PropertyToID("_TwistScale");
		}
		else
		{
			UnityEngine.Debug.Log(this.TornadoMaterial.name + " not have property twist");
		}
		if (this.materialID != -1)
		{
			this._twistScale = this.TornadoMaterial.GetVector(this.materialID);
		}
	}

	private void Update()
	{
		if (this.particleSys != null)
		{
			int particles = this.particleSys.GetParticles(this.particleArray);
			for (int i = 0; i < particles; i++)
			{
				Vector3 position = this.particleArray[i].position;
				float num = (position.y - base.transform.position.y) * this._twistScale.y;
				position.x = Mathf.Sin(Time.time * this._twistScale.z + position.y * this._twistScale.x) * num;
				position.z = Mathf.Sin(Time.time * this._twistScale.z + position.y * this._twistScale.x + 1.57075f) * num;
				this.particleArray[i].position = position;
				this.particleSys.SetParticles(this.particleArray, particles);
			}
		}
		if (this.myLight != null)
		{
			Vector3 localPosition = base.transform.localPosition;
			float num2 = localPosition.y * this._twistScale.y;
			localPosition.x = Mathf.Sin(Time.time * this._twistScale.z + localPosition.y * this._twistScale.x) * num2;
			localPosition.z = Mathf.Sin(Time.time * this._twistScale.z + localPosition.y * this._twistScale.x + 1.57075f) * num2;
			base.transform.localPosition = localPosition;
		}
	}

	public Material TornadoMaterial;

	private ParticleSystem.Particle[] particleArray;

	private ParticleSystem particleSys;

	private Light myLight;

	private Vector4 _twistScale;

	private int materialID = -1;
}
