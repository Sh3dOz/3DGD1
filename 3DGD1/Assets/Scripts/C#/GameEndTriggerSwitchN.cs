using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndTriggerSwitchN : MonoBehaviour {

	IEnumerator OnTriggerEnter(Collider collider)
	{
		if (collider.GetComponent<CubeRoll>())
		{
			yield return new WaitForSeconds(1);
			SceneManager.LoadScene("Win");
		}
	}
}