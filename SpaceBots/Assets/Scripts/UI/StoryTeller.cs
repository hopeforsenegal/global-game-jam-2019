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

	#endregion

	#region Private Member Variables

	private AudioPlayer m_AudioPlayer;
	private GameController m_GameController;

	#endregion

	#region Monobehaviours

	protected void Start()
	{
		fadeIn.FadeCompleteEvent += OnFadeCompleteEvent;
	}

	#endregion

	#region Public Methods

	#endregion

	#region Private Methods

	private void OnFadeCompleteEvent()
	{
		int index = 0;
		if (GameController.TryGetInstance(out m_GameController)) {
			index = m_GameController.sceneIndex;
		}
		var story = settings.scenes[index].stories[0];
		text.Show(story);
		if (AudioPlayer.TryGetInstance(out m_AudioPlayer)) {
			m_AudioPlayer.PlaySound(story.dialougeAudio);
		}
	}

	#endregion
}
