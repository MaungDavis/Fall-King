using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRigidBody : MonoBehaviour
{
    [SerializeField] private Transform m_RigidBody;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        transform.position = m_RigidBody.position;        
    }
}
