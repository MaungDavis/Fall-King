using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LevelSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject previousLevel;
    [SerializeField] private GameObject nextLevel;

    private CinemachineVirtualCamera virtualCamera;

    // Start is called before the first frame update
    void Start()
    {   
        this.virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Vector2 playerPosition = other.gameObject.transform.position;
            Vector2 boundaryPosition = this.gameObject.transform.position;
            
            // Move camera to next level
            if (playerPosition.y >= boundaryPosition.y)
            {
                virtualCamera.Follow = nextLevel.transform;
            }
            // Move camera to previous level
            else
            {
                virtualCamera.Follow = previousLevel.transform;
            }
        }
    }
}
