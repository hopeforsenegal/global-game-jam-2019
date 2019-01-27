
using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class GameController : MonoBehaviour
{
	#region Singleton

	public static GameController Instance
	{
		get;
		private set;
	}

	public static bool TryGetInstance(out GameController controller)
	{
		controller = Instance;
		return controller != null;
	}

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
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);
	}

	protected void Start()
	{
		m_SceneIndex = -1;
		ending = 0;
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
		if (Instance != this)
			return;
		Instance = null;
	}

	#endregion

	#region Public Methods

	public void ResetSceneToMainMenu()
	{
		m_SceneIndex = -1;
		ending = 0;
		Debug.LogFormat("[{0}:ResetSceneToMainMenu] scene:{1}", name, settings.initialScene);

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
		Debug.LogFormat("[{0}:LoadNextScene] scene:{1} m_SceneIndex:{2}", name, sceneToLoad, m_SceneIndex);
		UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);
	}

	#endregion

	#region Private Methods

	#endregion
}
