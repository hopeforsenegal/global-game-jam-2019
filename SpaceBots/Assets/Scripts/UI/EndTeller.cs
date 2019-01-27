using System;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class EndTeller : MonoBehaviour
{
	#region Enums and Constants

	#endregion

	#region Events

	#endregion

	#region Properties

	#endregion

	#region Inspectables

	public Settings settings;
	public FadeIn fadeIn;
	public DialougeText text;
	public FadeCanvasGroup exitBlack;
	public Image background;

	#endregion

	#region Private Member Variables

	private GameController m_GameController;
	private int m_StoryIndex;
	private bool m_Finished;

	#endregion

	#region Monobehaviours

	protected void Start()
	{
		text.DialougeShowCompleteEvent += OnDialougeShowCompleteEvent;
		text.DialougeHideEvent += OnDialougeHideEvent;
		fadeIn.FadeCompleteEvent += OnFadeCompleteEvent;
		exitBlack.FadeCompleteEvent += OnExitFadeCompleteEvent;
		m_StoryIndex = 0;
		if (GameController.TryGetInstance(out m_GameController)) {
			var ending = m_GameController.ending;
			var settingData = ending == 1 ? settings.endOne : settings.endTwo;
			background.sprite = settingData.robotImage;
		}
	}

	protected void OnDestroy()
	{
		text.DialougeShowCompleteEvent -= OnDialougeShowCompleteEvent;
		text.DialougeHideEvent -= OnDialougeHideEvent;
		fadeIn.FadeCompleteEvent -= OnFadeCompleteEvent;
		exitBlack.FadeCompleteEvent -= OnExitFadeCompleteEvent;
	}

	protected void Update()
	{
		if (!m_Finished)
			return;
		var hitEnterKey = Input.GetKey(KeyCode.KeypadEnter)
			|| Input.GetKey(KeyCode.Return);

		if (hitEnterKey) {
			GameController.Instance.ResetSceneToMainMenu();
		}
	}

	#endregion

	#region Public Methods

	#endregion

	#region Private Methods

	private void ShowNextStory()
	{
		int ending = 0;
		if (GameController.TryGetInstance(out m_GameController)) {
			ending = m_GameController.ending;
		}
		var settingData = ending == 1 ? settings.endOne : settings.endTwo;
		var stories = settingData.stories;
		var isEndScene = settingData.isEndScene;
		if (m_StoryIndex < stories.Length) {
			var story = stories[m_StoryIndex];
			m_StoryIndex++;
			text.Show(story);
		} else {
			exitBlack.Fade(0, 1, settings.fadeSpeed);
		}
	}

	private void OnFadeCompleteEvent()
	{
		if (!m_Finished) {
			ShowNextStory();
		}
	}

	private void OnDialougeShowCompleteEvent()
	{
		if (!m_Finished) {
			text.Hide();
		}
	}

	private void OnDialougeHideEvent()
	{
		if (!m_Finished) {
			ShowNextStory();
		}
	}

	private void OnExitFadeCompleteEvent()
	{
		m_Finished = true;
	}

	#endregion
}
