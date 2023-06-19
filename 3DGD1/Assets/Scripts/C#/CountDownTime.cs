using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CountDownTime : MonoBehaviour {

	private float startTime = 0f; // Time taken to complete game
	public static float timeTaken;

	void Start() {
		GetComponent<Text>().material.color = Color.white; // GUI text color
	}

	void Update() {
		CountDown();
	}

	void CountDown() {
		timeTaken = startTime + Time.timeSinceLevelLoad;
		ShowTime();
	}

	void ShowTime() {
		int minutes;
		int seconds;
		string timeString;

		minutes = (int)timeTaken / 60; // Derive minutes by dividing seconds by 60 seconds
		seconds = (int)timeTaken % 60; // Derive remainder after dividing by 60 seconds
		timeString = "Time Taken > " + minutes.ToString() + ":" + seconds.ToString("d2");
		GetComponent<Text>().text = timeString;
	}
}