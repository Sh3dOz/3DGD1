using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="LevelSelect/Level")]
public class Levels : ScriptableObject
{
    public string levelToLoad;
    public Sprite levelThumbnail;
}
