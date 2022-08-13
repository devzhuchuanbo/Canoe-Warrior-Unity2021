using System;
using UnityEngine;

public class NextPageButton : MonoBehaviour
{
	public void OnPress_IE()
	{
		this.click.Play();
		this.bang.gameObject.transform.position = Vector3.Lerp(this.bang.transform.position, this.Vec3, 1f);
	}

	public void OnRelease_IE()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public GameObject bang;

	public Vector3 Vec3;

	public AudioSource click;
}
