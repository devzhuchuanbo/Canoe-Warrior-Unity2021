using System;
using UnityEngine;

public class HUDText : MonoBehaviour
{
	private void Start()
	{
		this.c = 0;
	}

	public Transform GetCoinHUD()
	{
		if (this.c >= this.coinHUD.Length)
		{
			this.c = 0;
		}
		this.c++;
		return this.coinHUD[this.c - 1];
	}

	public void ShowCoinHudOut(int coin, Transform p)
	{
		Transform transform = this.GetCoinHUD();
		transform.gameObject.SetActive(true);
		HUDTextChild component = transform.GetComponent<HUDTextChild>();
		component.ShowOut(coin, this.coinColor, p);
	}

	public void ShowDMHudOut(int dm, Transform p)
	{
		Transform transform = this.GetCoinHUD();
		transform.gameObject.SetActive(true);
		HUDTextChild component = transform.GetComponent<HUDTextChild>();
		component.ShowOut(dm, this.dmColor, p);
	}

	public void ShowHudPos(Vector3 p)
	{
		Transform transform = this.GetCoinHUD();
		transform.gameObject.SetActive(true);
		HUDTextChild component = transform.GetComponent<HUDTextChild>();
		component.ShowPos("Critical", this.critColor, p);
	}

	public void ShowCoinHud(int coin)
	{
		Transform transform = this.GetCoinHUD();
		transform.gameObject.SetActive(true);
		HUDTextChild component = transform.GetComponent<HUDTextChild>();
		component.Show(coin, this.coinColor);
	}

	public void ShowDMHud(int dm)
	{
		Transform transform = this.GetCoinHUD();
		transform.gameObject.SetActive(true);
		HUDTextChild component = transform.GetComponent<HUDTextChild>();
		component.Show(dm, this.dmColor);
	}

	public Color coinColor;

	public Color dmColor;

	public Color critColor;

	public Transform[] coinHUD;

	private int c;
}
