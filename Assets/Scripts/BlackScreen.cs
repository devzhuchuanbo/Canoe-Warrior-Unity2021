using System;
using UnityEngine;

public class BlackScreen : MonoBehaviour
{
	private void Start()
	{
	}

	public void okmen()
	{
		MainEventsLog component = GameObject.FindGameObjectWithTag("MainEventLog").GetComponent<MainEventsLog>();
		component.FadeInDone();
	}

	public void okmenlv1(int i)
	{
		MainEventsLog component = GameObject.FindGameObjectWithTag("MainEventLog").GetComponent<MainEventsLog>();
		component.FadeInDonelv1(i);
	}
}
