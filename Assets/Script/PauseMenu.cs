using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {


    public void Pause()
    {
        FindObjectOfType<PlayerControl>().startGameSound.Stop();
        FindObjectOfType<PlayerControl>().chompingSound.Stop();
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {        
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        FindObjectOfType<PlayerControl>().chompingSound.Play();
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;        
        SceneManager.LoadScene("MainMenu");
        gameObject.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        FindObjectOfType<GameManager>().Reset();        
    }
}
