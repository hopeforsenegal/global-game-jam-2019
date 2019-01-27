using System;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class TutorialHighlightPiece : MonoBehaviour
{
	#region Enums and Constants

	#endregion

	#region Events

	#endregion

	#region Properties

	#endregion

	#region Inspectables

	#endregion

	#region Private Member Variables

	private PuzzlePiece m_PuzzlePiece;
	private Pulsater m_Pulsater;

	#endregion

	#region Monobehaviours

	protected void Awake()
	{
		m_Pulsater = GetComponent<Pulsater>();

		Debug.Assert(m_Pulsater != null);
	}

	protected void Start()
	{
		m_PuzzlePiece = GetComponentInParent<PuzzlePiece>();

		Debug.Assert(m_PuzzlePiece != null);
	}

	protected void Update()
	{
		if (m_PuzzlePiece != null && m_PuzzlePiece.hasBeenPickedUp) {
			m_Pulsater.enabled = false;
			gameObject.active = false;
		}
	}

	#endregion

	#region Public Methods

	#endregion

	#region Private Methods

	#endregion
}
