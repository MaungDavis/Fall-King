using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//? Only use to ease the customization, prefer using the actual component to make edits
//! Circle colider radius / 2 = the actual size of the collider circle
//
public class MagnetController : MonoBehaviour
{
    [SerializeField] private float magneticZoneSize = 1;

    [SerializeField] private GameObject magneticZone;

    private CircleCollider2D circleCollider;
    private Vector3 initialScale;
    private void Start()
    {
        circleCollider = magneticZone.GetComponent<CircleCollider2D>();
        initialScale = circleCollider.transform.localScale;
    }

    // Start is called before the first frame update
    void Update()
    {
        circleCollider.transform.localScale = initialScale * magneticZoneSize;
        //magneticZone.transform.localScale = new Vector3(circleCollider.transform.localScale.x, circleCollider.transform.localScale.y, circleCollider.transform.localScale.z) * scale;
        //Debug.Log($"The child localScale after {magneticZone.transform.localScale}");
        //Debug.Log($"The collider scale {circleCollider.transform.localScale}");
    }
}
