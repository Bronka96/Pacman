using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour {

    public float defaultMoveSpeed;
    [HideInInspector]
    public float moveSpeed;
    private Vector2 maxRight;
    private Vector2 maxLeft;
    private bool goingRight;
    private Rigidbody2D rigidBody;
    
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        moveSpeed = defaultMoveSpeed;
    }

    public void Spawn(Vector2 left, Vector2 right)
    {
        maxRight = right;
        maxLeft = left;
        goingRight = true;
        gameObject.SetActive(true);
    }
	
	// Update is called once per frame
	void Update()
    {
        if(transform.position.x >= maxRight.x)
        {
            goingRight = false;
        }
        else if(transform.position.x <= maxLeft.x)
        {
            goingRight = true;
        }
		if(goingRight)
        {
            rigidBody.velocity = new Vector2(moveSpeed, rigidBody.velocity.y);
        }
        else
        {
            rigidBody.velocity = new Vector2(-moveSpeed, rigidBody.velocity.y);
        }
	}
    void OnEnable()
    {
        moveSpeed = defaultMoveSpeed;
    }


}
