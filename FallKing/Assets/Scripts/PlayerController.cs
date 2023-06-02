using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using Cinemachine;

//Todo: When player release the key, force the user to stop (currently using add force make the user keep sliding)
//TODO: The user will keep a minimal amount of momentum when stop?
    //! Add a huge force to the opposite moving direction?
public class PlayerController : MonoBehaviour
{
    [Header("Horizontal Physics")]
    [SerializeField] private float glidingAcceleration = 15f;
    [Tooltip("The player max left and right velocity")]
    [SerializeField] private float maxMoveMagnitude = 20f;
    [Tooltip("Time from when release key until the player come to complete stop")]
    [SerializeField] private float timeToStop = 1f;

    [Header("Vertical Physics")]
    [Tooltip("The player up force, need to be lower than gravity scale to make the player still falling")]
    [SerializeField] private float hoverForce = 0.8f;
    [Tooltip("The max falling speed the player can have through gravity (doesn't account for holding down key)")]
    [SerializeField] private float maxFallMagnitude = 20f;
    [SerializeField] private Transform respawnLevel;
    
    private Transform respawnPoint;
    private CinemachineVirtualCamera virtualCamera;
    private Rigidbody2D rigidBody;
    private float initialGravity;
    private float playerInputX, playerInputY;
    bool playerReleasedKey = true;

    void Start()
    {
        respawnPoint = respawnLevel.Find("StageRespawnPoint");
        rigidBody = GetComponent<Rigidbody2D>();
        initialGravity = rigidBody.gravityScale;
        //Debug.LogError($"The gravity force {initialGravity} and the hoverForce {hoverForce}");
        Assert.IsTrue(hoverForce > 0);  //Cannot have negative hover force for later calculation nor too big either
        Assert.IsTrue(hoverForce < initialGravity);
        this.virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        playerInputX = movementVector.x;
        playerInputY = movementVector.y;
        if (playerInputX == 0)
        {
            playerReleasedKey = true;
        }
    }

    public void setRespawnPoint(Transform newRespawn)
    {
        this.respawnPoint = newRespawn;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Collide with left or right boundary
        if (collision.gameObject.tag == "Boundary")
        {
            playerInputX = 0;
        }

        // Collide with any tile collision box
        if (collision.gameObject.tag == "Ground")
        {
            //virtualCamera.Follow = this.respawnLevel;
            this.transform.position = new Vector2(this.respawnPoint.position.x, this.respawnPoint.position.y);
        }
    }

    void FixedUpdate()
    {   ////TODO: When hover key pressed, fan physics doesn't work due to manually changing velocity in controll
        ////TODO: Stop using Add force to control player hovering speed? Instead change the gravity? Clamp the velocity as needed
        ////? Currently if the player is shoot up by fan while holding hover, the lesser gravity will allow to shoot up even further

        // Player release moving key, add a force to the opposite moving direction
        if (playerReleasedKey)  ////TODO: To use impulse, make sure to only execute this block once per player key released. Or else it will apply over and over the whole time the key is not pressed
        {
            //! Reverse impulse
            var reverseImpulse = -(rigidBody.mass * rigidBody.velocity.x) / timeToStop;     // negative to represent opposite force
            //Debug.Log($"The impulse stop force {reverseImpulse}");
            //Debug.LogError("Only print once");
            //Debug.Log($"The current normalized velocity {rigidBody.velocity.normalized.x}");
            rigidBody.AddForce(new Vector2(reverseImpulse * Mathf.Abs(rigidBody.velocity.normalized.x), 0), ForceMode2D.Impulse);
            //Debug.Log($"The force being add {new Vector2(reverseImpulse * Mathf.Abs(rigidBody.velocity.normalized.x), 0)}");
            playerReleasedKey = false;
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
