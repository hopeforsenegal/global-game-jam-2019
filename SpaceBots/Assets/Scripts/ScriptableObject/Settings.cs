using System;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Settings", menuName = "ScriptableObjectData/Settings", order = 1)]
public class Settings : ScriptableObject
{
	[Header("UI")]
	[Tooltip("the main menu background")]
	public Image mainMenuBackground;

	[Header("General")]
	[Tooltip("the main game scene")]
	public string gameScene;
}
