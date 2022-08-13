using System;
using UnityEngine;

public class Touch_BTN_Arrows : MonoBehaviour
{
	private void Start()
	{
		this.IsPressed = false;
		this.RightSide = false;
		this.LeftSide = false;
	}

	private void Update()
	{
		if (this.IsPressed)
		{
			if (this.RightSide && CameraTouchControl.DragPos[this.StoredTouchID].x < base.transform.position.x)
			{
				this.RightSide = false;
				this.LeftSide = true;
				this.ButtonRight_EnableSprite.color = this.ColorON;
				this.ButtonLeft_EnableSprite.color = this.ColorOFF;
				this.NinjaMovScript.Button_Right_release();
				this.NinjaMovScript.Button_Left_press();
			}
			else if (!this.RightSide && CameraTouchControl.DragPos[this.StoredTouchID].x > base.transform.position.x)
			{
				this.RightSide = true;
				this.LeftSide = false;
				this.ButtonRight_EnableSprite.color = this.ColorOFF;
				this.ButtonLeft_EnableSprite.color = this.ColorON;
				this.NinjaMovScript.Button_Left_release();
				this.NinjaMovScript.Button_Right_press();
			}
		}
	}

	public void OnPress_IE(int TouchID)
	{
		this.StoredTouchID = TouchID;
		this.IsPressed = true;
		if (CameraTouchControl.inputHitPos[this.StoredTouchID].x < base.transform.position.x)
		{
			this.ButtonRight_EnableSprite.color = this.ColorON;
			this.LeftSide = true;
			this.NinjaMovScript.Button_Left_press();
		}
		else
		{
			this.ButtonLeft_EnableSprite.color = this.ColorON;
			this.RightSide = true;
			this.NinjaMovScript.Button_Right_press();
		}
	}

	public void OnRelease_IE(int TouchID)
	{
		if (CameraTouchControl.inputHitPos[this.StoredTouchID].x < base.transform.position.x)
		{
			this.LeftSide = false;
			this.NinjaMovScript.Button_Left_release();
			this.ButtonRight_EnableSprite.color = this.ColorOFF;
		}
		else
		{
			this.RightSide = false;
			this.NinjaMovScript.Button_Right_release();
			this.ButtonLeft_EnableSprite.color = this.ColorOFF;
		}
		if (!this.RightSide && !this.LeftSide)
		{
			this.IsPressed = false;
		}
	}

	public NinjaMovementScript NinjaMovScript;

	private bool IsPressed;

	private bool RightSide;

	private bool LeftSide;

	public Color ColorON;

	public Color ColorOFF;

	public SpriteRenderer ButtonLeft_EnableSprite;

	public SpriteRenderer ButtonRight_EnableSprite;

	private int StoredTouchID;
}
