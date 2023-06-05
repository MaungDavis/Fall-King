using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Respawn : MonoBehaviour
{
    [SerializeField] private Transform respawnLevel;

    private Transform respawnPoint;
    private CinemachineVirtualCamera virtualCamera;
    private Rigidbody2D rigidBody;

    void Start()
    {
        respawnPoint = respawnLevel.Find("StageRespawnPoint");
        this.virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        this.rigidBody = GetComponent<Rigidbody2D>();
    }
    public void setRespawnPoint(Transform newRespawn)
    {
        this.respawnPoint = newRespawn;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Ground")
        {
            return;
        }

        // Handle collision with ground
        virtualCamera.Follow = this.respawnLevel;
        this.transform.position = new Vector2(this.respawnPoint.position.x, this.respawnPoint.position.y); 
        this.rigidBody.velocity = new Vector2(0, 0);
    }
}
