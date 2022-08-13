using System;
using UnityEngine;
using UnityEngine.UI;

public class ScrollNumb : MonoBehaviour
{
	private void ShowStar(int numb)
	{
		if (numb >= 3)
		{
			this.scroll1.color = this.colorBright;
			this.scroll2.color = this.colorBright;
			this.scroll3.color = this.colorBright;
		}
		else if (numb == 2)
		{
			this.scroll1.color = this.colorBright;
			this.scroll2.color = this.colorBright;
			this.scroll3.color = this.colorGray;
		}
		else if (numb == 1)
		{
			this.scroll1.color = this.colorBright;
			this.scroll2.color = this.colorGray;
			this.scroll3.color = this.colorGray;
		}
		else
		{
			this.scroll1.color = this.colorGray;
			this.scroll2.color = this.colorGray;
			this.scroll3.color = this.colorGray;
		}
	}

	public Image scroll1;

	public Image scroll2;

	public Image scroll3;

	public Color colorGray;

	public Color colorBright;
}
