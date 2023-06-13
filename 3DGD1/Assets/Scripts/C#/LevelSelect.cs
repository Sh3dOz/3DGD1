using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public Image levelImage;
    public Levels[] levels;
    public int index;
    public void NextLevelRight()
    {
        index++;
        if(index > levels.Length)
        {
            index--;
        }
        levelImage.overrideSprite = levels[index].levelThumbnail;
    }

    public void NextLevelLeft()
    {
        index--;
        if(index < 0)
        {
            index = 0;
        }
        levelImage.overrideSprite = levels[index].levelThumbnail;
    }

    public void StartLevel()
    {
        if(PlayerPrefs.GetInt(levels[index].levelToLoad) == 1) SceneManager.LoadScene(levels[index].levelToLoad);
    }
}
