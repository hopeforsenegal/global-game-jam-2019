using System;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Settings", menuName = "ScriptableObjectData/Settings", order = 1)]
public class Settings : ScriptableObject
{
	[Header("UI")]
	[Tooltip("the main menu background")]
	public Sprite mainMenuBackground;
	public float fadeSpeed;


	[Header("General")]
	[Tooltip("the game's title")]
	public string gametitle;

	[Tooltip("the scene")]
	public SceneSettings[] scenes;


	[Header("Audio")]
	public AudioClip mainMenuAudio;
	public AudioClip pickupSoundEffect;
	public AudioClip dropCorrectSoundEffect;
	public AudioClip dropWrongSoundEffect;
	public AudioClip rotateSoundEffect;


	[Serializable]
	public struct SceneSettings
	{
		[Header("Audio")]
		[Tooltip("The scenes audio")]
		public AudioClip sceneAudio;


		[Header("General")]
		[Tooltip("the game scene")]
		public string sceneToLoad;
	}
}
