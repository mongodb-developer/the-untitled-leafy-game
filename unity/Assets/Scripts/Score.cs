using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    private Text scoreText;
    private int score;

    void Start()
    {
        scoreText = GetComponent<Text>();
        this.Reset();
    }

    void Update() { }

    public void Reset() {
        score = GameData.totalScore;
        scoreText.text = "SCORE: " + GameData.totalScore;
    }

    public void AddPoints(int amount) {
        score += amount;
        scoreText.text = "SCORE: " + score;
    }

    public void BankScore() {
        GameData.totalScore += score;
    }

    void PersistScore() {

    }

}
