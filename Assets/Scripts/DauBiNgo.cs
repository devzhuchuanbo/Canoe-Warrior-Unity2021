using System;
using UnityEngine;

public class DauBiNgo : MonoBehaviour
{
	private void Start()
	{
		string @string = PlayerPrefs.GetString("TransInUse");
		if (@string == "bienhinh_bingo" || @string == "bienhinh_bingo2")
		{
			this.dauThuong.gameObject.SetActive(false);
			this.dauBiNgo.gameObject.SetActive(true);
		}
		else
		{
			this.dauThuong.gameObject.SetActive(true);
			this.dauBiNgo.gameObject.SetActive(false);
		}
	}

	public Transform dauThuong;

	public Transform dauBiNgo;
}
