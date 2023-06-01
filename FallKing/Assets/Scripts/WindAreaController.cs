using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindAreaController : MonoBehaviour
{
    [SerializeField] private float randomizationInterval = 1f;

    private AreaEffector2D areaEffector;
    private float timer = 0f;

    void Start()
    {
        areaEffector = GetComponent<AreaEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > randomizationInterval)
        {
            areaEffector.forceAngle = (areaEffector.forceAngle + 180) % 360;
            timer = 0f;
        }
        timer += Time.deltaTime;
    }
}
