using System;
using UnityEngine;

public class ChatStory : MonoBehaviour
{
	public void Chien()
	{
		this.orochi.gameObject.SendMessage("ChienLuon");
	}

	public Transform orochi;
}
