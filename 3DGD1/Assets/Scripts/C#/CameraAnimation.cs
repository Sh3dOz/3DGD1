using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraAnimation : MonoBehaviour
{
    public bool isMoving;
    Animator myAnim;
    LevelSelect levelSelect;
    public Text timeTaken;
    public Text stepsTaken;
    // Start is called before the first frame update
    void Start()
    {
        levelSelect = FindObjectOfType<LevelSelect>();
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        myAnim.SetInteger("Index", levelSelect.index);
        myAnim.SetBool("IsMoving", isMoving);
    }

    public void NotMoving()
    {
        isMoving = false;
    }

    public void EnableStart()
    {
        if (levelSelect.index > 0)
        {
            if (PlayerPrefs.GetFloat(levelSelect.levels[levelSelect.index].levelToLoad) != 1)
            {
                levelSelect.startButton.SetActive(false);
            }
            else
            {
                levelSelect.startButton.SetActive(true);
            }
        }
    }

    public void UpdateScore()
    {
        if (PlayerPrefs.GetFloat(levelSelect.levels[levelSelect.index].levelToLoad) != 1)
        {
            timeTaken.text = "Time Taken : -";
            stepsTaken.text = "Steps Taken : -";
        }
        else
        {
            float time;
            int steps;
            time = PlayerPrefs.GetFloat(levelSelect.levels[levelSelect.index].levelToLoad + "Time Taken");
            steps = PlayerPrefs.GetInt(levelSelect.levels[levelSelect.index].levelToLoad + "Steps Taken");

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
}
