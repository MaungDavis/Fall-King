using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 0;
    private Rigidbody2D rigidBody;
    private float movementX, movementY;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 movement = new Vector2(movementX, movementY);
        rigidBody.AddForce(movement * speed);
    }
}
