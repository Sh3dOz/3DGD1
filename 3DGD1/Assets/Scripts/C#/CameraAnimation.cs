using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimation : MonoBehaviour
{
    public bool isMoving;
    Animator myAnim;
    LevelSelect levelSelect;
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
            Debug.Log((PlayerPrefs.GetFloat(levelSelect.levels[levelSelect.index - 1].levelToLoad)));
            if (PlayerPrefs.GetFloat(levelSelect.levels[levelSelect.index - 1].levelToLoad) != 1)
            {
                levelSelect.startButton.SetActive(false);
            }
            else
            {
                levelSelect.startButton.SetActive(true);
            }
        }
    }
}
