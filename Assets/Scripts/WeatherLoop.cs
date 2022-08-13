using System;
using UnityEngine;

public class WeatherLoop : MonoBehaviour
{
	private void Start()
	{
		this.player = GameObject.FindGameObjectWithTag("Player");
	}

	private void Update()
	{
		if (base.transform.position.x < this.player.transform.position.x - 46f)
		{
			base.transform.position += new Vector3(90f, 0f, 0f);
		}
		if (base.transform.position.x > this.player.transform.position.x + 44f)
		{
			base.transform.position -= new Vector3(90f, 0f, 0f);
		}
	}

	private GameObject player;
}
