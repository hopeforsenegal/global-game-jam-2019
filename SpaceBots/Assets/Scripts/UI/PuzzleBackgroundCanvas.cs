using System;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class PuzzleBackgroundCanvas : MonoBehaviour
{
	#region Enums and Constants

	#endregion

	#region Events

	#endregion

	#region Properties

	#endregion

	#region Inspectables

	public Settings settings;
	public Image robotBackground;

	#endregion

	#region Private Member Variables

	#endregion

	#region Monobehaviours

	protected void Start()
	{
		robotBackground.sprite = settings.scenes[GameController.Instance.sceneIndex].robotImage;
	}

	#endregion

	#region Public Methods

	#endregion

	#region Private Methods

	#endregion
}
