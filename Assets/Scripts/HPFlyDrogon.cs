using System;
using UnityEngine;

public class HPFlyDrogon : MonoBehaviour
{
	private void Start()
	{
		this.flyUp = false;
		this.player = GameObject.FindGameObjectWithTag("Player");
		base.gameObject.SetActive(false);
	}

	private void Update()
	{
		base.gameObject.transform.Translate(Vector3.left * this.flySpeed * Time.deltaTime);
		if (this.flyUp)
		{
			base.gameObject.transform.Translate(Vector3.up * this.flySpeed * Time.deltaTime);
		}
		if (base.transform.position.x < this.player.transform.position.x - 25f)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void Hit(Vector2 v2)
	{
		base.GetComponent<Collider2D>().enabled = false;
		this.flyUp = true;
		this.Chin.gameObject.SetActive(false);
		UnityEngine.Object.Instantiate(this.CucMau, this.Pos.position, Quaternion.identity);
	}

	private void Hit2(Vector2 v2)
	{
		base.GetComponent<Collider2D>().enabled = false;
		this.flyUp = true;
		this.Chin.gameObject.SetActive(false);
		UnityEngine.Object.Instantiate(this.CucMau, this.Pos.position, Quaternion.identity);
	}

	private void DashOver()
	{
		base.GetComponent<Collider2D>().enabled = false;
		this.flyUp = true;
		this.Chin.gameObject.SetActive(false);
		UnityEngine.Object.Instantiate(this.CucMau, this.Pos.position, Quaternion.identity);
	}

	public float flySpeed;

	public GameObject CucMau;

	public Transform Pos;

	public Transform Chin;

	private GameObject player;

	private bool flyUp;
}
