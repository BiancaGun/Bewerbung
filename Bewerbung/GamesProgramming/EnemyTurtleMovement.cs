using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurtleMovement : MonoBehaviour
{
    [SerializeField] int moveSpeed;
    [SerializeField] int shellMoveSpeed;
    [SerializeField] int ShellHitSpeed;
    [SerializeField] public bool TurtleCanMove = true;
    [SerializeField] public bool TurtleIsShell = false;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    PlayerStatus playerStatus;
    PlayerMovement playerMovement;
    public float ShellPos;
    public bool ShellCanMove = false;
    float playerCollisionPos;
    float ShellDirection;
    bool turtleMoveLeft;
    bool shellMoveLeft;
    int randoNum;

    // Start is called before the first frame update
    void Start()
    {
        playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        ShellMovement();
    }

    private void Movement()
    {
        if (TurtleCanMove && turtleMoveLeft)
            rb.velocity = new Vector2(moveSpeed * Time.deltaTime, rb.velocity.y);
        else if (TurtleCanMove)
            rb.velocity = new Vector2(-moveSpeed * Time.deltaTime, rb.velocity.y);
    }    
    
    private void ShellMovement()
    {
        if (ShellCanMove && shellMoveLeft)
            rb.velocity = new Vector2(shellMoveSpeed * ShellHitSpeed * Time.deltaTime, rb.velocity.y);
        else if (ShellCanMove)
            rb.velocity = new Vector2(-shellMoveSpeed * ShellHitSpeed * Time.deltaTime, rb.velocity.y);
    }

    public void TurtleFlip()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
        turtleMoveLeft = !turtleMoveLeft;
        shellMoveLeft = !shellMoveLeft;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && TurtleIsShell && ShellCanMove == false)
        {
            ShellPos = transform.position.x;
            playerStatus.CheckPlayerCurrentPos();
            playerCollisionPos = playerStatus.PlayerCurrentPos.x;
            ShellDirection = playerCollisionPos - ShellPos;
            if (ShellDirection < 0)
            {
                rb.AddForce(new Vector2(ShellHitSpeed,0));
            }
            else if (ShellDirection > 0)
            {
                rb.AddForce(new Vector2(-ShellHitSpeed,0));
                shellMoveLeft = false;
            }
            else if (ShellDirection == 0)
            {
                randoNum = Random.Range(1, 11);
                if (randoNum % 2 == 0)
                {
                    shellMoveLeft = false;
                }
            }
            StartCoroutine(WaitForShellCanMove());
            playerMovement.EnemyPush();
        }

        if (collision.gameObject.tag != "Ground")
        {
            TurtleFlip();
        }
    }
    IEnumerator WaitForShellCanMove()
    {
        yield return new WaitForSeconds(0.1f);
        ShellCanMove = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall" && TurtleCanMove)
        {
            TurtleFlip();
        }
    }
}
