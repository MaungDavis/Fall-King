using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float glidingAcceleration = 15f;
    [SerializeField] private float hoverForce = 0.8f;
    [SerializeField] private float maxMoveMagnitude = 20f;
    [SerializeField] private float maxFallMagnitude = 20f;
    private Rigidbody2D rigidBody;
    private float initialGravity;
    private float playerInputX, playerInputY;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        initialGravity = rigidBody.gravityScale;
        //Debug.LogError($"The gravity force {initialGravity} and the hoverForce {hoverForce}");
        Assert.IsTrue(hoverForce > 0);  //Cannot have negative hover force for later calculation nor too big either
        Assert.IsTrue(hoverForce < initialGravity);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        playerInputX = movementVector.x;
        playerInputY = movementVector.y;
    }

    //TODO: Make a temporary player jump button to test out the NPC AI idea
    void OnFire(InputValue fireValue)
    {

    }

    // Update is called once per frame
    /// <summary>
    /// Up = only lower the gravity force
    /// Player can only fall up to maximum speed
    /// </summary>
    void FixedUpdate()
    {   ////TODO: When hover key pressed, fan physics doesn't work due to manually changing velocity in controll
            ////TODO: Stop using Add force to control player hovering speed? Instead change the gravity? Clamp the velocity as needed
        ////? Currently if the player is shoot up by fan while holding hover, the lesser gravity will allow to shoot up even further
        float movementX = playerInputX;
        if (playerInputY > 0)   //If the key up is pressed and the player is falling
        {
            if (rigidBody.velocity.y < 0)
            {
                rigidBody.gravityScale = initialGravity - hoverForce;
                var moveWithUp = new Vector2(movementX, 0);
                rigidBody.AddForce(moveWithUp * glidingAcceleration);
                //rigidBody.velocity = new Vector2(rigidBody.velocity.x, -hoverForce);  //! Force the player to have only one speed
            }
        }
        else
        {   ////! Allow player to hold down to go down faster -> Will be a problem since the downspeed is clamped below
            var movementY = playerInputY;
            rigidBody.gravityScale = initialGravity;
            var move = new Vector2 (movementX, movementY);
            rigidBody.AddForce (move * glidingAcceleration);
            //Debug.Log($"The new velocity {move}");
        }

        //Clamp the falling speed AND left/right movement IF falling AND the player not holding down to go down faster
        if (rigidBody.velocity.y < 0 && playerInputY > 0)
        {
            rigidBody.velocity = new Vector2(Mathf.Clamp(rigidBody.velocity.x, -maxMoveMagnitude, maxMoveMagnitude), Mathf.Clamp(rigidBody.velocity.y, -maxFallMagnitude, 0f));
            //Debug.Log("The velocity is clamped");
        }
        else
        {   //Only clamp the left/right
            rigidBody.velocity = Vector2.ClampMagnitude(rigidBody.velocity, maxMoveMagnitude);
        }

        //Debug.Log($"current velocity: {rigidBody.velocity.y} and horizontal: {rigidBody.velocity.x}");
    }
}
