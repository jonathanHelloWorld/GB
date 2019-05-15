using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManagerr : Singleton<AudioManagerr> {

	public AudioSource sfxSource;
	public AudioSource sfx2Source;
	public AudioClip rightClip, wrongClip,showLetterClip;

	public void PlayRightSong()
	{
		if (sfxSource.isPlaying)
			sfxSource.Stop();

		sfxSource.clip = rightClip;
		sfxSource.Play();

	}

	public void PlayWrongSong()
	{
		if (sfxSource.isPlaying)
			sfxSource.Stop();

		sfxSource.clip = wrongClip;
		sfxSource.Play();

	}

	public void PlayLetterSong()
	{

		if (sfx2Source.isPlaying)
			sfx2Source.Stop();

		sfx2Source.clip = showLetterClip;
		sfx2Source.Play();

	}
}
