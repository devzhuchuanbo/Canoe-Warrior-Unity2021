using System;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
	private void FixedUpdate()
	{
		Vector3 position = base.transform.position;
		Vector3 vector = Vector3.Lerp(base.transform.position, this.FollowTargetOBJ.transform.position, this.FollowSpeed * Time.deltaTime);
		base.transform.position = new Vector3(vector.x, base.transform.position.y, base.transform.position.z);
		Vector3 a = position - base.transform.position;
		this.BackgroundROOTS[0].transform.Translate(-a * 0.8f);
		this.BackgroundROOTS[1].transform.Translate(-a * 0.7f);
		this.BackgroundROOTS[2].transform.Translate(-a * 0.5f);
		this.BackgroundROOTS[3].transform.Translate(-a * 0.4f);
		this.BackgroundROOTS[4].transform.Translate(-a * 0.3f);
		this.BackgroundROOTS[5].transform.Translate(-a * 0.15f);
	}

	public GameObject FollowTargetOBJ;

	public float FollowSpeed;

	public GameObject[] BackgroundROOTS;
}
