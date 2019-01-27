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
	[Tooltip("the game's starting scene")]
	public string initialScene;

	[Header("Audio")]
	public AudioClip mainMenuAudio;
	public AudioClip[] pickupSoundEffect;
	public AudioClip dropCorrectSoundEffect;
	public AudioClip dropWrongSoundEffect;
	public AudioClip[] rotateSoundEffect;


	[Header("General")]
	[Tooltip("the scenes")]
	public SceneSettings[] scenes;
	public SceneSettings endOne;
	public SceneSettings endTwo;

	[Serializable]
	public struct SceneStory
	{
		public string dialouge;
		public AudioClip dialougeAudio;
		public int dialougeTime;
	}

	[Serializable]
	public struct SceneSettings
	{
		[Header("Audio")]
		[Tooltip("The scenes audio")]
		public AudioClip sceneAudio;


		[Header("General")]
		[Tooltip("the game scene")]
		public string sceneToLoad;
		public string theme;
		public bool isEndScene;

		[Header("Render")]
		[Tooltip("the robot image")]
		public Sprite robotImage;

		[Header("Story")]
		[Tooltip("the story")]
		public SceneStory[] stories;
	}
}
