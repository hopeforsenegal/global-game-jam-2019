
using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class GameController : MonoBehaviour
{
	#region Singleton

	public static GameController Instance
	{
		get
		{
			return sInstance;
		}
	}

	public static bool TryGetInstance(out GameController returnVal)
	{
		returnVal = sInstance;
		if (returnVal == null) {
			Debug.LogWarning(string.Format("Couldn't access {0}", typeof(GameController).Name));
		}
		return (returnVal != null);
	}

	private static GameController sInstance;

	#endregion

	#region Enums and Constants

	#endregion

	#region Events

	#endregion

	#region Properties

	public int sceneIndex;

	#endregion

	#region Inspectables

	#endregion

	#region Private Member Variables

	#endregion

	#region Monobehaviours

	protected void Awake()
	{
		if (sInstance == null) {
			sInstance = this;
		} else if (sInstance != this) {
			DestroyObject(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);
	}

	protected void Start()
	{
	}

	protected void Update()
	{
	}

	protected void OnEnable()
	{
	}

	protected void OnDisable()
	{
	}

	protected void OnDestroy()
	{
		sInstance = null;
	}

	#endregion

	#region Public Methods

	#endregion

	#region Private Methods

	#endregion
}
