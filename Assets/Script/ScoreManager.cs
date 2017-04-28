using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public Text scoreText;
    public Text highscoreText;

    public float scoreCount;
    public float highscoreCount;

    public float pointsPerSecond;

    public bool scoreIncreasing;

	// Use this for initialization
	void Start () {
        if (PlayerPrefs.HasKey("Highscore"))
        {
            highscoreCount = PlayerPrefs.GetFloat("Highscore");
        }
        
        scoreIncreasing = true;
	}
	
	// Update is called once per frame
	void Update () {

        if (scoreIncreasing)
        {
            scoreCount += pointsPerSecond * Time.deltaTime;
        }
        if (scoreCount > highscoreCount)
        {
            highscoreCount = scoreCount;
            PlayerPrefs.SetFloat("Highscore", highscoreCount);
        }
        scoreText.text = "SCORE: " + Mathf.Round(scoreCount);
        highscoreText.text = "HIGHSCORE: " + Mathf.Round(highscoreCount);
	}

    public void AddScore(int pointsToAdd)
    {
        scoreCount += pointsToAdd;
    }
}
