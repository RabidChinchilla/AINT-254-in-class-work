using UnityEngine;
using System.Collections;

public class SoundManagerContainer : MonoBehaviour 
{
	public enum SoundType
	{
		soundTracks,
		endOfMatchSound,
		menuSound,
		soundFX,
		soundHit,
		soundMiss,
		soundMultiply
	}

	public float soundFadeInAndOutValue = 0.5f;
	[Range(0.0f, 1.0f)]
	public float masterVolume = 0.75f;
	[Range(0.0f, 1.0f)]
	public float lowVolume = 0.25f;
	[Range(0.0f, 1.0f)]
	public float trackTransitionVolume = 0.0f;

	[HideInInspector]
	public AudioSource audioSource;
	public AudioSource fxAudioSource;
	public SoundDataModel[] soundsInGame; 
	private IEnumerator m_currentCoroutine;

	private static bool isInstantiated = false;

	void Awake()
	{
		DontDestroyOnLoad(this);
	}

	// Use this for initialization
	void Start () 
	{
		if(!isInstantiated)
		{
			audioSource = gameObject.AddComponent<AudioSource>();
			fxAudioSource = gameObject.AddComponent<AudioSource>();
			StartCoroutine(RunCurrentCoroutine());

			isInstantiated = true;
		}
	}

	private IEnumerator RunCurrentCoroutine()
	{
		//SoundManager m_soundManager = new SoundManager();
		SoundManager.Instance.Initialise(this);

		while(true)
		{
			//Check if we have a current coroutine and MoveNext on it if we do
			if(m_currentCoroutine != null && m_currentCoroutine.MoveNext())
			{
				//Return whatever the coroutine yielded, so we will yield the
				//same thing
				yield return m_currentCoroutine.Current;
			}
			else
			{
				//RestartCoroutine();
				//Otherwise wait for the next frame
				yield return null;
			}
		}
	}

	public void SetCurrentCoroutine(IEnumerator coroutine)
	{
		m_currentCoroutine = coroutine;
	}

	public IEnumerator GetCurrentCoroutine()
	{
		return m_currentCoroutine;
	}

	void OnDestroy()
	{

	}
}
