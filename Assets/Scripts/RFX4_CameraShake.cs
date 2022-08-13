using System;
using System.Collections;
using UnityEngine;

public class RFX4_CameraShake : MonoBehaviour
{
	private void PlayShake()
	{
		base.StopAllCoroutines();
		base.StartCoroutine(this.Shake());
	}

	private void Update()
	{
		if (this.isPlaying && this.IsEnabled)
		{
			this.isPlaying = false;
			this.PlayShake();
		}
	}

	private void OnEnable()
	{
		this.isPlaying = true;
		RFX4_CameraShake[] array = UnityEngine.Object.FindObjectsOfType(typeof(RFX4_CameraShake)) as RFX4_CameraShake[];
		if (array != null)
		{
			foreach (RFX4_CameraShake rfx4_CameraShake in array)
			{
				rfx4_CameraShake.canUpdate = false;
			}
		}
		this.canUpdate = true;
	}

	private IEnumerator Shake()
	{
		float elapsed = 0f;
		Transform camT = Camera.main.transform;
		Vector3 originalCamRotation = camT.rotation.eulerAngles;
		Vector3 direction = (base.transform.position - camT.position).normalized;
		float time = 0f;
		float randomStart = UnityEngine.Random.Range(-1000f, 1000f);
		float distanceDamper = 1f - Mathf.Clamp01((camT.position - base.transform.position).magnitude / this.DistanceForce);
		Vector3 oldRotation = Vector3.zero;
		while (elapsed < this.Duration && this.canUpdate)
		{
			elapsed += Time.deltaTime;
			float percentComplete = elapsed / this.Duration;
			float damper = this.ShakeCurve.Evaluate(percentComplete) * distanceDamper;
			time += Time.deltaTime * damper;
			camT.position -= direction * Time.deltaTime * Mathf.Sin(time * this.Speed) * damper * this.Magnitude / 2f;
			float alpha = randomStart + this.Speed * percentComplete / 10f;
			float x = Mathf.PerlinNoise(alpha, 0f) * 2f - 1f;
			float y = Mathf.PerlinNoise(1000f + alpha, alpha + 1000f) * 2f - 1f;
			float z = Mathf.PerlinNoise(0f, alpha) * 2f - 1f;
			if (Quaternion.Euler(originalCamRotation + oldRotation) != camT.rotation)
			{
				originalCamRotation = camT.rotation.eulerAngles;
			}
			oldRotation = Mathf.Sin(time * this.Speed) * damper * this.Magnitude * new Vector3(0.5f + y, 0.3f + x, 0.3f + z) * this.RotationDamper;
			camT.rotation = Quaternion.Euler(originalCamRotation + oldRotation);
			yield return null;
		}
		yield break;
	}

	public AnimationCurve ShakeCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);

	public float Duration = 2f;

	public float Speed = 22f;

	public float Magnitude = 1f;

	public float DistanceForce = 100f;

	public float RotationDamper = 2f;

	public bool IsEnabled = true;

	private bool isPlaying;

	[HideInInspector]
	public bool canUpdate;
}
