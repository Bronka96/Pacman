using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostVulnerable : MonoBehaviour {
    private float time;
    private float timer;
    [HideInInspector]
    public bool vulnerable;

	// Use this for initialization
	void Start () {
        time = 0;
        timer = 0;
        vulnerable = false;
	}
	
    public void Set(float duration)
    {
        time = 0;
        timer = duration;        
        if(!vulnerable)
        {
            vulnerable = true;
            gameObject.GetComponent<Animator>().SetBool("GhostAlive", false);
        }
        
    }
    
    public void Reset()
    {
        time = 0;
        timer = 0;
        gameObject.GetComponent<Animator>().SetBool("GhostAlive", true);
        vulnerable = false;
    }

	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;

        if (vulnerable && time >= timer)
        {
            Reset();
        }
    }
}
