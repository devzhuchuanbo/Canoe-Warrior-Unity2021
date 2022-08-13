using System;
using UnityEngine;

public class CameraTouchControl : MonoBehaviour
{
	public static GameObject goPointedAt { get; private set; }

	private void Start()
	{
		CameraTouchControl.inputHitPos = new Vector3[20];
		CameraTouchControl.DragPos = new Vector3[20];
	}

	private void Update()
	{
		if (UnityEngine.Input.touchCount > 0)
		{
			for (int i = 0; i < UnityEngine.Input.touchCount; i++)
			{
				Touch touch = UnityEngine.Input.GetTouch(i);
				if (touch.phase == TouchPhase.Began)
				{
					this.Press(touch.position, i);
				}
				if (touch.phase == TouchPhase.Ended)
				{
					this.Release(touch.position, i);
				}
				if (touch.phase == TouchPhase.Moved)
				{
					this.RaycastDragPosition(touch.position, i);
				}
			}
			return;
		}
		if (Input.GetMouseButtonDown(0))
		{
			this.Press(UnityEngine.Input.mousePosition, 0);
		}
		else if (Input.GetMouseButtonUp(0))
		{
			this.Release(UnityEngine.Input.mousePosition, 0);
		}
		else if (Input.GetMouseButton(0))
		{
			this.RaycastDragPosition(UnityEngine.Input.mousePosition, 0);
		}
	}

	private void Press(Vector2 screenPos, int TouchNumber)
	{
		this.lastGo = this.RaycastObject(screenPos, TouchNumber);
		if (this.lastGo != null)
		{
			this.lastGo.SendMessage("OnPress_IE", TouchNumber, SendMessageOptions.DontRequireReceiver);
		}
	}

	private void Release(Vector2 screenPos, int TouchNumber)
	{
		this.lastGo = this.RaycastObject(screenPos, TouchNumber);
		if (this.lastGo != null)
		{
			GameObject x = this.RaycastObject(screenPos, TouchNumber);
			if (x == this.lastGo)
			{
				this.lastGo.SendMessage("OnClick_IE", SendMessageOptions.DontRequireReceiver);
			}
			this.lastGo.SendMessage("OnRelease_IE", TouchNumber, SendMessageOptions.DontRequireReceiver);
			this.lastGo = null;
		}
	}

	private GameObject RaycastObject(Vector2 screenPos, int TouchNumber)
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(base.GetComponent<Camera>().ScreenPointToRay(screenPos), out raycastHit, 200f, this.IncludeThisLayer))
		{
			CameraTouchControl.inputHitPos[TouchNumber] = raycastHit.point;
			CameraTouchControl.DragPos[TouchNumber] = raycastHit.point;
			return raycastHit.collider.gameObject;
		}
		return null;
	}

	private void RaycastDragPosition(Vector2 screenPos, int TouchNumber)
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(base.GetComponent<Camera>().ScreenPointToRay(screenPos), out raycastHit, 200f, this.IncludeThisLayer))
		{
			CameraTouchControl.DragPos[TouchNumber] = raycastHit.point;
		}
	}

	private GameObject lastGo;

	public static Vector3[] inputHitPos;

	public static Vector3[] DragPos;

	public LayerMask IncludeThisLayer;
}
