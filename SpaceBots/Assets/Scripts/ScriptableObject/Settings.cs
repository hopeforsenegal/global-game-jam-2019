using System;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Settings", menuName = "ScriptableObjectData/Settings", order = 1)]
public class Settings : ScriptableObject
{
	[Header("UI")]
	[Tooltip("the main menu background")]
	public Sprite mainMenuBackground;


	[Header("General")]
	[Tooltip("the game's title")]
	public string gametitle;

	[Tooltip("the game scene")]
	public string gameScene;


	[Header("Audio")]
	[Tooltip("the game's title")]
	public AudioClip mainMenuAudio;
}
