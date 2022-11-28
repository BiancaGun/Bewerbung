using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurtle : MonoBehaviour
{
    PlayerStatus playerStatus;
    PlayerLife playerLife;
    PlayerMovement playerMovement;
    EnemyTurtleMovement enemyTurtleMovement;
    SpriteRenderer spriteRenderer;
    [SerializeField] bool TurtleCanDoDMG;

    // Start is called before the first frame update
    void Start()
    {
        playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        playerLife = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLife>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        enemyTurtleMovement = GetComponent<EnemyTurtleMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CheckTurtleCanDoDMG()
    {
        if (enemyTurtleMovement.TurtleCanMove || enemyTurtleMovement.ShellCanMove)
        {
            TurtleCanDoDMG = true;
        }
        else
        {
            TurtleCanDoDMG = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckTurtleCanDoDMG();

        if (collision.gameObject.tag == "Player" && playerStatus.PlayerIsFalling == false && playerStatus.CanTakeDMG && TurtleCanDoDMG)
        {
            playerLife.LoseLife();
            enemyTurtleMovement.TurtleFlip();
        }
        else if (collision.gameObject.tag == "Player" && playerStatus.PlayerIsFalling && playerStatus.CanTakeDMG)
        {
            if (enemyTurtleMovement.TurtleIsShell == false)
            {
                spriteRenderer.flipY = true;
                StartCoroutine(WaitForTurtleIsShell());
                enemyTurtleMovement.TurtleCanMove = false;
                playerMovement.EnemyPush();
            }
            else if (enemyTurtleMovement.TurtleIsShell && enemyTurtleMovement.ShellCanMove)
            {
                playerMovement.EnemyPush();
                Destroy(this.gameObject);
            }
        }
        else if (collision.gameObject.tag == "Player" && playerStatus.InstantKill)
        {
            Destroy(this.gameObject);
        }
        
        if (collision.gameObject.tag == "Enemy" && enemyTurtleMovement.TurtleIsShell && TurtleCanDoDMG)
        {
            Destroy(collision.gameObject);
        }
    }

    IEnumerator WaitForTurtleIsShell()
    {
        yield return new WaitForSeconds(0.1f);
        enemyTurtleMovement.TurtleIsShell = true;
    }
}
