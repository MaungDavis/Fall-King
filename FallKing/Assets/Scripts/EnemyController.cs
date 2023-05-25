using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpInterval;

    private Rigidbody2D rigidBody;
    private float jumpTimer = 0;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (jumpTimer > jumpInterval)
        {
            Vector2 currentPos = new Vector2(transform.position.x, transform.position.y);
            Vector2 targetPos = new Vector2(player.transform.position.x, player.transform.position.y);

            var hit = Physics2D.Linecast(currentPos, targetPos);

            if (hit && hit.collider.gameObject.CompareTag("Player"))
            {
                rigidBody.AddForce((targetPos - currentPos) * jumpForce);


                Debug.Log($"The thing that was hit {hit.rigidbody.name}");
                Debug.DrawLine(transform.position, hit.rigidbody.position);
            }

            jumpTimer = 0;
        }
        jumpTimer += Time.deltaTime;
    }
}
