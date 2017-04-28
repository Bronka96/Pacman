using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCherry : MonoBehaviour {

    public int scoreToAdd;

    private ScoreManager scoreManager;
    private PlayerControl player;
    private List<GameObject> ghosts;
    private GhostGenerator ghostGenerator;
    public float duration;
    private ScoreText scoreText;

	// Use this for initialization
	void Start () {
        scoreManager = FindObjectOfType<ScoreManager>();
        player = FindObjectOfType<PlayerControl>();
        ghostGenerator = FindObjectOfType<GhostGenerator>();
        scoreText = FindObjectOfType<ScoreText>();
	}

    void powerUp()
    {
        ghosts = ghostGenerator.ghostList;
        for(int i = 0; i<ghosts.Count; i++)
        {
            ghosts[i].GetComponent<GhostVulnerable>().Set(duration);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            player.chompingSound.Stop();
            player.cherrySound.Play();
            player.chompingSound.PlayDelayed(0.5f);
            scoreManager.AddScore(scoreToAdd);
            //scoreText.text = "+"+scoreToAdd;
            //scoreText.color = Color.white;
            //scoreText.CrossFadeAlpha(0f, .5f, false);
            scoreText.DisplayScore(scoreToAdd);
            powerUp();
            gameObject.SetActive(false);
            
        }
    }
}
