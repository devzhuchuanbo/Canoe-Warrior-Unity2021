using System;
using UnityEngine;

public class PauseAnim : MonoBehaviour
{
	private void Start()
	{
	}

	private void ps()
	{
		this.mainEventLogScript.pausing = true;
	}

	public MainEventsLog mainEventLogScript;
}
