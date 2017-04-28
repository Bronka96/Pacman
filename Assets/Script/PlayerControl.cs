using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControl : MonoBehaviour {

    public float moveSpeed;
    public float speedMultiplier;
    public float speedIncreaseMilestone;
    private float speedMilestoneCount;
    private float moveSpeedDefault;
    private float speedMilestoneCountDefault;
    private float speedIncreaseMilestoneDefault;
    public float jumpForce;
    public float jumpTime;
    private float jumpTimeCounter;


    public bool grounded;
    public LayerMask ground;
    public Transform groundDetector;
    public float groundDetectorRadius;

    private bool stoppedJumping;
    private bool canDoubleJump;

    public GameManager gameManager;

    private Rigidbody2D rigidBody;

    public AudioSource startGameSound;
    public AudioSource chompingSound;
    public AudioSource deathSound;
    public AudioSource cherrySound;
    public AudioSource ghostSound;

    private ScoreManager scoreManager;
    public ScoreText scoreText;

    private bool canEatGhosts;

	// Use this for initialization
	void Start () {
        startGameSound.Play();
        rigidBody = GetComponent<Rigidbody2D>();
        jumpTimeCounter = jumpTime;
        speedMilestoneCount = speedIncreaseMilestone;
        moveSpeedDefault = moveSpeed;
        speedMilestoneCountDefault = speedMilestoneCount;
        speedIncreaseMilestoneDefault = speedIncreaseMilestone;
        stoppedJumping = true;
        canDoubleJump = true;
        chompingSound.PlayDelayed(4);
        canEatGhosts = false;
        scoreManager = FindObjectOfType<ScoreManager>();
        scoreText = FindObjectOfType<ScoreText>();
    }
	
	// Update is called once per frame
	void Update () {
        grounded = Physics2D.OverlapCircle(groundDetector.position, groundDetectorRadius, ground);

        //Speed up
        if(transform.position.x  > speedMilestoneCount)
        {
            speedMilestoneCount += speedIncreaseMilestone;
            speedIncreaseMilestone *= speedMultiplier;
            moveSpeed *= speedMultiplier;
        }
        rigidBody.velocity = new Vector2(moveSpeed, rigidBody.velocity.y);
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                if (grounded)
                {
                    rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
                    stoppedJumping = false;
                }
                else if (!grounded && canDoubleJump)
                {
                    rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
                    jumpTimeCounter = jumpTime;
                    canDoubleJump = false;
                    stoppedJumping = false;
                }

            }
            if ((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && jumpTimeCounter > 0 && !stoppedJumping)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
            if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
            {
                jumpTimeCounter = 0;
                stoppedJumping = true;
            }
            if (grounded)
            {
                jumpTimeCounter = jumpTime;
                canDoubleJump = true;
            }
        }
       
	}

    IEnumerator GhostDeath()
    {
        yield return new WaitForSeconds(2);
        gameManager.RestartGame();
        moveSpeed = moveSpeedDefault;
        rigidBody.simulated = true;
        speedMilestoneCount = speedMilestoneCountDefault;
        speedIncreaseMilestone = speedIncreaseMilestoneDefault;
        gameObject.GetComponent<Animator>().SetTrigger("PacmanAlive");
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "killbox")
        {
            chompingSound.Stop();
            deathSound.Play();
            gameManager.RestartGame();
            moveSpeed = moveSpeedDefault;
            speedMilestoneCount = speedMilestoneCountDefault;
            speedIncreaseMilestone = speedIncreaseMilestoneDefault;
            
        }
        if(other.gameObject.layer == LayerMask.NameToLayer("Ghost"))
        {
            if (other.gameObject.GetComponent<GhostVulnerable>().vulnerable)
            {                
                scoreManager.AddScore(100);                
                other.gameObject.SetActive(false);
                scoreText.DisplayScore(100);
                chompingSound.Stop();
                ghostSound.Play();
                chompingSound.PlayDelayed(0.5f);

            }
            else
            {
                gameObject.GetComponent<Animator>().SetTrigger("PacmanDead");
                other.gameObject.GetComponent<GhostMovement>().moveSpeed = 0;
                rigidBody.simulated = false;
                chompingSound.Stop();
                deathSound.Play();
                StartCoroutine(GhostDeath());
            }
        }
    }


}
