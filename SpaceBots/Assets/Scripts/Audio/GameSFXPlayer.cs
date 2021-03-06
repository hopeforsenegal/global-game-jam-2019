﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSFXPlayer : MonoBehaviour
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
			PuzzleControler.PickupEvent += OnPickupSoundEffect;
			PuzzleControler.DropWrongEvent += OnPickupSoundEffect;
			PuzzleControler.RotateEvent += OnPickupSoundEffect;
			PuzzlePiece.PickupEvent += OnPickupSoundEffect;
			PuzzlePiece.DropWrongEvent += OnPickupSoundEffect;
			PuzzlePiece.RotateEvent += OnPickupSoundEffect;
			Anchors.DropCorrectEvent += OnDropCorrectSoundEffect;
		}
	}

	#endregion

	#region Public Methods

	#endregion

	#region Private Methods

	private void OnPickupSoundEffect()
	{
		m_AudioPlayer.PlaySoundDelay(settings.pickupSoundEffect, 0f);
	}

	private void OnDropCorrectSoundEffect()
	{
		m_AudioPlayer.PlaySound(settings.dropCorrectSoundEffect);
	}

	private void OnDropWrongSoundEffect()
	{
		m_AudioPlayer.PlaySound(settings.dropWrongSoundEffect);
	}

	private void OnRotateSoundEffect()
	{
		m_AudioPlayer.PlaySoundDelay(settings.rotateSoundEffect, 0f);
	}

	#endregion
}