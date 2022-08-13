using System;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class RFX4_ParticleCollisionHandler : MonoBehaviour
{
	private void Start()
	{
		this.part = base.GetComponent<ParticleSystem>();
		this.collisionEvents = new ParticleCollisionEvent[16];
	}

	private void OnParticleCollision(GameObject other)
	{
		int safeCollisionEventSize = this.part.GetSafeCollisionEventSize();
		if (this.collisionEvents.Length < safeCollisionEventSize)
		{
			this.collisionEvents = new ParticleCollisionEvent[safeCollisionEventSize];
		}
		int num = this.part.GetCollisionEvents(other, this.collisionEvents);
		for (int i = 0; i < num; i++)
		{
			foreach (GameObject original in this.EffectsOnCollision)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(original, this.collisionEvents[i].intersection + this.collisionEvents[i].normal * this.Offset, default(Quaternion)) as GameObject;
				gameObject.transform.LookAt(this.collisionEvents[i].intersection + this.collisionEvents[i].normal);
				if (!this.UseWorldSpacePosition)
				{
					gameObject.transform.parent = base.transform;
				}
				UnityEngine.Object.Destroy(gameObject, this.DestroyTimeDelay);
			}
		}
	}

	public GameObject[] EffectsOnCollision;

	public float Offset;

	public float DestroyTimeDelay = 5f;

	public bool UseWorldSpacePosition;

	private ParticleSystem part;

	private ParticleCollisionEvent[] collisionEvents;

	private ParticleSystem ps;
}
