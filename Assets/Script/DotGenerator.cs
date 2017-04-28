using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotGenerator : MonoBehaviour {

    public ObjectPool dotPool;

    public float distanceBetweenDots;

    public void CreateDot(Vector3 startPosition, int numberOfDots)
    {
        for(int i = 1; i<=numberOfDots; i++)
        {
            GameObject dot = dotPool.GetPooledObject();
            dot.transform.position = new Vector3(startPosition.x+((i-1)*distanceBetweenDots), startPosition.y, startPosition.z);
            if(!Physics.CheckSphere(dot.transform.position, 2))
            { 
                dot.SetActive(true);
            }
            
        }
    }
	
}
