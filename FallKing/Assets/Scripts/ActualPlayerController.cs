using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using Cinemachine;

public class ActualPlayerController : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody2D rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float currentSpeed = 0;

        if (Input.GetAxis("Horizontal") > 0)
        {
            currentSpeed = this.speed;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            currentSpeed = -this.speed;
        }

        rigidBody.velocity = new Vector2(currentSpeed, rigidBody.velocity.y);
    }
}