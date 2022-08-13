using System;
using UnityEngine;

public class MainBtn : MonoBehaviour
{
	public void HireAndShow()
	{
		foreach (Transform transform in this.ShowItem)
		{
			if (transform)
			{
				transform.gameObject.SetActive(true);
			}
		}
		foreach (Transform transform2 in this.HireItems)
		{
			if (transform2)
			{
				transform2.gameObject.SetActive(false);
			}
		}
	}

	public Transform[] ShowItem;

	public Transform[] HireItems;
}
