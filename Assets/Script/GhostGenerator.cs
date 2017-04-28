using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostGenerator : MonoBehaviour {

    public List<GameObject> ghostList;

    void Start()
    {
        for (int i = 0; i < ghostList.Count; i++)
        {
            ghostList[i] = Instantiate(ghostList[i]);
            ghostList[i].SetActive(false);            
        }
    }

    public GameObject GetGhost()
    {
        for (int i = 0; i < ghostList.Count; i++)
        {
            if (!ghostList[i].activeInHierarchy)
                return ghostList[i];
        }
        GameObject ghost = (GameObject)Instantiate(ghostList[Random.Range(0, ghostList.Count)]);
        ghost.SetActive(false);
        ghostList.Add(ghost);
        return ghost;
    }

    public void SpawnGhost(Vector2 start, Vector2 left, Vector2 right)
    {
        GameObject ghost = GetGhost();
        ghost.transform.position = start;
        ghost.GetComponent<GhostMovement>().Spawn(left, right);
    }
}
