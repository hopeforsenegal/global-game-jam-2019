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

	// Use this for initialization
	void Start()
	{
		background.sprite = settings.mainMenuBackground;
		title.text = settings.gametitle;

		playButton.onClick.AddListener(PlayGame);
		exitButton.onClick.AddListener(ExitGame);

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
		UnityEngine.SceneManagement.SceneManager.LoadScene(settings.gameScene);
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
