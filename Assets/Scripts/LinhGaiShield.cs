using System;
using UnityEngine;

public class LinhGaiShield : MonoBehaviour
{
	private void KilledPlayer()
	{
		this.mainEnemyScript.SendMessage("NangKhien", SendMessageOptions.DontRequireReceiver);
	}

	public Transform mainEnemyScript;
}
