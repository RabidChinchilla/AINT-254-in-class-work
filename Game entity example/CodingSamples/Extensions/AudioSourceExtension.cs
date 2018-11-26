using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public static class AudioSourceExtension 
{
	// Use this for initialization
	public static IEnumerator PauseWithFadeOut(this AudioSource audioSource, float duration, float minVolume = 0.0f, Action onComplete = null) 
	{
		var startingVolume = audioSource.volume;

		while(audioSource.volume > minVolume)
		{
			audioSource.volume -= Time.deltaTime * (startingVolume / duration);
			//Debug.Log ("Pause: " + audioSource.volume);
			yield return null;
		}
		audioSource.volume = minVolume;
		audioSource.Pause();

		if(onComplete != null)
			onComplete();
	}

	public static IEnumerator PlayWithFadeIn(this AudioSource audioSource, float duration, float maxVolume = 1.0f, Action onComplete = null) 
	{
		audioSource.Play();
		var timer = 0.0f;
		var step = maxVolume / duration;
		
		while(audioSource.volume < maxVolume)
		{
			audioSource.volume += Time.deltaTime * step;
			//audioSource.volume = Mathf.Lerp(startingVolume, 1, timer);
			//audioSource.volume =  timer;//Time.deltaTime * step;// * (1.0f / duration);
			//Debug.Log (audioSource.volume);
			yield return null;
		}
		audioSource.volume = maxVolume;
		
		if(onComplete != null)
			onComplete();
	}

	public static IEnumerator StopWithFadeOut(this AudioSource audioSource, float duration, float minVolume = 0.0f, Action onComplete = null) 
	{
		var startingVolume = audioSource.volume;
		
		while(audioSource.volume > minVolume)
		{
			audioSource.volume -= Time.deltaTime * (startingVolume / duration);
			yield return null;
		}
		audioSource.volume = minVolume;
		audioSource.Stop();
		
		if(onComplete != null)
			onComplete();
	}

	public static IEnumerator FadeOutSoundToValue(this AudioSource audioSource, float duration, float lowVolume, Action onComplete = null) 
	{
		var startingVolume = audioSource.volume;
		
		while(audioSource.volume > lowVolume)
		{
			audioSource.volume -= Time.deltaTime * (startingVolume / duration);
			//Debug.Log ("Pause: " + audioSource.volume);
			yield return null;
		}
		audioSource.volume = lowVolume;
		
		if(onComplete != null)
			onComplete();
	}

	public static IEnumerator FadeInSoundToValue(this AudioSource audioSource, float duration, float maxVolume, Action onComplete = null) 
	{
		var step = maxVolume / duration;
		
		while(audioSource.volume < maxVolume)
		{
			audioSource.volume += Time.deltaTime * step;
			yield return null;
		}
		audioSource.volume = maxVolume;
		
		if(onComplete != null)
			onComplete();
	}
}
