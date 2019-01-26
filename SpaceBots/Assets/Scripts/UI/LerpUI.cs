using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpUI : MonoBehaviour
{
	#region Enums and Constants

	#endregion

	#region Events

	public event Action TransitionCompleteEvent;

	#endregion

	#region Properties

	#endregion

	#region Inspectables

	public RectTransform endTranform;

	#endregion

	#region Private Member Variables

	private RectTransform m_FaceButton;
	private Vector3 m_NewPos;
	private Vector3 m_ButtonVelocity = Vector3.zero;
	private float m_SmoothTime = 0.5f;

	#endregion

	#region Monobehaviours

	protected void Start()
	{
		//Get the RectTransform component
		m_FaceButton = GetComponent<RectTransform>();
		m_NewPos = endTranform.localPosition;
	}

	protected void Update()
	{
		//Update the localPosition towards the newPos
		m_FaceButton.localPosition = Vector3.SmoothDamp(m_FaceButton.localPosition, m_NewPos, ref m_ButtonVelocity, m_SmoothTime);
		if (IsEqualWithinPrecision(m_FaceButton.localPosition.y, m_NewPos.y, 0.1f)) {
			var invokeEvent = TransitionCompleteEvent;
			if (invokeEvent != null) {
				invokeEvent();
			}
			enabled = false;
		}
	}

	#endregion

	#region Public Methods

	#endregion

	#region Private Methods

	private static bool IsEqualWithinPrecision(float a, float b, float precision)
	{
		return Mathf.Abs(a - b) < precision || Mathf.Approximately(Mathf.Abs(a - b), 0f);
	}

	#endregion
}
