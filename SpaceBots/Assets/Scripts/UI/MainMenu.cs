using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[DisallowMultipleComponent]
public class MainMenu : MonoBehaviour
{
	public Settings settings;
	public Image background;
	public Button playButton;
	public Button exitButton;
	public TextMeshProUGUI title;
	public AudioPlayer audioPlayer;
	public FadeCanvasGroup ui;
	public FadeCanvasGroup black;

	// Use this for initialization
	void Start()
	{
		background.sprite = settings.mainMenuBackground;
		title.text = settings.gametitle;

		playButton.onClick.AddListener(PlayGame);
		exitButton.onClick.AddListener(ExitGame);
		ui.FadeCompleteEvent += OnUIFadeCompleteEvent;
		black.FadeCompleteEvent += OnBlackFadeCompleteEvent;

		audioPlayer.PlayMusic(settings.mainMenuAudio);
	}

	// Update is called once per frame
	void Update()
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

	protected void OnMouseDown()
	{
		PlayGame();
	}

	private void PlayGame()
	{
		ui.Fade(1, 0, settings.fadeSpeed);
	}

	private void OnUIFadeCompleteEvent()
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
}
