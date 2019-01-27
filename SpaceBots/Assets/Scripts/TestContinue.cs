using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestContinue : MonoBehaviour
{
	public Settings settings;
	private bool m_Active;
	private GameController m_GameController;

	// Start is called before the first frame update
	void Start()
	{
		var index = SceneIndex();
		var stories = settings.scenes[index].stories;
		var isEndScene = settings.scenes[index].isEndScene;
		if (!isEndScene) {
			StartCoroutine(OnTimerShow(1));
		}
		m_Active = false;
	}

	// Update is called once per frame
	//oid Update()
	//
	//	if (!m_Active)
	//		return;
	//	
	//	var hitEnterKey = Input.GetKey(KeyCode.KeypadEnter)
	//		|| Input.GetKey(KeyCode.Return);
    //
	//	var hitEscKey = Input.GetKey(KeyCode.Escape);
    //
	//	if (hitEnterKey) {
	//		GameController.Instance.LoadNextScene();
	//	}
	//
    //

	private IEnumerator OnTimerShow(float delay)
	{
		yield return new WaitForSeconds(delay);
		m_Active = true;

	}

	private int SceneIndex()
	{
		int index = 0;
		if (GameController.TryGetInstance(out m_GameController)) {
			index = m_GameController.sceneIndex;
		}
		return index;
	}
}
