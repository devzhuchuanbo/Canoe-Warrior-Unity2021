using System;
using UnityEngine;

public class GUIView : MonoBehaviour
{
	private void Start()
	{
		if (!PlayerPrefs.HasKey("PlayerName"))
		{
			this.SaveData();
		}
		this.RefreshData();
	}

	private void Update()
	{
		if (Input.anyKeyDown)
		{
			this.RefreshData();
		}
	}

	private void OnGUI()
	{
		int num = 450;
		int num2 = 400;
		GUILayout.BeginArea(new Rect((float)(Screen.width / 2 - num / 2), (float)(Screen.height / 2 - num2 / 2), (float)num, (float)num2));
		GUILayout.BeginVertical(new GUILayoutOption[0]);
		string text = "Unfortunately, this will take a a few seconds. This is due Unity working different on a Mac :(";
		GUILayout.TextArea(string.Format("Instructions: {0} 1) Open the Advanced PlayerPrefs Window and dock it somewhere. {0} 2) Change the values in the scene using the gui widgets. {0} 3) Go back to the Advanced PlayerPrefs Window and click the refresh button. " + ((Application.platform != RuntimePlatform.OSXEditor) ? string.Empty : text) + " {0} 4) Observe that the values in the Advanced PlayerPrefs Window has changed to your scene input. {0}{0} 5) Now in the Advanced PlayerPrefs Window, change the values and save those changes {0} 6) Go give the scene focus by clicking in the sceneview. {0} 7) Watch the gui values update to your changes", Environment.NewLine), new GUILayoutOption[0]);
		GUILayout.Space(12f);
		GUILayout.Label("Progress: " + (int)this.progress + "%", new GUILayoutOption[0]);
		float a = GUILayout.HorizontalSlider(this.progress, 0f, 100f, new GUILayoutOption[0]);
		if (!Mathf.Approximately(a, this.progress))
		{
			this.progress = a;
			this.SaveData();
		}
		GUILayout.Space(12f);
		bool flag = GUILayout.Toggle(this.muted, "Is Audio Muted?", new GUILayoutOption[0]);
		if (flag != this.muted)
		{
			this.muted = flag;
			this.SaveData();
		}
		GUILayout.Space(12f);
		GUILayout.Label("Highscore: " + this.score, new GUILayoutOption[0]);
		GUILayout.Space(12f);
		GUILayout.Label("Playername", new GUILayoutOption[0]);
		string a2 = GUILayout.TextField(this.playername, new GUILayoutOption[0]);
		if (a2 != this.playername)
		{
			this.playername = a2;
			this.SaveData();
		}
		GUILayout.EndVertical();
		GUILayout.EndArea();
	}

	public void RefreshData()
	{
		this.progress = PlayerPrefs.GetFloat("Progress", 100f);
		this.muted = (PlayerPrefs.GetString("IsSoundMuted", "true") == "true");
		this.score = PlayerPrefs.GetInt("Highscore", 123);
		this.playername = PlayerPrefs.GetString("PlayerName", "Noname");
	}

	public void SaveData()
	{
		PlayerPrefs.SetFloat("Progress", this.progress);
		PlayerPrefs.SetString("IsSoundMuted", (!this.muted) ? "false" : "true");
		PlayerPrefs.SetInt("Highscore", this.score);
		PlayerPrefs.SetString("PlayerName", this.playername);
	}

	private const string PROGRESS_KEY = "Progress";

	private const string MUTED_KEY = "IsSoundMuted";

	private const string SCORE_KEY = "Highscore";

	private const string PLAYERNAME_KEY = "PlayerName";

	private float progress;

	private bool muted;

	private int score;

	private string playername = "Noname";
}
