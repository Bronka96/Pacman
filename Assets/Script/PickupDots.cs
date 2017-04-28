using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupDots : MonoBehaviour {

    public int scoreToAdd;
    private ScoreManager scoreManager;

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            scoreManager.AddScore(scoreToAdd);
            gameObject.SetActive(false);
        }
    }
}
