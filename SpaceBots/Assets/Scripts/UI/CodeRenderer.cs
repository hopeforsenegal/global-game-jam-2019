using System;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class CodeRenderer : MonoBehaviour
{
	#region Enums and Constants

	#endregion

	#region Events

	#endregion

	#region Properties

	#endregion

	#region Inspectables

	public Code code;
	public Text text;

	#endregion

	#region Private Member Variables

	private RectTransform m_TextTransform;
	private Vector3 m_InitialPosition;

	#endregion

	#region Monobehaviours

	protected void Start()
	{
		text.text = code.text;
		m_TextTransform = text.rectTransform;
		m_InitialPosition = m_TextTransform.localPosition;
	}

	protected void Update()
	{
		//m_TextTransform.localPosition.y += code.scrollSpeed;
		m_TextTransform.localPosition = new Vector3(m_TextTransform.localPosition.x, m_TextTransform.localPosition.y + code.scrollSpeed);
		if (m_TextTransform.localPosition.y > 2000) {
			m_TextTransform.localPosition = m_InitialPosition;
		}
	}

	#endregion

	#region Public Methods

	#endregion

	#region Private Methods

	#endregion
}
