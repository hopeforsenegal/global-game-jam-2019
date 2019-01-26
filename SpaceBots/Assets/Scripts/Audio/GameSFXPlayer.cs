using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSFXPlayer : MonoBehaviour
{
	public Settings settings;

	private AudioPlayer m_AudioPlayer;

	// Start is called before the first frame update
	void Start()
	{
		if (AudioPlayer.TryGetInstance(out m_AudioPlayer)) {
			PuzzleControler.PickupEvent += OnPickupSoundEffect;
			PuzzleControler.DropCorrectEvent += OnDropCorrectSoundEffect;
			PuzzleControler.DropWrongEvent += OnPickupSoundEffect;
			PuzzleControler.RotateEvent += OnPickupSoundEffect;
			PuzzlePiece.PickupEvent += OnPickupSoundEffect;
			PuzzlePiece.DropCorrectEvent += OnDropCorrectSoundEffect;
			PuzzlePiece.DropWrongEvent += OnPickupSoundEffect;
			PuzzlePiece.RotateEvent += OnPickupSoundEffect;
		}
	}

	void OnPickupSoundEffect()
	{
		m_AudioPlayer.PlaySound(settings.pickupSoundEffect);
	}

	void OnDropCorrectSoundEffect()
	{
		m_AudioPlayer.PlaySound(settings.dropCorrectSoundEffect);
	}

	void OnDropWrongSoundEffect()
	{
		m_AudioPlayer.PlaySound(settings.dropWrongSoundEffect);
	}

	void OnRotateSoundEffect()
	{
		m_AudioPlayer.PlaySound(settings.rotateSoundEffect);
	}
}