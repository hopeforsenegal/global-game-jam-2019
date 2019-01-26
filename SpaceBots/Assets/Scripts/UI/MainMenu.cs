﻿using System;
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
	public Text title;
	public LerpUI lerpTitle;
	public AudioPlayer audioPlayer;
	public FadeCanvasGroup ui;
	public FadeCanvasGroup black;

	#endregion

	#region Private Member Variables

	#endregion

	#region Monobehaviours

	protected void Start()
	{
		background.sprite = settings.mainMenuBackground;
		title.text = settings.gametitle;

		playButton.onClick.AddListener(PlayGame);
		exitButton.onClick.AddListener(ExitGame);
		ui.FadeCompleteEvent += OnUIFadeCompleteEvent;
		black.FadeCompleteEvent += OnBlackFadeCompleteEvent;
		lerpTitle.TransitionCompleteEvent += OnTransitionCompleteEvent;

		audioPlayer.PlayMusic(settings.mainMenuAudio);
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


	private void PlayGame()
	{
		ui.Fade(1, 0, settings.fadeSpeed);
	}

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
		GameController.Instance.sceneIndex = 0;
		UnityEngine.SceneManagement.SceneManager.LoadScene(settings.scenes[GameController.Instance.sceneIndex].sceneToLoad);
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
