using System;
using UnityEngine;

public class PlayAudioSource : MonoBehaviour
{
	public void PlayAudio()
	{
		if (this.SoundSource)
		{
			this.SoundSource.Play();
		}
	}

	public AudioSource SoundSource;
}
