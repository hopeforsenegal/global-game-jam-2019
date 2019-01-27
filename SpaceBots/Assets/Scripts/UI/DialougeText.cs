using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class DialougeText : MonoBehaviour
{
	#region Enums and Constants

	#endregion

	#region Events

	public event Action DialougeShowCompleteEvent;
	public event Action DialougeHideEvent;

	#endregion

	#region Properties

	#endregion

	#region Inspectables

	private AudioPlayer m_AudioPlayer;
	private Text m_Text;
	private FadeCanvasGroup m_TextFadeCanvasGroup;
	private LerpUI m_TextLerpUI;

	#endregion

	#region Private Member Variables

	private Vector3 m_OriginalPosition;

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

	protected void Start()
	{
		m_OriginalPosition = GetComponent<RectTransform>().localPosition;
	}

	#endregion

	#region Public Methods

	public void Show(Settings.SceneStory story)
	{
		if (AudioPlayer.TryGetInstance(out m_AudioPlayer)) {
			m_AudioPlayer.PlaySound(story.dialougeAudio);
		}
		m_TextFadeCanvasGroup.Fade(0f, 1f, 1f);
		m_Text.text = story.dialouge;
		m_TextLerpUI.enabled = true;
		StartCoroutine(OnTimerShow(story.dialougeTime));
	}

	public void Hide()
	{
		m_TextFadeCanvasGroup.Fade(1, 0f, 0.3f);
		StartCoroutine(OnTimerHide(0.3f));
	}

	public void Reset()
	{
		m_Text.text = "";
		m_TextFadeCanvasGroup.Fade(0f, 0f, 0f);
		m_TextLerpUI.enabled = false;
		m_TextLerpUI.transform.localPosition = m_OriginalPosition;
	}

	#endregion

	#region Private Methods

	private IEnumerator OnTimerHide(float delay)
	{
		yield return new WaitForSeconds(delay);
		Reset();
		var invokeEvent = DialougeHideEvent;
		if (invokeEvent != null) {
			invokeEvent();
		}
	}

	private IEnumerator OnTimerShow(float delay)
	{
		yield return new WaitForSeconds(delay);
		var invokeEvent = DialougeShowCompleteEvent;
		if (invokeEvent != null) {
			invokeEvent();
		}
	}

	#endregion
}
