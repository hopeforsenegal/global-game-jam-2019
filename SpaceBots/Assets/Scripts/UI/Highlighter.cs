using System;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class Highlighter : MonoBehaviour
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

	private Renderer m_Renderer;
	private Color m_StartColor;

	#endregion

	#region Monobehaviours

	protected void Awake()
	{
		m_Renderer = GetComponent<Renderer>();
		Debug.Assert(m_Renderer != null);
	}

	protected void Start()
	{
		m_StartColor = m_Renderer.material.color;
	}

	#endregion

	#region Public Methods

	public void Highlight()
	{
		m_Renderer.material.color = settings.highlightColor;
	}

	public void Unhighlight()
	{
		m_Renderer.material.color = m_StartColor;
	}

	#endregion

	#region Private Methods

	#endregion
}
