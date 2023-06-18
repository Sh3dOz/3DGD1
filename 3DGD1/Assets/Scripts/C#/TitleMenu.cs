using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour {

	public AudioSource SFX;
	public AudioClip UISelect;

	public void StartGame()
    {
		SFX.PlayOneShot(UISelect);
		if (PlayerPrefs.GetFloat("OpenGame") == 0)
		{
			PlayerPrefs.SetFloat("OpenGame", 1);
			SceneManager.LoadScene("Instructions");
		}
		else
		{
			SceneManager.LoadScene("Level Select");
		}
	}

	public void Options()
    {
		SFX.PlayOneShot(UISelect);
		SceneManager.LoadScene("Options");
	}

	public void Credits()
    {
		SFX.PlayOneShot(UISelect);
		SceneManager.LoadScene("Credits");
	}

	public void QuitGame()
    {
		SFX.PlayOneShot(UISelect);
		Application.Quit();
	}
}