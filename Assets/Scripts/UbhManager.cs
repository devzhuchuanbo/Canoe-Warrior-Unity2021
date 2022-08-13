using System;
using UnityEngine;

public class UbhManager : UbhMonoBehaviour
{
	private void Start()
	{
		this._GoLetterBox.SetActive(!this._ScaleToFit);
	}

	private void Update()
	{
		if (UbhUtil.IsMobilePlatform())
		{
			if (!this.IsPlaying() && Input.GetMouseButtonDown(0))
			{
				this.GameStart();
			}
		}
		else if (!this.IsPlaying() && UnityEngine.Input.GetKeyDown(KeyCode.X))
		{
			this.GameStart();
		}
	}

	private void GameStart()
	{
		if (this._Score != null)
		{
			this._Score.Initialize();
		}
		if (this._GoTitle != null)
		{
			this._GoTitle.SetActive(false);
		}
		this.CreatePlayer();
	}

	public void GameOver()
	{
		if (this._Score != null)
		{
			this._Score.Save();
		}
		if (this._GoTitle != null)
		{
			this._GoTitle.SetActive(true);
		}
		else
		{
			this.CreatePlayer();
		}
	}

	private void CreatePlayer()
	{
		UnityEngine.Object.Instantiate(this._PlayerPrefab, this._PlayerPrefab.transform.position, this._PlayerPrefab.transform.rotation);
	}

	public bool IsPlaying()
	{
		return !(this._GoTitle != null) || !this._GoTitle.activeSelf;
	}

	public const int BASE_SCREEN_WIDTH = 600;

	public const int BASE_SCREEN_HEIGHT = 450;

	public bool _ScaleToFit;

	[SerializeField]
	private GameObject _PlayerPrefab;

	[SerializeField]
	private GameObject _GoTitle;

	[SerializeField]
	private GameObject _GoLetterBox;

	[SerializeField]
	private UbhScore _Score;
}
