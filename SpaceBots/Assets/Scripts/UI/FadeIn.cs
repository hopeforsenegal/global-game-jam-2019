using System;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
[RequireComponent(typeof(FadeCanvasGroup))]
public class FadeIn : MonoBehaviour
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

	private FadeCanvasGroup m_FadeCanvasGroup;

	#endregion

	#region Monobehaviours

	protected void Awake()
	{
		m_FadeCanvasGroup = GetComponent<FadeCanvasGroup>();

		Debug.Assert(m_FadeCanvasGroup != null);
	}

	protected void Start()
	{
		m_FadeCanvasGroup.Fade(1f, 0f, settings.fadeSpeed);
	}

	#endregion

	#region Public Methods

	#endregion

	#region Private Methods

	#endregion
}
