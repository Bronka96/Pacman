using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreText : MonoBehaviour {

    private TextMesh text;
    private PlayerControl player;
    private float playerPositionY;
    public float fadeTime;
    private float timer;
    private float currentAlpha;
    private bool fading;

	// Use this for initialization
	void Start () {
        text = gameObject.GetComponent<TextMesh>();
        player = FindObjectOfType<PlayerControl>();
        fading = false;
        timer = 0;
        currentAlpha = 1f;
        
	}
	
    void Update()
    {
        if (fading)
        {
            if (timer < fadeTime)
            {
                timer += Time.deltaTime;
                currentAlpha = 1-(timer / fadeTime);
                text.color = new Color(1, 1, 1, currentAlpha);
            }
            else
            {
                fading = false;
            }
        }


    }
	public void DisplayScore(int scoreToDisplay)
    {
        fading = true;
        text.text = "+" + scoreToDisplay;
        timer = 0;
        text.color = new Color(1, 1, 1, 1);
        // text.GetComponent<CanvasRenderer>().SetAlpha(1f);
    }
}
