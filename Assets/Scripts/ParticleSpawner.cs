//// dnSpy decompiler from Assembly-UnityScript.dll class: ParticleSpawner
//using System;
//using UnityEngine;
//using UnityScript.Lang;

//[Serializable]
//public class ParticleSpawner : MonoBehaviour
//{
//	public ParticleSpawner()
//	{
//		this.maxButtons = 10;
//	}

//	public virtual void Start()
//	{
//		System.Array.Sort<GameObject>(this.particles, new Comparison<GameObject>(this._0024Start_0024closure_00242));
//		this.pages = (int)Mathf.Ceil((float)((Extensions.get_length(this.particles) - 1) / this.maxButtons));
//	}

//	public virtual void OnGUI()
//	{
//		Time.timeScale = GUI.VerticalSlider(new Rect((float)185, (float)50, (float)20, (float)150), Time.timeScale, 2f, (float)0);
//		if (Extensions.get_length(this.particles) > this.maxButtons)
//		{
//			if (GUI.Button(new Rect((float)20, (float)((this.maxButtons + 1) * 18), (float)75, (float)18), "Prev"))
//			{
//				if (this.page > 0)
//				{
//					this.page--;
//				}
//				else
//				{
//					this.page = this.pages;
//				}
//			}
//			if (GUI.Button(new Rect((float)95, (float)((this.maxButtons + 1) * 18), (float)75, (float)18), "Next"))
//			{
//				if (this.page < this.pages)
//				{
//					this.page++;
//				}
//				else
//				{
//					this.page = 0;
//				}
//			}
//			GUI.Label(new Rect((float)60, (float)((this.maxButtons + 2) * 18), (float)150, (float)22), "Page" + (this.page + 1) + " / " + (this.pages + 1));
//		}
//		this.showInfo = GUI.Toggle(new Rect((float)185, (float)20, (float)75, (float)25), this.showInfo, "Info");
//		if (this.showInfo)
//		{
//			GUI.Label(new Rect((float)250, (float)20, (float)500, (float)500), this.currentPSInfo);
//		}
//		int num = Extensions.get_length(this.particles) - this.page * this.maxButtons;
//		if (num > this.maxButtons)
//		{
//			num = this.maxButtons;
//		}
//		for (int i = 0; i < num; i++)
//		{
//			string text = this.particles[i + this.page * this.maxButtons].transform.name;
//			text = text.Replace(this.removeTextFromButton, string.Empty);
//			if (GUI.Button(new Rect((float)20, (float)(i * 18 + 18), (float)150, (float)18), text))
//			{
//				if (this.currentPS)
//				{
//					UnityEngine.Object.Destroy(this.currentPS);
//				}
//				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.particles[i + this.page * this.maxButtons]);
//				this.currentPS = gameObject;
//				this.PlayPS((ParticleSystem)gameObject.GetComponent(typeof(ParticleSystem)), i + this.page * this.maxButtons + 1);
//				this.InfoPS((ParticleSystem)gameObject.GetComponent(typeof(ParticleSystem)), i + this.page * this.maxButtons + 1);
//			}
//		}
//	}

//	public virtual void PlayPS(ParticleSystem _ps, int _nr)
//	{
//		Time.timeScale = (float)1;
//		_ps.Play();
//	}

//	public virtual void InfoPS(ParticleSystem _ps, int _nr)
//	{
//		this.currentPSInfo = "System" + ": " + _nr + "/" + Extensions.get_length(this.particles) + "\n" + "Name: " + _ps.gameObject.name + "\n\n" + "Main PS Sub Particles: " + _ps.transform.childCount + "\n" + "Main PS Materials: " + Extensions.get_length(_ps.GetComponent<Renderer>().materials) + "\n" + "Main PS Shader: " + _ps.GetComponent<Renderer>().material.shader.name;
//		if (Extensions.get_length(_ps.GetComponent<Renderer>().materials) >= 2)
//		{
//			this.currentPSInfo += "\n\n *Plasma not mobile optimized*";
//		}
//		this.currentPSInfo += "\n\n Use mouse wheel to zoom, click and hold to rotate";
//		this.currentPSInfo = this.currentPSInfo.Replace("(Clone)", string.Empty);
//	}

//	public virtual void Main()
//	{
//	}

//	internal int _0024Start_0024closure_00242(GameObject g1, GameObject g2)
//	{
//		return string.Compare(g1.name, g2.name);
//	}

//	public GameObject[] particles;

//	public int maxButtons;

//	public bool showInfo;

//	public string removeTextFromButton;

//	private int page;

//	private int pages;

//	private string currentPSInfo;

//	private GameObject currentPS;
//}
