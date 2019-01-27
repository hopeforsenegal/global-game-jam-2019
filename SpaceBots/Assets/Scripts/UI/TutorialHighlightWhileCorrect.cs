using System;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class TutorialHighlightWhileCorrect : MonoBehaviour
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
		Anchors.DropCorrectEvent += OnDropCorrectEvent;
	}

	private void OnDropCorrectEvent()
	{
		m_Pulsater.enabled = false;
		gameObject.active = false;
	}

	#endregion

	#region Public Methods

	#endregion

	#region Private Methods

	#endregion
}
