using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestContinue : MonoBehaviour
{
	public Settings settings;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		var hitEnterKey = Input.GetKey(KeyCode.KeypadEnter)
			|| Input.GetKey(KeyCode.Return);

		var hitEscKey = Input.GetKey(KeyCode.Escape);

		if (hitEnterKey) {
			NextScene();
		}
	}

	void NextScene()
	{
		GameController.Instance.sceneIndex++;
		UnityEngine.SceneManagement.SceneManager.LoadScene(settings.scenes[GameController.Instance.sceneIndex].sceneToLoad);
	}
}
