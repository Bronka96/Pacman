using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Transform platformGenerator;
    private Vector3 platformStartPoint;
    public Transform platformGenerator2;
    private Vector3 platformStartPoint2;
    public PlayerControl player;
    private Vector3 playerStartPoint;
    private PlatformDestroyer[] platformList;
    private ScoreManager scoreManager;
    public DeathMenu deathScreen;
    public PauseMenu pauseMenu;
    public AudioSource startGameSound;
    private GhostGenerator ghostGenerator;
   

	// Use this for initialization
	void Start () {
        platformStartPoint = platformGenerator.position;
        platformStartPoint2 = platformGenerator2.position;
        playerStartPoint = player.transform.position;
        scoreManager = FindObjectOfType<ScoreManager>();
        ghostGenerator = FindObjectOfType<GhostGenerator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Escape) && player.gameObject.activeInHierarchy == true)
        {
            pauseMenu.Pause();
        }
	} 

    public void RestartGame()
    {
        player.gameObject.SetActive(false);
        scoreManager.scoreIncreasing = false;
        deathScreen.gameObject.SetActive(true);
        //StartCoroutine("RestartGameCo");
    }

    public void Reset()
    {
        startGameSound.Play();
        deathScreen.gameObject.SetActive(false);
        for (int i = 0; i < ghostGenerator.ghostList.Count; i++)
        {
            ghostGenerator.ghostList[i].GetComponent<GhostVulnerable>().Reset();
        }
        platformList = FindObjectsOfType<PlatformDestroyer>();
        for (int i = 0; i < platformList.Length; i++)
        {
            platformList[i].gameObject.SetActive(false);
        }
        platformGenerator.transform.position = platformStartPoint;
        platformGenerator2.transform.position = platformStartPoint2;
        player.transform.position = playerStartPoint;

        player.gameObject.SetActive(true);

        scoreManager.scoreCount = 0;
        scoreManager.scoreIncreasing = true;
        player.chompingSound.PlayDelayed(4);
    }  

    //public IEnumerator RestartGameCo()
    //{
    //    player.gameObject.SetActive(false);
    //    scoreManager.scoreIncreasing = false;
    //    yield return new WaitForSeconds(0.5f);
    //    platformList = FindObjectsOfType<PlatformDestroyer>();
    //    for(int i = 0; i < platformList.Length; i++)
    //    {
    //        platformList[i].gameObject.SetActive(false);
    //    }
    //    Debug.Log("before" + platformGenerator.transform.position.x);
    //    platformGenerator.transform.position = platformStartPoint;
    //    Debug.Log("after" + platformGenerator.transform.position.x);
    //    platformGenerator2.transform.position = platformStartPoint2;
    //    player.transform.position = playerStartPoint;
        
    //    player.gameObject.SetActive(true);

    //    scoreManager.scoreCount = 0;
    //    scoreManager.scoreIncreasing = true;
    //}
}
