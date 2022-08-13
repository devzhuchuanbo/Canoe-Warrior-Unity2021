using System;
using UnityEngine;

public class RFX4_EffectEvent : MonoBehaviour
{
	private void OnEnable()
	{
		if (this.Effect != null)
		{
			this.Effect.SetActive(false);
		}
		if (this.AdditionalEffect != null)
		{
			this.AdditionalEffect.SetActive(false);
		}
		if (this.CharacterEffect != null)
		{
			this.CharacterEffect.SetActive(false);
		}
		if (this.CharacterEffect2 != null)
		{
			this.CharacterEffect2.SetActive(false);
		}
	}

	public void ActivateEffect()
	{
		if (this.Effect == null)
		{
			return;
		}
		this.Effect.SetActive(true);
	}

	public void ActivateAdditionalEffect()
	{
		if (this.AdditionalEffect == null)
		{
			return;
		}
		this.AdditionalEffect.SetActive(true);
	}

	public void ActivateCharacterEffect()
	{
		if (this.CharacterEffect == null)
		{
			return;
		}
		this.CharacterEffect.SetActive(true);
	}

	public void ActivateCharacterEffect2()
	{
		if (this.CharacterEffect2 == null)
		{
			return;
		}
		this.CharacterEffect2.SetActive(true);
	}

	private void LateUpdate()
	{
		if (this.Effect != null && this.AttachPoint != null)
		{
			this.Effect.transform.position = this.AttachPoint.position;
		}
		if (this.AdditionalEffect != null && this.AdditionalEffectAttachPoint != null)
		{
			this.AdditionalEffect.transform.position = this.AdditionalEffectAttachPoint.position;
		}
		if (this.CharacterEffect != null && this.CharacterAttachPoint != null)
		{
			this.CharacterEffect.transform.position = this.CharacterAttachPoint.position;
		}
		if (this.CharacterEffect2 != null && this.CharacterAttachPoint2 != null)
		{
			this.CharacterEffect2.transform.position = this.CharacterAttachPoint2.position;
		}
	}

	public GameObject CharacterEffect;

	public Transform CharacterAttachPoint;

	public GameObject CharacterEffect2;

	public Transform CharacterAttachPoint2;

	public GameObject Effect;

	public Transform AttachPoint;

	public GameObject AdditionalEffect;

	public Transform AdditionalEffectAttachPoint;
}
