using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Transform camera;
    [SerializeField] private Transform player;
    [SerializeField] private float relativeMove = 0.3f;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(camera.position.x, camera.position.y);
    }
}
