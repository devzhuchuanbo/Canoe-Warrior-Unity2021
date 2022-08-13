using System;
using UnityEngine;

public class Guid : MonoBehaviour
{
	public void OnPress_IE()
	{
		this.mainEventLogScript.pausing = false;
		UnityEngine.Object.Destroy(this.guid);
	}

	public MainEventsLog mainEventLogScript;

	public GameObject guid;
}
