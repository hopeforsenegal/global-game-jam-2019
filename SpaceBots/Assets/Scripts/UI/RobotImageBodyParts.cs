using System;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
[RequireComponent(typeof(Image))]
public class RobotImageBodyParts : MonoBehaviour
{
	#region Enums and Constants

	#endregion

	#region Events

	#endregion

	#region Properties

	#endregion

	#region Inspectables

	public Settings settings;
	public PuzzleControler puzzleControler;

	#endregion

	#region Private Member Variables

	private Image m_Image;
	private GameController m_GameController;

	#endregion

	#region Monobehaviours

	protected void Awake()
	{
		m_Image = GetComponent<Image>();

		Debug.Assert(m_Image != null);
	}

	protected void Start()
	{
		var index = SceneIndex();
		m_Image.sprite = settings.scenes[index].robotImage;

		Anchors.DropCorrectEvent += OnDropCorrectEvent;
	}

	protected void OnDestroy()
	{
		Anchors.DropCorrectEvent -= OnDropCorrectEvent;
	}

	#endregion

	#region Public Methods

	#endregion

	#region Private Methods

	private void OnDropCorrectEvent()
	{
		var index = SceneIndex();
		var numImages = settings.scenes[index].robotBodyParts.Length;
		var scale = (int)Mathf.Floor(Scale(0, puzzleControler.finalScore, 0, numImages, puzzleControler.score));
		scale = Mathf.Clamp(scale, 0, numImages - 1);
		m_Image.sprite = settings.scenes[index].robotBodyParts[scale];
	}

	private int SceneIndex()
	{
		int index = 0;
		if (GameController.TryGetInstance(out m_GameController)) {
			index = m_GameController.sceneIndex;
		}
		return index;
	}


	public static float Scale(float oldMin, float oldMax, float newMin, float newMax, float valueToScale)
	{
		if (Mathf.Approximately(oldMin, newMin) && Mathf.Approximately(oldMax, newMax))
			return valueToScale;

		var oldRange = oldMax - oldMin;
		var newRange = newMax - newMin;
		var newValue = (valueToScale - oldMin) * newRange / oldRange + newMin;

		return newValue;
	}

	#endregion
}
