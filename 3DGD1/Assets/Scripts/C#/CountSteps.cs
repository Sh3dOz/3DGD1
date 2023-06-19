using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountSteps : MonoBehaviour 
{
	LevelManager manager;
    private void Start()
    {
        manager = FindObjectOfType<LevelManager>();
    }
    void Update() {
		GetComponent<Text>().text = "STEPS TAKEN > " + manager.steps.ToString();
	}
}