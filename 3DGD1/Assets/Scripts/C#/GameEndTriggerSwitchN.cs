using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndTriggerSwitchN : MonoBehaviour {
	bool isTriggered;
	public GameEndTriggerSwitchN secondWin;
	public bool main;
	LevelManager manager;
    private void Start()
    {
		manager = FindObjectOfType<LevelManager>();
    }
    private void Update()
    {
		if (main)
		{
			if (isTriggered && secondWin.isTriggered == true)
			{
				StartCoroutine(manager.Score());
				LoadWin();
			}
		}
	}
    void OnTriggerEnter(Collider collider)
	{
		if (collider.GetComponent<CubeRoll>())
		{
			collider.GetComponent<CubeRoll>().canMove = false;
			isTriggered = true;
		}
	}

	void LoadWin()
    {
		PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name, 1);
		PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "Time Taken", CountDownTime.timeTaken);
		PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "Steps Taken", manager.steps);
	}
}