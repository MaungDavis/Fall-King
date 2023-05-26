using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float jumpForce;
    [SerializeField] private float scanInterval;    //Time between scanning for target
    [SerializeField] private float moveInterval;    //Time between each move to target
    [SerializeField] private float timeToLocation;  //Time object should take to move to target location (exclude the interval)
    [SerializeField] private float horizontalMoveForce;   //Move force toward the player horizontally

    private Rigidbody2D rigidBody;
    private RaycastHit2D hit;
    private float horizontalDistToTarget = 0;
    private float scanTimer = 0;
    private float moveTimer = 0;
    private float initialJumpForce;
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        initialJumpForce = jumpForce;
        Assert.AreNotEqual(timeToLocation, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            jumpForce = initialJumpForce;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            jumpForce = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (scanTimer > scanInterval)
        {
            Vector2 currentPos = new Vector2(transform.position.x, transform.position.y);
            Vector2 targetPos = new Vector2(player.transform.position.x, player.transform.position.y);

            hit = Physics2D.Linecast(currentPos, targetPos);
            horizontalDistToTarget = (targetPos - currentPos).normalized.x;
            scanTimer = 0f;

        }
        scanTimer += Time.deltaTime;
    }

    //TODO: Change the physisc, when the target is too close, the force will make the user jump over the target over and over
    //TODO: A better method would be letting the user indicate the time they want to reach target, and from there calculate the force need
    private void FixedUpdate()
    {
        if (moveTimer > moveInterval)
        {
            if (hit.collider && hit.collider.gameObject.CompareTag("Player"))
            {
                Vector2 hopToTarget = new Vector2(horizontalDistToTarget * horizontalMoveForce, jumpForce);
                rigidBody.AddForce(hopToTarget);

                //Debug.Log($"The thing that was hit {hit.rigidbody.name}");
                //Debug.DrawLine(transform.position, hit.rigidbody.position);
            }
            moveTimer = 0f;
        }
        moveTimer += Time.deltaTime;
    }
}
