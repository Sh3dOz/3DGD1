using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndTriggerSwitchN : MonoBehaviour {
	bool isTriggered;
	public GameEndTriggerSwitchN secondWin;
	public bool main;
    private void Update()
    {
		if (main)
		{
			if (isTriggered && secondWin.isTriggered == true)
			{
				StartCoroutine(LoadWin());
			}
		}
	}
    void OnTriggerEnter(Collider collider)
	{
		if (collider.GetComponent<CubeRoll>())
		{
			isTriggered = true;
		}
	}

	IEnumerator LoadWin()
    {
		PlayerPrefs.SetFloat(SceneManager.GetActiveScene().ToString(), 1);
		yield return new WaitForSeconds(1);
		SceneManager.LoadScene("Win");
	}
}