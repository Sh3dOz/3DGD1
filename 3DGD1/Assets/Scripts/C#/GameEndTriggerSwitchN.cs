using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndTriggerSwitchN : MonoBehaviour {
	bool isTriggered;
	public GameEndTriggerSwitchN secondWin;
	public bool main;
	LevelManager manager;
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
		PlayerPrefs.SetFloat(SceneManager.GetActiveScene().ToString() + "Time Taken", CountDownTime.timeTaken);
		PlayerPrefs.SetInt(SceneManager.GetActiveScene().ToString() + "Steps Taken", manager.steps);
		yield return new WaitForSeconds(1);
		SceneManager.LoadScene("Win");
	}
}