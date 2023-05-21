using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //[SerializeField] private float speed = 0;
    [SerializeField] private float glidingAcceleration = 15f;
    [SerializeField] private float hoverForce = 0.8f;
    [SerializeField] private float gravityForce = 0.2f;
    [SerializeField] private float maxMoveMagnitude = 20f;

    private Rigidbody2D rigidBody;
    private float playerInputX, playerInputY;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.gravityScale = gravityForce;
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        playerInputX = movementVector.x;
        playerInputY = movementVector.y;
    }


    // Update is called once per frame
    /// <summary>
    /// Up = only lower the gravity force
    /// </summary>
    void FixedUpdate()
    {   //Check gravity interaction with vector when let go of key
        float movementX = playerInputX;
        if (playerInputY > 0)   //If the key up is pressed
        {
            rigidBody.gravityScale = 0f;
            var moveWithUp = new Vector2(movementX, 0);
            rigidBody.AddForce(moveWithUp * glidingAcceleration);
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, -hoverForce);  //! Force the player to have only one speed
        }
        else
        {
            var movementY = playerInputY;
            rigidBody.gravityScale = gravityForce;
            var move = new Vector2 (movementX, movementY);
            rigidBody.AddForce (move * glidingAcceleration);
            Debug.Log($"The new velocity {move}");
        }
        rigidBody.velocity = Vector2.ClampMagnitude(rigidBody.velocity, maxMoveMagnitude);  //Clamp the speed so player would not move too fast
        Debug.Log($"current velocity: {rigidBody.velocity.y} and horizontal: {rigidBody.velocity.x}");
    }
}
