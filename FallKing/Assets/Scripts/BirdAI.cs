using UnityEngine;
using Pathfinding;

public class BirdAI : MonoBehaviour
{
    [Header("Pathfinding")]
    [SerializeField] Transform target;
    [SerializeField] float pathUpdateInterval = 1f;    // time between each path calculation
    [SerializeField] float distThresholdToGetNextWaypoint = 3f; // how far the user need to be before moving to the next way point

    [Header("Physics")]
    [SerializeField] float speed = 200f;
    [SerializeField]

    Path path;
    int currentWaypoint = 0;
    Seeker seeker;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, pathUpdateInterval);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
        {
            Debug.Log("The path is null");
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }

        // Direction calculation
        Vector2 toNextWaypointDir = ((Vector2)(path.vectorPath[currentWaypoint]) - rb.position).normalized;
        Vector2 moveForce = toNextWaypointDir * speed;

        // Movement
        rb.AddForce(moveForce);
        //Debug.Log($"The move force {moveForce}");

        // Check if on the way to next way point
        float distToNextWaypoint = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distToNextWaypoint < distThresholdToGetNextWaypoint)  // increment target waypoint when pass the threshold
        {
            currentWaypoint++;
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;    //at the begining of a new calculated path
        }
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }




}
