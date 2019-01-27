using System;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class MainMenu : MonoBehaviour
{
	#region Enums and Constants

	#endregion

	#region Events

	#endregion

	#region Properties

	#endregion

	#region Inspectables

	public Settings settings;
	public Image background;
	public Button playButton;
	public Button exitButton;
	public LerpUI lerpTitle;
	public FadeCanvasGroup ui;
	public FadeCanvasGroup black;

	#endregion

	#region Private Member Variables

	private AudioPlayer m_AudioPlayer;
	private GameController m_GameController;

	#endregion

	#region Monobehaviours

	protected void Start()
	{
		Debug.LogFormat("[{0}:Start]", name);

		Debug.Assert(GameController.TryGetInstance(out m_GameController), "m_GameController not set");
		Debug.Assert(AudioPlayer.TryGetInstance(out m_AudioPlayer), "m_AudioPlayer not set");

		background.sprite = settings.mainMenuBackground;

		playButton.onClick.AddListener(PlayGame);
		exitButton.onClick.AddListener(ExitGame);
		ui.FadeCompleteEvent += OnUIFadeCompleteEvent;
		black.FadeCompleteEvent += OnBlackFadeCompleteEvent;
		lerpTitle.TransitionCompleteEvent += OnTransitionCompleteEvent;

		m_AudioPlayer.PlayMusic(settings.mainMenuAudio);
	}

	protected void OnDestroy()
	{
		playButton.onClick.RemoveListener(PlayGame);
		exitButton.onClick.RemoveListener(ExitGame);
		ui.FadeCompleteEvent -= OnUIFadeCompleteEvent;
		black.FadeCompleteEvent -= OnBlackFadeCompleteEvent;
		lerpTitle.TransitionCompleteEvent -= OnTransitionCompleteEvent;
	}

	protected void Update()
	{
		var hitEnterKey = Input.GetKey(KeyCode.KeypadEnter)
			|| Input.GetKey(KeyCode.Return);

		var hitEscKey = Input.GetKey(KeyCode.Escape);

		if (hitEnterKey) {
			PlayGame();
		} else if (hitEscKey) {
			ExitGame();
		}
	}

	#endregion

	#region Public Methods

	#endregion

	#region Private Methods

	private void OnUIFadeCompleteEvent()
	{
		lerpTitle.enabled = true;
	}

	private void OnTransitionCompleteEvent()
	{
		black.Fade(0, 1, settings.fadeSpeed);
	}

	private void OnBlackFadeCompleteEvent()
	{
		m_GameController.LoadNextScene();
	}

	private void PlayGame()
	{
		Debug.LogFormat("[{0}:PlayGame]", name);

		m_AudioPlayer.PlaySoundDelay(settings.pickupSoundEffect, 0f);
		ui.Fade(1, 0, settings.fadeSpeed);
	}

	private void ExitGame()
	{
#if UNITY_EDITOR
		// Application.Quit() does not work in the editor so
		// UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}

	#endregion
}
