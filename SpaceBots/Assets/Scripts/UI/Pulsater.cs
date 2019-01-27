using System;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class Pulsater : MonoBehaviour
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

	protected void Update()
	{
		m_Renderer.material.color = Color.Lerp(Color.white, Color.black, Mathf.PingPong(Time.time, 1));
	}

	#endregion

	#region Public Methods

	#endregion

	#region Private Methods

	#endregion
}
