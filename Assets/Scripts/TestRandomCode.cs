using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRandomCode : MonoBehaviour
{
    private Rigidbody BallRigidBody;
    public float speed = 15;

    private bool isTravelling;
    private Vector3 TravelDir;
    private Vector3 NextColision;


    void Start()
    {
        BallRigidBody = GetComponent<Rigidbody>();
        Debug.Log(isTravelling);
    }

    private void FixedUpdate()
    {
        if(isTravelling)
        {
            BallRigidBody.velocity = speed * Vector3.forward;
        }
        
    }
}
