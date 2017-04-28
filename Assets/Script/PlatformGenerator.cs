using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour {

    public Transform generationPoint;
    public float distanceBetween;
    public float distanceBetweenMin;
    public float distanceBetweenMax;
    public ObjectPool[] objectPools;
    private float platformWidth;
    private float[] platformWidths;
    private int platformSelector;
    private float minHeight;
    public Transform maxHeightPoint;
    private float maxHeight;
    public float maxHeightChange;
    private float heightChange;

    private DotGenerator dotGenerator;
    public float randomDotTreshold;

    private GhostGenerator ghostGenerator;
    public float randomGhostTreshold;

    private BonusGenerator bonusGenerator;
    public float randomBonusTreshold;


    // Use this for initialization
    void Start () {
        platformWidths = new float[objectPools.Length];
        for(int i = 0; i< objectPools.Length; i++)
        {
            Transform[] children = new Transform[2] { objectPools[i].pooledObject.transform.FindChild("PlatformLeft"), objectPools[i].pooledObject.transform.FindChild("PlatformRight") };
            platformWidths[i] = children[0].GetComponent<MeshRenderer>().bounds.size.x + children[1].GetComponent<MeshRenderer>().bounds.size.x;

        }

        minHeight = transform.position.y;
        maxHeight = maxHeightPoint.position.y;

        dotGenerator = FindObjectOfType<DotGenerator>();
        ghostGenerator = FindObjectOfType<GhostGenerator>();
        bonusGenerator = FindObjectOfType<BonusGenerator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.x < generationPoint.position.x)
        {
            platformSelector = Random.Range(0, objectPools.Length);
            distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);
            heightChange = transform.position.y + Random.Range(maxHeightChange, -maxHeightChange);
            if(heightChange > maxHeight)
            {
                heightChange = maxHeight;
            }
            else if(heightChange < minHeight)
            {
                heightChange = minHeight;
            }
            
            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector]/2) + distanceBetween, heightChange, transform.position.z);
            GameObject newPlatform = objectPools[platformSelector].GetPooledObject();
      
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.transform.position = transform.position;
            MeshRenderer left = newPlatform.transform.FindChild("PlatformLeft").GetComponent<MeshRenderer>();
            MeshRenderer right = newPlatform.transform.FindChild("PlatformRight").GetComponent<MeshRenderer>();
            Bounds platformBounds = new Bounds(transform.position, new Vector3(left.bounds.size.x + right.bounds.size.x + 2, 9.0f, 1.0f));
            
            bool canSpawn = true;
            for (int i = 0; i < objectPools.Length; i++)
            {
                for (int j = 0; j < objectPools[i].pooledObjects.Count; j++)
                {
                    if (newPlatform != objectPools[i].pooledObjects[j])
                    {
                        MeshRenderer otherLeft = objectPools[i].pooledObjects[j].transform.FindChild("PlatformLeft").GetComponent<MeshRenderer>();
                        MeshRenderer otherRight = objectPools[i].pooledObjects[j].transform.FindChild("PlatformRight").GetComponent<MeshRenderer>();
                       
                        if (platformBounds.Intersects(otherLeft.bounds) || platformBounds.Intersects(otherRight.bounds) ) 
                        {
                            if (otherLeft.gameObject.activeInHierarchy)
                            {
                                canSpawn = false;
                            }
                        }              
                    }
                }
            }
           
            if(canSpawn)
            {
                newPlatform.SetActive(true);
                if (Random.Range(0f, 100f) < randomDotTreshold)
                {
                    dotGenerator.CreateDot(new Vector3(transform.position.x - (platformWidths[platformSelector] / 2) + 3, transform.position.y + 2.5f, transform.position.z), (int)platformWidths[platformSelector] / 4);
                }
                else
                {
                    if(Random.Range(0f, 100f) < randomGhostTreshold)
                    {
                        Vector2 start = new Vector2(transform.position.x - (platformWidths[platformSelector] / 2)+0.8f, transform.position.y + 3);
                        Vector2 rightPos = new Vector2(transform.position.x + (platformWidths[platformSelector] / 2)-0.8f, transform.position.y + 3);
                        ghostGenerator.SpawnGhost(start, start, rightPos);
                    }
                    else if(Random.Range(0f, 100f) < randomBonusTreshold)
                    {
                        GameObject bonus = Instantiate(bonusGenerator.GetBonus());
                        bonus.transform.position = new Vector3(transform.position.x -1, transform.position.y + 2, transform.position.z);
                        bonus.transform.rotation = transform.rotation;
                        bonus.SetActive(true);
                    }
                }
                transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2), transform.position.y, transform.position.z);
            }
        }
    }
}
