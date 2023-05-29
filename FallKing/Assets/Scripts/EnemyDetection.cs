using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public bool detectedPlayer = false;

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
