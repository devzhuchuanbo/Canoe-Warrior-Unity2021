using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AddMaterialOnHit : MonoBehaviour
{
	public void UpdateMaterial(RaycastHit hit)
	{
		Transform transform = hit.transform;
		if (transform != null)
		{
			UnityEngine.Object.Destroy(base.gameObject, this.RemoveAfterTime);
			this.fadeInOutShaderColor = base.GetComponents<FadeInOutShaderColor>();
			this.fadeInOutShaderFloat = base.GetComponents<FadeInOutShaderFloat>();
			this.uvTextureAnimator = base.GetComponent<UVTextureAnimator>();
			this.renderParent = base.transform.parent.GetComponent<Renderer>();
			Material[] sharedMaterials = this.renderParent.sharedMaterials;
			int num = sharedMaterials.Length + 1;
			Material[] array = new Material[num];
			sharedMaterials.CopyTo(array, 0);
			this.renderParent.material = this.Material;
			this.instanceMat = this.renderParent.material;
			array[num - 1] = this.instanceMat;
			this.renderParent.sharedMaterials = array;
			if (this.UsePointMatrixTransform)
			{
				Matrix4x4 matrix = Matrix4x4.TRS(hit.transform.InverseTransformPoint(hit.point), Quaternion.Euler(180f, 180f, 0f), this.TransformScale);
				this.instanceMat.SetMatrix("_DecalMatr", matrix);
			}
			if (this.materialQueue != -1)
			{
				this.instanceMat.renderQueue = this.materialQueue;
			}
			if (this.fadeInOutShaderColor != null)
			{
				foreach (FadeInOutShaderColor fadeInOutShaderColor in this.fadeInOutShaderColor)
				{
					fadeInOutShaderColor.UpdateMaterial(this.instanceMat);
				}
			}
			if (this.fadeInOutShaderFloat != null)
			{
				foreach (FadeInOutShaderFloat fadeInOutShaderFloat in this.fadeInOutShaderFloat)
				{
					fadeInOutShaderFloat.UpdateMaterial(this.instanceMat);
				}
			}
			if (this.uvTextureAnimator != null)
			{
				this.uvTextureAnimator.SetInstanceMaterial(this.instanceMat, hit.textureCoord);
			}
		}
	}

	public void SetMaterialQueue(int matlQueue)
	{
		this.materialQueue = matlQueue;
	}

	private void OnDestroy()
	{
		if (this.renderParent == null)
		{
			return;
		}
		List<Material> list = this.renderParent.sharedMaterials.ToList<Material>();
		list.Remove(this.instanceMat);
		this.renderParent.sharedMaterials = list.ToArray();
	}

	public float RemoveAfterTime = 5f;

	public Material Material;

	public bool UsePointMatrixTransform;

	public Vector3 TransformScale = Vector3.one;

	private FadeInOutShaderColor[] fadeInOutShaderColor;

	private FadeInOutShaderFloat[] fadeInOutShaderFloat;

	private UVTextureAnimator uvTextureAnimator;

	private Renderer renderParent;

	private Material instanceMat;

	private int materialQueue = -1;
}
