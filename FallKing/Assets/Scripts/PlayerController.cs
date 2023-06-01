using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

//Todo: When player release the key, force the user to stop (currently using add force make the user keep sliding)
//TODO: The user will keep a minimal amount of momentum when stop?
    //! Add a huge force to the opposite moving direction?
public class PlayerController : MonoBehaviour
{
    [Header("Horizontal Physics")]
    [SerializeField] private float glidingAcceleration = 15f;
    [SerializeField] private float maxMoveMagnitude = 20f;
    [Tooltip("Time from when release key for player to complete stop")]
    [SerializeField] private float timeToStop = 1f;

    [Header("Vertical Physics")]
    [SerializeField] private float hoverForce = 0.8f;
    [SerializeField] private float maxFallMagnitude = 20f;

    private Rigidbody2D rigidBody;
    private float initialGravity;
    private float playerInputX, playerInputY;
    bool playerRelasedKey = true;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Collide with left or right boundary
        if (collision.gameObject.tag == "Boundary")
        {
            playerInputX = 0;
        }
    }

    void FixedUpdate()
    {   ////TODO: When hover key pressed, fan physics doesn't work due to manually changing velocity in controll
        ////TODO: Stop using Add force to control player hovering speed? Instead change the gravity? Clamp the velocity as needed
        ////? Currently if the player is shoot up by fan while holding hover, the lesser gravity will allow to shoot up even further

        // Player release moving key, add a force to the opposite moving direction
        if (playerInputX == 0)  //TODO: To use impulse, make sure to only execute this block once per player key released. Or else it will apply over and over the whole time the key is not pressed
        {
            //! Reverse impulse? Wrong physics tho
            var reverseImpulse = (rigidBody.mass * rigidBody.velocity.magnitude) / timeToStop;
            Debug.Log($"The impulse stop force {reverseImpulse}");
            rigidBody.AddForce(new Vector2(reverseImpulse * (-rigidBody.velocity.normalized.x), 0) * glidingAcceleration, ForceMode2D.Impulse);
        }

        if (playerInputY > 0)   //If the key up is pressed and the player is falling
        {
            if (rigidBody.velocity.y < 0)
            {
                rigidBody.gravityScale = initialGravity - hoverForce;
                //var moveWithUp = new Vector2(movementX, 0); //? Seperate add force horizontal and vertical later
                //rigidBody.AddForce(moveWithUp * glidingAcceleration);
                //rigidBody.velocity = new Vector2(rigidBody.velocity.x, -hoverForce);  //! Force the player to have only one speed
            }
        }
        else
        {   ////! Allow player to hold down to go down faster -> Will be a problem since the downspeed is clamped below
            var movementY = playerInputY;
            rigidBody.gravityScale = initialGravity;
            var moveUp = new Vector2(0, movementY);
            rigidBody.AddForce(moveUp);
            //Debug.Log($"The new velocity {move}");
        }

        //! Now add horizontal force
        float movementX = playerInputX;
        rigidBody.AddForce(new Vector2(movementX, 0) * glidingAcceleration);

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
