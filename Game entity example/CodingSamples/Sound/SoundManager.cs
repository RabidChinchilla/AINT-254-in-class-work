using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class SoundManager : IDisposable
{
	private static SoundManager instance;
	private AudioSource m_audioSource;
	private AudioSource m_fxAudioSource;
	private float m_fadeOutValue;
	private float m_defaultMaster;

	private List<AudioClip> m_soundTracks;
	private List<AudioClip> m_soundFX;
	private List<AudioClip> m_soundEndOfMatch;
	private List<AudioClip> m_soundMenu;
	private List<AudioClip> m_soundHit;
	private List<AudioClip> m_soundMiss;
	private List<AudioClip> m_soundMultiply;
	private SoundManagerContainer m_parent;

	private IEnumerator m_trackList;
	private IEnumerator m_trackEndOfMatch;
	private IEnumerator m_trackMenu;
	private IEnumerator m_lastEnumerator;
	private IEnumerator m_pauseEnumerator;
	
	private float m_musicVolume = 1.0f;
	private float m_fxVolume = 1.0f;

	// Use this for initialization
	public SoundManager() 
	{

	}
	
	public static SoundManager Instance
	{
		get 
		{
			if (instance == null)
			{
				instance = new SoundManager();
			}
			return instance;
		}
	}

	public void Initialise(SoundManagerContainer parent)
	{
		m_trackList = StartPlayingTrackList();
		m_trackEndOfMatch = StartPlayingEndOfMatchList();
		m_trackMenu = StartPlayingMenuList();
		m_pauseEnumerator = PauseEnumerator();

		m_parent = parent;
		m_fadeOutValue = m_parent.soundFadeInAndOutValue;
		m_audioSource = parent.audioSource;
		m_fxAudioSource = parent.fxAudioSource;
		m_defaultMaster = m_parent.masterVolume;

		m_soundTracks = new List<AudioClip>();
		m_soundFX = new	List<AudioClip>();
		m_soundEndOfMatch = new List<AudioClip>();
		m_soundMenu = new List<AudioClip>();
		m_soundHit = new List<AudioClip>();
		m_soundMiss = new List<AudioClip>();
		m_soundMultiply = new List<AudioClip>();

		for (int i = 0; i < parent.soundsInGame.Length; i++) 
		{
			switch (parent.soundsInGame[i].typeOfClip) 
			{ 
				case SoundManagerContainer.SoundType.endOfMatchSound:
					m_soundEndOfMatch.Add(parent.soundsInGame[i].audioClip);
				break;
				case SoundManagerContainer.SoundType.menuSound:
					m_soundMenu.Add(parent.soundsInGame[i].audioClip);
				break;
				case SoundManagerContainer.SoundType.soundFX:
					m_soundFX.Add(parent.soundsInGame[i].audioClip);
				break;
				case SoundManagerContainer.SoundType.soundTracks:
					m_soundTracks.Add(parent.soundsInGame[i].audioClip);
				break;
				case SoundManagerContainer.SoundType.soundHit:
					m_soundHit.Add(parent.soundsInGame[i].audioClip);
				break;
				case SoundManagerContainer.SoundType.soundMiss:
					m_soundMiss.Add(parent.soundsInGame[i].audioClip);
				break;
				case SoundManagerContainer.SoundType.soundMultiply:
					m_soundMultiply.Add(parent.soundsInGame[i].audioClip);
				break;
			}
		}
		
		Message.onGameStateChange += OnGameStateChange;
		//PlayMenuList();
		//PausePlay();
	}
	
	private void OnGameStateChange(GameSystem.GameState _gameState)
	{	
		switch(_gameState)
		{                    
		case GameSystem.GameState.PLAY:
			PlayTrackList();
			break;   
		case GameSystem.GameState.GAMEOVER:
			m_audioSource.pitch = 1.0f;
			PlayEndOfMatchList();
			break;                    
		case GameSystem.GameState.RETRY:
			
			break;              
		case GameSystem.GameState.MENU:
			//m_audioSource.pitch = 1.0f;
			//PlayMenuList();
			
			m_parent.StartCoroutine(this.PlaySound());
			break;
		case GameSystem.GameState.PAUSE:
			
			break;
		case GameSystem.GameState.QUIT:
			
			break;
		default:
			break;
		};
	}
	
	private IEnumerator PlaySound()
	{
		while(Application.loadedLevel != 1)
		{
			yield return null;
		}
		
		m_audioSource.pitch = 1.0f;
		PlayMenuList();
	}
	
	public void PausePlay()
	{
		if(m_audioSource.isPlaying)
		{
			m_parent.StartCoroutine
			(
				m_audioSource.PauseWithFadeOut(m_fadeOutValue, m_parent.trackTransitionVolume, () =>
				{
					SetGenericCoroutine(m_pauseEnumerator);
				})
			);
		}
		else
		{
			SetGenericCoroutine(m_pauseEnumerator);
		}
	}

	public void PlayLastList()
	{
		if(m_audioSource.isPlaying)
		{
			m_parent.StartCoroutine
			(
					m_audioSource.PauseWithFadeOut(m_fadeOutValue, m_parent.trackTransitionVolume,() =>
			    {
					SetGenericCoroutine(m_lastEnumerator);
				})
			);
		}
		else
		{
			SetGenericCoroutine(m_lastEnumerator);
		}
	}

	private IEnumerator PauseEnumerator()
	{
		while(true)
		{
			yield return null;
		}
	}

	private void SetGenericCoroutine(IEnumerator coroutine)
	{
		if(m_parent.GetCurrentCoroutine() != null || m_parent.GetCurrentCoroutine() != PauseEnumerator())
			m_lastEnumerator = m_parent.GetCurrentCoroutine();

		m_parent.SetCurrentCoroutine(coroutine);
	}

	public void PlayEndOfMatchList()
	{
		if(m_audioSource.isPlaying)
		{
			m_parent.StartCoroutine
			(
				m_audioSource.PauseWithFadeOut(m_fadeOutValue, m_parent.trackTransitionVolume, () =>
                {
					SetGenericCoroutine(m_trackEndOfMatch);
				})
			);
		}
		else
		{
			SetGenericCoroutine(m_trackEndOfMatch);
		}
	}

	private IEnumerator StartPlayingEndOfMatchList()
	{
		m_soundEndOfMatch.Shuffle();
		int i = 0;
		bool done = false;
		int prevFrame = 0;
		
		while(i < m_soundEndOfMatch.Count)
		{
			m_audioSource.clip = m_soundEndOfMatch[i];
			m_audioSource.audio.timeSamples = 0;
			m_parent.StartCoroutine(m_audioSource.PlayWithFadeIn(m_fadeOutValue, m_musicVolume));
			done = false;
			prevFrame = 0;

			while (!done) 
			{
				if(m_audioSource.clip != m_soundEndOfMatch[i])
				{
					m_audioSource.clip = m_soundEndOfMatch[i];
					m_audioSource.audio.timeSamples = prevFrame;
				}

				if(!m_audioSource.isPlaying)
				{
					m_parent.StartCoroutine(m_audioSource.PlayWithFadeIn(m_fadeOutValue, m_musicVolume));
				}
				
				if(prevFrame > m_audioSource.audio.timeSamples)
				{
					done = true;
					m_audioSource.Stop();//m_parent.StartCoroutine(m_audioSource.StopIss(0.5f));
					break;
				}
				
				prevFrame = m_audioSource.audio.timeSamples;

				yield return null;
			}

			i ++;
			
			if(i == m_soundEndOfMatch.Count)
				i = 0;
			
			yield return null;
		}
	}

	public void PlayMenuList()
	{
		if(m_audioSource.isPlaying)
		{
			m_parent.StartCoroutine
			(
				m_audioSource.PauseWithFadeOut(m_fadeOutValue, m_parent.trackTransitionVolume, () =>
			    {
					SetGenericCoroutine(m_trackMenu);
					//m_parent.StartCoroutine(m_audioSource.PlayWithFadeIn(m_fadeOutValue, m_parent.masterVolume));
				})
			);
		}
		else
		{
			SetGenericCoroutine(m_trackMenu);
		}
	}

	private IEnumerator StartPlayingMenuList()
	{
		m_soundMenu.Shuffle();
		int i = 0;
		bool done = false;
		int prevFrame = 0;
		
		while(i < m_soundMenu.Count)
		{
			m_audioSource.clip = m_soundMenu[i];
			m_audioSource.audio.timeSamples = 0;
			m_parent.StartCoroutine(m_audioSource.PlayWithFadeIn(m_fadeOutValue, m_musicVolume));
			done = false;
			prevFrame = 0;
			
			while (!done) 
			{
				if(m_audioSource.clip != m_soundMenu[i])
				{
					m_audioSource.clip = m_soundMenu[i];
					m_audioSource.audio.timeSamples = prevFrame;
				}
				
				if(!m_audioSource.isPlaying)
				{
					m_parent.StartCoroutine(m_audioSource.PlayWithFadeIn(m_fadeOutValue, m_musicVolume));
				}
				
				if(prevFrame > m_audioSource.audio.timeSamples)
				{
					done = true;
					m_audioSource.Stop();//m_parent.StartCoroutine(m_audioSource.StopIss(0.5f));
					break;
				}
				
				prevFrame = m_audioSource.audio.timeSamples;
				
				yield return null;
			}
			
			i ++;
			
			if(i == m_soundMenu.Count)
				i = 0;
			
			yield return null;
		}
	}

	public void PlayTrackList()
	{
		if(m_audioSource.isPlaying)
		{
			m_parent.StartCoroutine
			(
				m_audioSource.PauseWithFadeOut(m_fadeOutValue, m_parent.trackTransitionVolume, () =>
                {
					SetGenericCoroutine(m_trackList);
				})
			);
		}
		else
		{
			SetGenericCoroutine(m_trackList);
		}
	}

	private IEnumerator StartPlayingTrackList()
	{
		m_soundTracks.Shuffle();
		int i = 0;
		bool done = false;
		int prevFrame = 0;

		while(i < m_soundTracks.Count)
		{
			m_audioSource.clip = m_soundTracks[i];
			m_audioSource.audio.timeSamples = 0;
			m_parent.StartCoroutine(m_audioSource.PlayWithFadeIn(m_fadeOutValue, m_musicVolume));
			done = false;
			prevFrame = 0;
			
			while (!done) 
			{
				if(m_audioSource.clip != m_soundTracks[i])
				{
					m_audioSource.clip = m_soundTracks[i];
					m_audioSource.audio.timeSamples = prevFrame;
				}
				
				if(!m_audioSource.isPlaying)
				{
					m_parent.StartCoroutine(m_audioSource.PlayWithFadeIn(m_fadeOutValue, m_musicVolume));
				}
				
				if(prevFrame > m_audioSource.audio.timeSamples)
				{
					done = true;
					m_audioSource.Stop();//m_parent.StartCoroutine(m_audioSource.StopIss(0.5f));
					break;
				}
				
				prevFrame = m_audioSource.audio.timeSamples;
				
				yield return null;
			}
			
			i ++;
			
			if(i == m_soundTracks.Count)
				i = 0;
			
			yield return null;
		}
	}

	public void PlaySoundFXOnce(string soundName, float volume = 0.5f)
	{
		for (int i = 0; i < m_soundFX.Count; i++) 
		{
			if(soundName == m_soundFX[i].name)
			{
				m_fxAudioSource.PlayOneShot(m_soundFX[i], volume);
				return;
			}
		}

		for (int i = 0; i < m_parent.soundsInGame.Length; i++) 
		{
			if(soundName == m_parent.soundsInGame[i].audioClip.name)
			{
				m_fxAudioSource.PlayOneShot(m_parent.soundsInGame[i].audioClip, volume);
				break;
			}
		}
	}
	
	public void PlayRandomSoundHitOnce(float volume = 0.5f)
	{
		m_fxAudioSource.PlayOneShot(m_soundHit[UnityEngine.Random.Range(0, m_soundHit.Count)], volume);
	}
	
	public void PlayRandomSoundMissOnce(float volume = 0.5f)
	{
		m_fxAudioSource.PlayOneShot(m_soundMiss[UnityEngine.Random.Range(0, m_soundMiss.Count)], volume);
	}
	
	public void PlayRandomSoundMultiplyOnce(float volume = 0.5f)
	{
		m_fxAudioSource.PlayOneShot(m_soundMultiply[UnityEngine.Random.Range(0, m_soundMultiply.Count)], volume);
	}
	
	public void SetSoundTrackVolume(float _volume)
	{
		m_musicVolume = _volume;
		
		if(m_musicVolume < 0.0f)
			m_musicVolume = 0.0f;
			
		if(m_musicVolume > 1.0f)
			m_musicVolume = 1.0f;
			
		m_audioSource.volume = m_musicVolume;
	}
	
	public float GetSoundTrackVolume()
	{
		return m_musicVolume;
	}
	
	public void SetFXVolume(float _volume)
	{
		m_fxVolume = _volume;
		
		if(m_fxVolume < 0.0f)
			m_fxVolume = 0.0f;
		
		if(m_fxVolume > 1.0f)
			m_fxVolume = 1.0f;
	}
	
	public float GetFXVolume()
	{
		return m_fxVolume;
	}
	
	public void ResetSound()
	{
		if(m_audioSource != null)
			m_audioSource.pitch = 1.0f;
	}
	
	public void UpPitch()
	{
		m_parent.StartCoroutine(this.AdjustPitch(1.0f));
	}
	
	public void DownPitch(float _value)
	{
		m_parent.StartCoroutine(this.AdjustPitch(_value));
	}
	
	private IEnumerator AdjustPitch(float times)
	{
		var targetPitch = m_audioSource.pitch + (0.1f * times);
		var step = targetPitch / 2.0f;
		
		while(m_audioSource.pitch < targetPitch)
		{
			m_audioSource.pitch += Time.deltaTime * step;
			
			yield return null;
		}
		m_audioSource.pitch = targetPitch;
	}

	public void SetCurrentVolumeToLow()
	{
		m_parent.StartCoroutine(m_audioSource.FadeOutSoundToValue(m_fadeOutValue, m_parent.lowVolume));
	}

	public void ResetCurrentVolumeToMax()
	{
		m_parent.StartCoroutine(m_audioSource.FadeInSoundToValue(m_fadeOutValue, m_musicVolume));
	}

	#region IDisposable
	public void Dispose()
	{
		m_fxAudioSource = null;
		m_audioSource = null;
		m_soundTracks = null;
		m_soundFX = null;
		m_soundHit = null;
		m_soundMiss = null;
		m_soundMultiply = null;

		Message.onGameStateChange -= OnGameStateChange;
	}
	#endregion 
}
