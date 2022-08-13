using System;
using UnityEngine;

public class MyGuiV2 : MonoBehaviour
{
	private void Start()
	{
		this.oldAmbientColor = RenderSettings.ambientLight;
		this.oldLightIntensity = this.DirLight.intensity;
		this.anim = this.Target.GetComponent<Animator>();
		this.guiStyleHeader.fontSize = 14;
		this.guiStyleHeader.normal.textColor = new Color(1f, 1f, 1f);
		EffectSettings component = this.Prefabs[this.current].GetComponent<EffectSettings>();
		if (component != null)
		{
			this.prefabSpeed = component.MoveSpeed;
		}
		this.current = this.CurrentPrefabNomber;
		this.InstanceCurrent(this.GuiStats[this.CurrentPrefabNomber]);
	}

	private void InstanceEffect(Vector3 pos)
	{
		this.currentGo = (UnityEngine.Object.Instantiate(this.Prefabs[this.current], pos, this.Prefabs[this.current].transform.rotation) as GameObject);
		this.effectSettings = this.currentGo.GetComponent<EffectSettings>();
		this.effectSettings.Target = this.GetTargetObject(this.GuiStats[this.current]);
		if (this.isHomingMove)
		{
			this.effectSettings.IsHomingMove = this.isHomingMove;
		}
		this.prefabSpeed = this.effectSettings.MoveSpeed;
		this.effectSettings.EffectDeactivated += this.effectSettings_EffectDeactivated;
		this.currentGo.transform.parent = base.transform;
	}

	private void InstanceEffectWithoutObjectPool()
	{
		this.currentGo = (UnityEngine.Object.Instantiate(this.Prefabs[this.current], this.GetInstancePosition(this.GuiStats[this.current]), this.Prefabs[this.current].transform.rotation) as GameObject);
		this.effectSettings = this.currentGo.GetComponent<EffectSettings>();
		this.effectSettings.Target = this.GetTargetObject(this.GuiStats[this.current]);
		if (this.isHomingMove)
		{
			this.effectSettings.IsHomingMove = this.isHomingMove;
		}
		this.prefabSpeed = this.effectSettings.MoveSpeed;
		this.effectSettings.EffectDeactivated += this.effectSettings_EffectDeactivated;
		this.currentGo.transform.parent = base.transform;
	}

	private GameObject GetTargetObject(MyGuiV2.GuiStat stat)
	{
		switch (stat)
		{
		case MyGuiV2.GuiStat.Ball:
			return this.Target;
		case MyGuiV2.GuiStat.Bottom:
			return this.BottomPosition;
		case MyGuiV2.GuiStat.Middle:
			return this.MiddlePosition;
		case MyGuiV2.GuiStat.Top:
			return this.TopPosition;
		default:
			return base.gameObject;
		}
	}

	private void InstanceDefaulBall()
	{
		this.defaultBall = (UnityEngine.Object.Instantiate(this.Prefabs[1], base.transform.position, this.Prefabs[1].transform.rotation) as GameObject);
		this.defaultBallEffectSettings = this.defaultBall.GetComponent<EffectSettings>();
		this.defaultBallEffectSettings.Target = this.Target;
		this.defaultBallEffectSettings.EffectDeactivated += this.defaultBall_EffectDeactivated;
		this.defaultBall.transform.parent = base.transform;
	}

	private void defaultBall_EffectDeactivated(object sender, EventArgs e)
	{
		this.defaultBall.transform.position = base.transform.position;
		this.isReadyDefaulBall = true;
	}

	private void effectSettings_EffectDeactivated(object sender, EventArgs e)
	{
		if (this.current == 15 || this.current == 16)
		{
			UnityEngine.Object.Destroy(this.effectSettings.gameObject);
			this.InstanceEffect(this.GetInstancePosition(this.GuiStats[this.current]));
		}
		else
		{
			this.currentGo.transform.position = this.GetInstancePosition(this.GuiStats[this.current]);
			this.isReadyEffect = true;
		}
	}

	private void OnGUI()
	{
		if (GUI.Button(new Rect(10f, 15f, 105f, 30f), "Previous Effect"))
		{
			this.ChangeCurrent(-1);
		}
		if (GUI.Button(new Rect(130f, 15f, 105f, 30f), "Next Effect"))
		{
			this.ChangeCurrent(1);
		}
		if (this.Prefabs[this.current] != null)
		{
			GUI.Label(new Rect(300f, 15f, 100f, 20f), "Prefab name is \"" + this.Prefabs[this.current].name + "\"  \r\nHold any mouse button that would move the camera", this.guiStyleHeader);
		}
		if (GUI.Button(new Rect(10f, 60f, 225f, 30f), "Day/Night"))
		{
			this.DirLight.intensity = (this.isDay ? this.oldLightIntensity : 0f);
			RenderSettings.ambientLight = (this.isDay ? this.oldAmbientColor : new Color(0.1f, 0.1f, 0.1f));
			this.isDay = !this.isDay;
		}
		if (GUI.Button(new Rect(10f, 105f, 225f, 30f), "Change environment"))
		{
			if (this.isDefaultPlaneTexture)
			{
				this.Plane1.GetComponent<Renderer>().material = this.PlaneMaterials[0];
				this.Plane2.GetComponent<Renderer>().material = this.PlaneMaterials[0];
			}
			else
			{
				this.Plane1.GetComponent<Renderer>().material = this.PlaneMaterials[1];
				this.Plane2.GetComponent<Renderer>().material = this.PlaneMaterials[2];
			}
			this.isDefaultPlaneTexture = !this.isDefaultPlaneTexture;
		}
		if (this.current <= 40)
		{
			GUI.Label(new Rect(10f, 152f, 225f, 30f), "Ball Speed " + (int)this.prefabSpeed + "m", this.guiStyleHeader);
			this.prefabSpeed = GUI.HorizontalSlider(new Rect(115f, 155f, 120f, 30f), this.prefabSpeed, 1f, 30f);
			this.isHomingMove = GUI.Toggle(new Rect(10f, 190f, 150f, 30f), this.isHomingMove, " Is Homing Move");
			this.effectSettings.MoveSpeed = this.prefabSpeed;
		}
		GUI.Label(new Rect(1f, 1f, 30f, 30f), string.Empty + (int)this.fps, this.guiStyleHeader);
	}

	private void Update()
	{
		this.anim.enabled = this.isHomingMove;
		this.timeleft -= Time.deltaTime;
		this.accum += Time.timeScale / Time.deltaTime;
		this.frames++;
		if ((double)this.timeleft <= 0.0)
		{
			this.fps = this.accum / (float)this.frames;
			this.timeleft = this.UpdateInterval;
			this.accum = 0f;
			this.frames = 0;
		}
		if (this.isReadyEffect)
		{
			this.isReadyEffect = false;
			this.currentGo.SetActive(true);
		}
		if (this.isReadyDefaulBall)
		{
			this.isReadyDefaulBall = false;
			this.defaultBall.SetActive(true);
		}
	}

	private void InstanceCurrent(MyGuiV2.GuiStat stat)
	{
		switch (stat)
		{
		case MyGuiV2.GuiStat.Ball:
			this.InstanceEffect(base.transform.position);
			break;
		case MyGuiV2.GuiStat.Bottom:
			this.InstanceEffect(this.BottomPosition.transform.position);
			break;
		case MyGuiV2.GuiStat.Middle:
			this.MiddlePosition.SetActive(true);
			this.InstanceEffect(this.MiddlePosition.transform.position);
			break;
		case MyGuiV2.GuiStat.Top:
			this.InstanceEffect(this.TopPosition.transform.position);
			break;
		}
	}

	private Vector3 GetInstancePosition(MyGuiV2.GuiStat stat)
	{
		switch (stat)
		{
		case MyGuiV2.GuiStat.Ball:
			return base.transform.position;
		case MyGuiV2.GuiStat.Bottom:
			return this.BottomPosition.transform.position;
		case MyGuiV2.GuiStat.Middle:
			return this.MiddlePosition.transform.position;
		case MyGuiV2.GuiStat.Top:
			return this.TopPosition.transform.position;
		default:
			return base.transform.position;
		}
	}

	private void ChangeCurrent(int delta)
	{
		UnityEngine.Object.Destroy(this.currentGo);
		UnityEngine.Object.Destroy(this.defaultBall);
		base.CancelInvoke("InstanceDefaulBall");
		this.current += delta;
		if (this.current > this.Prefabs.Length - 1)
		{
			this.current = 0;
		}
		else if (this.current < 0)
		{
			this.current = this.Prefabs.Length - 1;
		}
		if (this.effectSettings != null)
		{
			this.effectSettings.EffectDeactivated -= this.effectSettings_EffectDeactivated;
		}
		if (this.defaultBallEffectSettings != null)
		{
			this.defaultBallEffectSettings.EffectDeactivated -= this.effectSettings_EffectDeactivated;
		}
		this.MiddlePosition.SetActive(this.GuiStats[this.current] == MyGuiV2.GuiStat.Middle);
		this.InstanceEffect(this.GetInstancePosition(this.GuiStats[this.current]));
	}

	public int CurrentPrefabNomber;

	public float UpdateInterval = 0.5f;

	public Light DirLight;

	public GameObject Target;

	public GameObject TopPosition;

	public GameObject MiddlePosition;

	public GameObject BottomPosition;

	public GameObject Plane1;

	public GameObject Plane2;

	public Material[] PlaneMaterials;

	public MyGuiV2.GuiStat[] GuiStats;

	public GameObject[] Prefabs;

	private float oldLightIntensity;

	private Color oldAmbientColor;

	private GameObject currentGo;

	private GameObject defaultBall;

	private bool isDay;

	private bool isHomingMove;

	private bool isDefaultPlaneTexture;

	private int current;

	private Animator anim;

	private float prefabSpeed = 4f;

	private EffectSettings effectSettings;

	private EffectSettings defaultBallEffectSettings;

	private bool isReadyEffect;

	private bool isReadyDefaulBall;

	private float accum;

	private int frames;

	private float timeleft;

	private float fps;

	private GUIStyle guiStyleHeader = new GUIStyle();

	public enum GuiStat
	{
		Ball,
		Bottom,
		Middle,
		Top
	}
}
