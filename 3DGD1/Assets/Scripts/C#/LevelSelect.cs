using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public Levels[] levels;
    public int index;
    public GameObject startButton;
    CameraAnimation cameraAnimation;
    public Text timeTaken;
    public Text stepsTaken;
    public AudioSource SFX;
    public AudioClip sfxSelect;
    private void Start()
    {
        cameraAnimation = FindObjectOfType<CameraAnimation>();
    }
    public void NextLevelRight()
    {
        SFX.PlayOneShot(sfxSelect);
        index++;
        if (index > levels.Length-1)
        {
            index--;
            float time;
            int steps;
            time = PlayerPrefs.GetFloat(levels[index].levelToLoad + "Time Taken");
            steps = PlayerPrefs.GetInt(levels[index].levelToLoad + "Steps Taken");

            int minutes;
            int seconds;
            string timeString;

            minutes = (int)time / 60; // Derive minutes by dividing seconds by 60 seconds
            seconds = (int)time % 60; // Derive remainder after dividing by 60 seconds
            timeString = "Time Taken : " + minutes.ToString() + ":" + seconds.ToString("d2");
            timeTaken.text = timeString;

            stepsTaken.text = "Steps Taken : " + steps;
        }
        else
        {
            float time;
            int steps;
            time = PlayerPrefs.GetFloat(levels[index].levelToLoad + "Time Taken");
            steps = PlayerPrefs.GetInt(levels[index].levelToLoad + "Steps Taken");

            int minutes;
            int seconds;
            string timeString;

            minutes = (int)time / 60; // Derive minutes by dividing seconds by 60 seconds
            seconds = (int)time % 60; // Derive remainder after dividing by 60 seconds
            timeString = "Time Taken : " + minutes.ToString() + ":" + seconds.ToString("d2");
            timeTaken.text = timeString;

            stepsTaken.text = "Steps Taken : " + steps;
        }
    }

    public void NextLevelLeft()
    {
        SFX.PlayOneShot(sfxSelect);
        index--;
        if (index < 0)
        {
            index = 0;
            float time;
            int steps;
            time = PlayerPrefs.GetFloat(levels[index].levelToLoad + "Time Taken");
            steps = PlayerPrefs.GetInt(levels[index].levelToLoad + "Steps Taken");

            int minutes;
            int seconds;
            string timeString;

            minutes = (int)time / 60; // Derive minutes by dividing seconds by 60 seconds
            seconds = (int)time % 60; // Derive remainder after dividing by 60 seconds
            timeString = "Time Taken : " + minutes.ToString() + ":" + seconds.ToString("d2");
            timeTaken.text = timeString;

            stepsTaken.text = "Steps Taken : " + steps;

        }
        else
        { 
            float time;
            int steps;
            time = PlayerPrefs.GetFloat(levels[index].levelToLoad + "Time Taken");
            steps = PlayerPrefs.GetInt(levels[index].levelToLoad + "Steps Taken");

            int minutes;
            int seconds;
            string timeString;

            minutes = (int)time / 60; // Derive minutes by dividing seconds by 60 seconds
            seconds = (int)time % 60; // Derive remainder after dividing by 60 seconds
            timeString = "Time Taken : " + minutes.ToString() + ":" + seconds.ToString("d2");
            timeTaken.text = timeString;

            stepsTaken.text = "Steps Taken : " + steps;
        }
    }

    public void StartLevel()
    {
        SFX.PlayOneShot(sfxSelect);
        SceneManager.LoadScene(levels[index].levelToLoad);
    }
}
