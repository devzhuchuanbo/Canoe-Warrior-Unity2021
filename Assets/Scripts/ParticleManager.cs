using System;
using UnityEngine;
using UnityEngine.UI;

public class ParticleManager : MonoBehaviour
{
	private void Start()
	{
		this.pLength = base.transform.childCount;
		this.particles = new GameObject[this.pLength];
		this.pTest = 0;
		foreach (object obj in base.gameObject.transform)
		{
			Transform transform = (Transform)obj;
			this.particles[this.pTest] = transform.gameObject;
			this.pTest++;
		}
		this.pLength = this.particles.Length;
		this.pCurrent = 0;
		this.particles[this.pCurrent].SetActive(true);
		this.pText.text = this.particles[this.pCurrent].name;
		if (this.disableObject)
		{
			this.goToDisable.SetActive(false);
		}
	}

	public void GoForward()
	{
		if (this.pCurrent + 1 < this.pLength)
		{
			this.particles[this.pCurrent].SetActive(false);
			this.particles[this.pCurrent + 1].SetActive(true);
			this.pCurrent++;
		}
		else
		{
			this.particles[this.pCurrent].SetActive(false);
			this.pCurrent = 0;
			this.particles[this.pCurrent].SetActive(true);
		}
		this.pText.text = this.particles[this.pCurrent].name;
	}

	public void GoBackward()
	{
		if (this.pCurrent > 0)
		{
			this.particles[this.pCurrent].SetActive(false);
			this.particles[this.pCurrent - 1].SetActive(true);
			this.pCurrent--;
		}
		else
		{
			this.particles[this.pCurrent].SetActive(false);
			this.pCurrent = this.pLength - 1;
			this.particles[this.pCurrent].SetActive(true);
		}
		this.pText.text = this.particles[this.pCurrent].name;
	}

	public int pLength;

	public int pCurrent;

	public Text pText;

	public int pTest;

	public GameObject[] particles;

	public bool disableObject;

	public GameObject goToDisable;
}
