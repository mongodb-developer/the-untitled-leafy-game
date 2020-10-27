using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameController : MonoBehaviour {

    private static int currentLevelIndex;
    private static string[] levels;

    void Start() {
        levels = new string[] {
            "LevelOne",
            "LevelThree"
        };
        currentLevelIndex = Array.IndexOf(levels, SceneManager.GetActiveScene().name);
    }

    public static void NextLevel() {
        if(currentLevelIndex < levels.Length - 1) {
            SceneManager.LoadScene(levels[currentLevelIndex + 1]);
        }
    }

    public static void GameOver() {

    }

}
