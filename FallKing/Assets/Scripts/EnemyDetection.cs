using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    [HideInInspector]
    public bool detectedPlayer = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger here");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("ENTERING detection");
    }


    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log("ENTERING collision");
    //    if (collision.)
    //    if (collision.collider && collision.collider.CompareTag("Player"))
    //    {
    //        Debug.Log("WTF this thing doesn't hit");
    //        detectedPlayer = true;
    //    }
    //}

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("EXITING Collision");
        if (collision.collider && collision.collider.CompareTag("Player"))
        {
            detectedPlayer = false;
        }
    }
}
