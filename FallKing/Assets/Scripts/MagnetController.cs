using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//? Only use to ease the customization, prefer using the actual component to make edits
//! Circle colider radius / 2 = the actual size of the collider circle
//
public class MagnetController : MonoBehaviour
{
    private CircleCollider2D circleCollider;
    [SerializeField] private GameObject magneticZone;

    // Start is called before the first frame update
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        var scale = circleCollider.radius * 4;
        magneticZone.transform.localScale = new Vector3(circleCollider.transform.localScale.x, circleCollider.transform.localScale.y, circleCollider.transform.localScale.z) * scale;
        Debug.Log($"The child localScale after {magneticZone.transform.localScale}");
        Debug.Log($"The collider scale {circleCollider.transform.localScale}");
    }
}
