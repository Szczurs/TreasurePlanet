using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BoatController : MonoBehaviour
{
    //visible Properties

    public Transform Motor; //place motor game object
    public float SteerPower = 500f;
    public float Power = 5f;
    public float MaxSpeed = 10f;
    public float Drag = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Awake()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        transform.rotation = Motor.localRotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var forceDirection = transform.forward;
        var steer = 0;

//Steer values [-1,0,1]
        if(Input.GetKey(KeyCode.A)){
            steer = 1;
        }
        if(Input.GetKey(KeyCode.D)){
            steer = -1;
        }

        //compute vectors
        var forward = Vector3.Scale(new Vector3(1, 0, 1), transform.forward);

        //Rotational force 
        GetComponent<Rigidbody>().AddForceAtPosition(steer * transform.right * SteerPower * (forward.magnitude/MaxSpeed) / 100f, Motor.position);

        var targetVel = Vector3.zero;

        //forward/backward movement
        if(Input.GetKey(KeyCode.W))
        {
            PhysicsHelper.ApplyForceToReachVelocity(GetComponent<Rigidbody>(), forward * MaxSpeed, Power);
        }
        if(Input.GetKey(KeyCode.S))
        {
            PhysicsHelper.ApplyForceToReachVelocity(GetComponent<Rigidbody>(), forward * -MaxSpeed, Power);
        }
    }
}
