using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusicPlayer : MonoBehaviour
{
	#region Enums and Constants

	#endregion

	#region Events

	#endregion

	#region Properties

	#endregion

	#region Inspectables

	public Settings settings;

	#endregion

	#region Private Member Variables

	private AudioPlayer m_AudioPlayer;

	#endregion

	#region Monobehaviours

	protected void Start()
	{
		if (AudioPlayer.TryGetInstance(out m_AudioPlayer)) {
			var audio = settings.scenes[GameController.Instance.sceneIndex].sceneAudio;
			m_AudioPlayer.PlayMusic(audio);
		}
	}

	#endregion

	#region Public Methods

	#endregion

	#region Private Methods

	#endregion
}