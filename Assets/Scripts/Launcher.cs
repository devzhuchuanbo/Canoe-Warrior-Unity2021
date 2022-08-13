using System;
using UnityEngine;

public class Launcher : MonoBehaviour
{
	private void Start()
	{
		Time.fixedDeltaTime = 0.01f;
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.T))
		{
			this.launch = true;
		}
		if (UnityEngine.Input.GetKey(KeyCode.Y))
		{
			this.launch = true;
		}
		if (this.launch)
		{
			this.launch = false;
			this.Launch();
			if (this.continuousLaunch)
			{
				this.launch = true;
			}
		}
	}

	private void Launch()
	{
		if (!this.launchObjParent)
		{
			this.launchObjParent = new GameObject();
			this.launchObjParent.name = "Launched Objects";
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.objToLaunch);
		gameObject.transform.SetParent(this.launchObjParent.transform);
		Rigidbody component = gameObject.GetComponent<Rigidbody>();
		gameObject.transform.position = base.transform.Find("LP").position;
		gameObject.transform.rotation = base.transform.Find("LP").rotation;
		component.AddRelativeForce(new Vector3(UnityEngine.Random.Range(0f, 10f), UnityEngine.Random.Range(0f, 10f), UnityEngine.Random.Range(this.forceRange.x, this.forceRange.y)));
		Renderer component2 = gameObject.GetComponent<Renderer>();
		component2.material = UnityEngine.Object.Instantiate<Material>(component2.material);
		component2.material.color = this.RandomColor();
		TrajectoryPredictor component3 = gameObject.GetComponent<TrajectoryPredictor>();
		component3.lineStartColor = component2.material.color;
		component3.lineEndColor = component2.material.color;
		switch (UnityEngine.Random.Range(0, 3))
		{
		case 0:
			component3.lineTexture = this.lineTextures[0];
			break;
		case 1:
			component3.lineTexture = this.lineTextures[1];
			component3.textureTilingMult = 0.35f;
			component3.lineWidth = 0.2f;
			break;
		case 2:
			component3.lineWidth = 0.1f;
			break;
		}
	}

	private Color RandomColor()
	{
		float r = UnityEngine.Random.Range(0f, 1f);
		float g = UnityEngine.Random.Range(0f, 1f);
		float b = UnityEngine.Random.Range(0f, 1f);
		return new Color(r, g, b);
	}

	public GameObject objToLaunch;

	public bool launch;

	public bool continuousLaunch;

	public Vector2 forceRange = new Vector2(100f, 200f);

	public Texture[] lineTextures;

	private GameObject launchObjParent;
}
