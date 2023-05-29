using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    private CircleCollider2D detectionCollider;
    public bool detectedPlayer;

    // Start is called before the first frame update
    //void Start()
    //{
    //    detectionCollider = GetComponent<CircleCollider2D>();
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider && collision.collider.CompareTag("Player"))
        {
            detectedPlayer = true;
            Debug.Log("WTF");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider && collision.collider.CompareTag("Player"))
        {
            detectedPlayer = false;
        }
    }
}
