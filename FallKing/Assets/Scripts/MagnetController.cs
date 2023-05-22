using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//? Only use to ease the customization, prefer using the actual component to make edits
public class MagnetController : MonoBehaviour
{
    [SerializeField] private float magneticForce;
    [SerializeField] private float effectRadius;

    private PointEffector2D effector;
    private CircleCollider2D circleCollider;
    // Start is called before the first frame update
    void Start()
    {
        effector = GetComponent<PointEffector2D>();
        circleCollider = GetComponent<CircleCollider2D>();

        effector.forceMagnitude = magneticForce;
        circleCollider.radius = effectRadius;
    }
}
