using System;
using UnityEngine;

public class RFX4_UVScroll : MonoBehaviour
{
	private void Start()
	{
		Renderer component = base.GetComponent<Renderer>();
		if (component == null)
		{
			Projector component2 = base.GetComponent<Projector>();
			if (component2 != null)
			{
				if (!component2.material.name.EndsWith("(Instance)"))
				{
					component2.material = new Material(component2.material)
					{
						name = component2.material.name + " (Instance)"
					};
				}
				this.mat = component2.material;
			}
		}
		else
		{
			this.mat = component.material;
		}
	}

	private void Update()
	{
		this.uvOffset += this.UvScrollMultiplier * Time.deltaTime;
		if (this.mat != null)
		{
			this.mat.SetTextureOffset(this.TextureName.ToString(), this.uvOffset);
		}
	}

	public Vector2 UvScrollMultiplier = new Vector2(1f, 0f);

	public RFX4_TextureShaderProperties TextureName;

	private Vector2 uvOffset = Vector2.zero;

	private Material mat;
}
