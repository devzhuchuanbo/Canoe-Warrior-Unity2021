using System;
using UnityEngine;

public class NinjaPlatformRoot : MonoBehaviour
{
	private void Update()
	{
		if (this.RootedTo != null)
		{
			base.transform.position = this.RootedTo.transform.position;
		}
	}

	public GameObject RootedTo;
}
