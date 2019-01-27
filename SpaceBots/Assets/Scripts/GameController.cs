
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

	public int sceneIndex
	{
		get
		{
			return m_SceneIndex;
		}
	}

	#endregion

	#region Inspectables

	public int ending;
	public Settings settings;

	#endregion

	#region Private Member Variables

	private int m_SceneIndex;

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
		m_SceneIndex = -1;
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

	public void ResetSceneToMainMenu()
	{
		m_SceneIndex = -1;
		UnityEngine.SceneManagement.SceneManager.LoadScene(settings.initialScene);
	}

	public void LoadNextScene()
	{
		m_SceneIndex++;
		string sceneToLoad = "";
		if (settings.scenes != null && settings.scenes.Length > 0 && m_SceneIndex < settings.scenes.Length) {
			sceneToLoad = settings.scenes[m_SceneIndex].sceneToLoad;
		} else {
			sceneToLoad = settings.initialScene;
		}
		UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);
	}

	#endregion

	#region Private Methods

	#endregion
}
