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

	public Text title;
	public Settings settings;
	public FadeIn fadeIn;
	public DialougeText text;
	public FadeCanvasGroup exitBlack;

	public FadeCanvasGroup theEndGame;
	public Button endOneButton;
	public Button endTwoButton;

	#endregion

	#region Private Member Variables

	private GameController m_GameController;
	private int m_StoryIndex;
	private bool m_stayOff;

	#endregion

	#region Monobehaviours

	protected void Start()
	{
		var index = SceneIndex();
		title.text = settings.scenes[index].theme;
		endOneButton.onClick.AddListener(EndingOne);
		endTwoButton.onClick.AddListener(EndingTwo);
		text.DialougeShowCompleteEvent += OnDialougeShowCompleteEvent;
		text.DialougeHideEvent += OnDialougeHideEvent;
		fadeIn.FadeCompleteEvent += OnFadeCompleteEvent;
		exitBlack.FadeCompleteEvent += OnExitFadeCompleteEvent;
		m_StoryIndex = 0;
		m_stayOff = false;
	}

	protected void OnDestroy()
	{
		text.DialougeShowCompleteEvent -= OnDialougeShowCompleteEvent;
		text.DialougeHideEvent -= OnDialougeHideEvent;
		fadeIn.FadeCompleteEvent -= OnFadeCompleteEvent;
		exitBlack.FadeCompleteEvent -= OnExitFadeCompleteEvent;
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
		var isEndScene = settings.scenes[index].isEndScene;
		if (m_StoryIndex < stories.Length) {
			var story = stories[m_StoryIndex];
			m_StoryIndex++;
			text.Show(story);
		} else {
			if (!isEndScene) {
				exitBlack.Fade(0, 1, settings.fadeSpeed);
			} else {
				if (!m_stayOff) {
					m_stayOff = true;
					theEndGame.Fade(0f, 1f, settings.fadeSpeed);
					theEndGame.UpdateInteractable(true);
				}
			}
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
		var index = SceneIndex();
		var isEndScene = settings.scenes[index].isEndScene;
		if (!isEndScene) {
			GameController.Instance.LoadNextScene();
		} else {
			if (!m_stayOff) {
				m_stayOff = true;
				Debug.LogFormat("[{0}:OnExitFadeCompleteEvent] fadeSpeed:{1}", name, settings.fadeSpeed);
				theEndGame.Fade(0f, 1f, settings.fadeSpeed);
				theEndGame.UpdateInteractable(true);
			}
		}
	}

	private void EndingOne()
	{
		GameController.Instance.ending = 1;
		UnityEngine.SceneManagement.SceneManager.LoadScene(settings.endOne.sceneToLoad);
	}

	private void EndingTwo()
	{
		GameController.Instance.ending = 2;
		UnityEngine.SceneManagement.SceneManager.LoadScene(settings.endTwo.sceneToLoad);
	}

	#endregion
}
