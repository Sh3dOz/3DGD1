using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]

public class BackToMain : MonoBehaviour {
	public AudioSource SFX;
	public AudioClip UISelect;

	public void LoadGame()
    {
		SFX.PlayOneShot(UISelect);
		SceneManager.LoadScene("Game");
	}

	public void backToMain()
    {
		SFX.PlayOneShot(UISelect);
		SceneManager.LoadScene("Title");
	}
}