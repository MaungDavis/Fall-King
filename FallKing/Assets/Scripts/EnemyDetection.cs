using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    [SerializeField] private Transform physicalBodyPos;

    [HideInInspector]
    public bool detectedPlayer = false;

    private void Update()
    {
        transform.position = physicalBodyPos.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        detectedPlayer = true;
        Debug.Log("TRIGGER");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        detectedPlayer = false;
        Debug.Log("EXITTTT");
    }
}
