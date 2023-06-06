using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndTriggerSwitchN : MonoBehaviour {
	public GameEndTriggerSwitchN secondEnd;
	bool isTriggered;
	IEnumerator OnTriggerEnter(Collider collider) {
		isTriggered = true;
		if (secondEnd.isTriggered == true)
		{
			yield return new WaitForSeconds(5);
			SceneManager.LoadScene("Win");
		}
	}
}