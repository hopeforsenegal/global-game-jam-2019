using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpUI : MonoBehaviour
{
	public event Action TransitionCompleteEvent;


	public RectTransform endTranform;

	//Declare RectTransform in script
	RectTransform faceButton;
	//The new position of your button
	Vector3 newPos;
	//Reference value used for the Smoothdamp method
	private Vector3 buttonVelocity = Vector3.zero;
	//Smooth time
	private float smoothTime = 0.5f;

	void Start()
	{
		//Get the RectTransform component
		faceButton = GetComponent<RectTransform>();
		newPos = endTranform.localPosition;
	}

	void Update()
	{
		//Update the localPosition towards the newPos
		faceButton.localPosition = Vector3.SmoothDamp(faceButton.localPosition, newPos, ref buttonVelocity, smoothTime);
		if (IsEqualWithinPrecision(faceButton.localPosition.y, newPos.y, 0.1f)) {
			var invokeEvent = TransitionCompleteEvent;
			if (invokeEvent != null) {
				invokeEvent();
			}
			enabled = false;
		}
	}


	public static bool IsEqualWithinPrecision(float a, float b, float precision)
	{
		return Mathf.Abs(a - b) < precision || Mathf.Approximately(Mathf.Abs(a - b), 0f);
	}
}
