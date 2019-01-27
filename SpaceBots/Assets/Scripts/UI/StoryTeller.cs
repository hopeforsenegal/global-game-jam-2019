using System;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class StoryTeller : MonoBehaviour
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

	#endregion

	#region Private Member Variables

	private GameController m_GameController;
	private int m_StoryIndex;

	#endregion

	#region Monobehaviours

	protected void Start()
	{
		text.DialougeShowCompleteEvent += OnDialougeShowCompleteEvent;
		text.DialougeHideEvent += OnDialougeHideEvent;
		fadeIn.FadeCompleteEvent += OnFadeCompleteEvent;
		m_StoryIndex = 0;
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

	private void ShowNextStory()
	{
		var index = SceneIndex();
		var stories = settings.scenes[index].stories;
		if (m_StoryIndex < stories.Length) {
			var story = stories[m_StoryIndex];
			m_StoryIndex++;
			text.Show(story);
		} else {
			exitBlack.Fade(0, 1, settings.fadeSpeed);
			exitBlack.FadeCompleteEvent += OnExitFadeCompleteEvent;
		}
	}

	private void OnFadeCompleteEvent()
	{
		ShowNextStory();
	}

	private void OnDialougeShowCompleteEvent()
	{
		text.Hide();
	}

	private void OnDialougeHideEvent()
	{
		ShowNextStory();
	}

	private void OnExitFadeCompleteEvent()
	{
		GameController.Instance.LoadNextScene();
	}

	#endregion
}
