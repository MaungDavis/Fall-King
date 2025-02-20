using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

//TODO: add a timer between each jump so that it doesn't appear to be too aggressive
//? REMEMBER: Have the layer of this object to be ignore raycast or else the linecast would not work

////TODO: Add jump distance. Currently the user can just bounce straight to the target location without any stops
////TODO: User can slide on the groudn after landing due to momentum. Think about leave it llike that or not.
public class EnemyJumpAI : MonoBehaviour
{
    [Header("Logic")]
    [SerializeField] private GameObject player;
    [SerializeField] private EnemyDetection detectorScript;

    [Header("Pathfinding")]
    [SerializeField] private float scanInterval;    //Time between scanning for target
    [SerializeField] private float moveInterval;    //Time between each move to target

    [Header("Physics")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float timeToLocation;  //Time object should take to move to target location (exclude the interval)
    [SerializeField] private float oneHopDistance;   //The max distance user can travel in one hop
    [SerializeField] LayerMask ignoreLayerLinecast;

    private Rigidbody2D rigidBody;
    private RaycastHit2D hit;
    private float scanTimer = 0;
    private float initialJumpForce;
    private float impulseForce; // Force used for the user hopping - use Force.Impulse

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        Debug.Log($"the componnent name {rigidBody.name}");
        initialJumpForce = jumpForce;
        Assert.AreNotEqual(timeToLocation, 0f);
    }

    //TODO: detect ground is not a good way yet, test it out more
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            Debug.Log("Enter the ground");
            jumpForce = initialJumpForce;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            jumpForce = 0;
        }
    }

    ////TODO: Change the physisc, when the target is too close, the force will make the user jump over the target over and over/
    ////TODO: A better method would be letting the user indicate the time they want to reach target, and from there calculate the force need
    void FixedUpdate()
    {
        if (detectorScript == null)
        {
            return;
        }
        bool detection = detectorScript.detectedPlayer;

        scanTimer += Time.deltaTime;
        if (scanTimer > scanInterval && detection)
        {
            Vector2 currentPos = new Vector2(transform.position.x, transform.position.y);
            Vector2 targetPos = new Vector2(player.transform.position.x, player.transform.position.y);

            float distToTarget = targetPos.x - currentPos.x;
            distToTarget = Mathf.Clamp(distToTarget, -oneHopDistance, oneHopDistance);  //Can only travel max distance

            var velocity = distToTarget / timeToLocation;
            impulseForce = (rigidBody.mass * velocity) / timeToLocation;   // force = impulse * velocity / times
            Debug.Log($"The impulse force {impulseForce}");

            hit = Physics2D.Linecast(currentPos, targetPos, ~ignoreLayerLinecast);
            //Debug.Log($"Ignore these mask {LayerMask.GetMask("Enemy Detection Layer", "Enemy Layer")}");
            Debug.Log($"The hit {hit.collider.gameObject.name}");
            scanTimer = 0f;
        }
         else
        {
            return;
        }

        if (hit.collider && hit.collider.gameObject.CompareTag("Player"))
        {
            Vector2 hopToTarget = new Vector2(impulseForce, jumpForce);
            rigidBody.AddForce(hopToTarget, ForceMode2D.Impulse);   //? FIXME: Change to impulse made the user still jumping despite the jumpForce is 0

            Debug.Log($"The force vector {hopToTarget}");
            Debug.Log($"The thing that was hit {hit.rigidbody.name}");
            Debug.DrawLine(transform.position, hit.rigidbody.position);
        }
    }
}
