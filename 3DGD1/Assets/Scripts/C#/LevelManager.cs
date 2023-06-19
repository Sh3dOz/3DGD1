using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    public int steps;
    public Image star1, star2, star3;
    public Animator anim1, anim2, anim3;
    public GameObject highTime;
    public GameObject highSteps;
    float pastTime;
    int pastSteps;
    public GameObject scoreCanvas;
    public Text timeTaken;
    public Text stepsTaken;
    public bool gameEnd;
    public IEnumerator Score()
    {
        gameEnd = true;
        scoreCanvas.SetActive(true);
        pastTime = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().ToString() + "Time Taken");
        pastSteps = PlayerPrefs.GetInt(SceneManager.GetActiveScene().ToString() + "Steps Taken");
        anim1.SetTrigger("Start");
        yield return new WaitForSeconds(0.3f);
        anim2.SetTrigger("Start");
        yield return new WaitForSeconds(0.3f);
        anim3.SetTrigger("Start");
        yield return new WaitForSeconds(0.3f);
        
        if(CountDownTime.timeTaken > pastTime)
        {
            highTime.SetActive(true);
        }
        if(steps > pastSteps)
        {
            highSteps.SetActive(true);
        }
        int minutes;
        int seconds;
        string timeString;

        minutes = (int)CountDownTime.timeTaken / 60; // Derive minutes by dividing seconds by 60 seconds
        seconds = (int)CountDownTime.timeTaken % 60; // Derive remainder after dividing by 60 seconds
        timeString = "Time Taken > " + minutes.ToString() + ":" + seconds.ToString("d2");
        timeTaken.text = timeString;
        stepsTaken.text = "Steps Taken : " + steps;
    }

    public void Level1()
    {
        SceneManager.LoadScene("Game 1");
    }

    public void Level2()
    {
        SceneManager.LoadScene("Game 2");
    }

    public void BackToMain()
    {
        SceneManager.LoadScene("Level Select");
    }
}
