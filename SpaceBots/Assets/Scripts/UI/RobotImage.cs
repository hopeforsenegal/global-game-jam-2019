using System;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
[RequireComponent(typeof(Image))]
public class RobotImage : MonoBehaviour
{
	#region Enums and Constants

	#endregion

	#region Events

	#endregion

	#region Properties

	#endregion

	#region Inspectables

	public Settings settings;

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
	}

	#endregion

	#region Public Methods

	#endregion

	#region Private Methods


	private int SceneIndex()
	{
		int index = 0;
		if (GameController.TryGetInstance(out m_GameController)) {
			index = m_GameController.sceneIndex;
		}
		return index;
	}

	#endregion
}
