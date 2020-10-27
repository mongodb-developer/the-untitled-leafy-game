using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    private Text scoreText;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset() {
        score = 0;
        scoreText.text = "SCORE: " + score;
    }

    public void AddPoints(int amount) {
        score += amount;
        scoreText.text = "SCORE: " + score;
    }

    void PersistScore() {

    }

}
