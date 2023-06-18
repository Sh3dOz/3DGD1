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
    private void Start()
    {
        cameraAnimation = FindObjectOfType<CameraAnimation>();
    }
    public void NextLevelRight()
    {
        index++;
        if (index > levels.Length)
        {
            index--;

        }
        else
        {
            cameraAnimation.isMoving = true;
        }
    }

    public void NextLevelLeft()
    {
        index--;
        if (index < 0)
        {
            index = 0;

        }
        else
        {
            cameraAnimation.isMoving = true;
        }
    }

    public void StartLevel()
    {
        if (PlayerPrefs.GetInt(levels[index].levelToLoad) == 1) SceneManager.LoadScene(levels[index].levelToLoad);
    }
}
