using System;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class DialougeText : MonoBehaviour
{
	#region Enums and Constants

	#endregion

	#region Events

	#endregion

	#region Properties

	#endregion

	#region Inspectables

	private Text m_Text;
	private FadeCanvasGroup m_TextFadeCanvasGroup;
	private LerpUI m_TextLerpUI;

	#endregion

	#region Private Member Variables

	#endregion

	#region Monobehaviours

	protected void Awake()
	{
		m_Text = GetComponent<Text>();
		m_TextFadeCanvasGroup = GetComponent<FadeCanvasGroup>();
		m_TextLerpUI = GetComponent<LerpUI>();

		Debug.Assert(m_Text != null);
		Debug.Assert(m_TextFadeCanvasGroup != null);
		Debug.Assert(m_TextLerpUI != null);
	}

	#endregion

	#region Public Methods

	public void Show(Settings.SceneStory story)
	{
		m_Text.text = story.dialouge;
		m_TextFadeCanvasGroup.Fade(0f, 1f, 1f);
		m_TextLerpUI.enabled = true;
	}

	#endregion

	#region Private Methods

	#endregion
}
