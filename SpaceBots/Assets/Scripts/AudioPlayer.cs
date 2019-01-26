
using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class AudioPlayer : MonoBehaviour
{
	#region Singleton

	public static AudioPlayer Instance
	{
		get
		{
			return sInstance;
		}
	}

	public static bool TryGetInstance(out AudioPlayer returnVal)
	{
		returnVal = sInstance;
		if (returnVal == null) {
			Debug.LogWarning(string.Format("Couldn't access {0}", typeof(AudioPlayer).Name));
		}
		return (returnVal != null);
	}

	private static AudioPlayer sInstance;

	#endregion

	#region Enums and Constants

	#endregion

	#region Events

	#endregion

	#region Properties

	#endregion

	#region Inspectables

	#endregion

	#region Private Member Variables

	private AudioSource[] m_AudioSources;

	#endregion

	#region Monobehaviours

	protected void Awake()
	{
		if (sInstance == null) {
			sInstance = this;
		} else if (sInstance != this) {
			DestroyObject(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);
	}

	protected void Start()
	{
		m_AudioSources = GetComponents<AudioSource>();
	}

	protected void Update()
	{
	}

	protected void OnEnable()
	{
	}

	protected void OnDisable()
	{
	}

	protected void OnDestroy()
	{
		sInstance = null;
	}

	#endregion

	#region Public Methods

	#endregion

	#region Private Methods

	public void PlaySound(AudioClip audio)
	{
		bool soundPlayed = false;
		foreach (AudioSource source in m_AudioSources) {
			if (!source.isPlaying) {
				source.clip = audio;
				source.Play();
				soundPlayed = true;
				break;
			}
		}
		if (!soundPlayed) {
			if (m_AudioSources != null && m_AudioSources.Length > 0) {
				m_AudioSources[0].clip = audio;
				m_AudioSources[0].Play();
			}
		}
	}

	public void PlaySoundDelay(AudioClip[] audio, float delay)
	{
		int n = Random.Range(0, audio.Length);
		PlaySoundDelay(audio[n], delay);
	}

	public void PlaySoundDelay(AudioClip audio, float delay)
	{
		StartCoroutine(SoundPlaying(audio, delay));
	}

	private IEnumerator SoundPlaying(AudioClip audio, float delay)
	{
		yield return new WaitForSeconds(delay);
		PlaySound(audio);
	}
	#endregion
}
