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

    public Transform rotationPivotForFrontElement;

    public float maxRotationForFrontElement = 30f;

    public float frontElementRotation;

    public float rotationSpeed = 2.0f;

    public int steer;


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
    void Update()
    {

    }
    void FixedUpdate()
    {
        //transform.Rotate(0,transform.rotation)
        //gameObject.transform.rotation.x = 0;
        //transform.rotation.z = 0;



        var forceDirection = transform.forward;
        steer = 0;


        //Steer values [-1,0,1]
        if (Input.GetKey(KeyCode.A))
        {
            steer = 1;

        }
        if (Input.GetKey(KeyCode.D))
        {
            steer = -1;
        }
    
        #region Apply front element rotation

        frontElementRotation = rotationPivotForFrontElement.localRotation.eulerAngles.y;

        
        if (steer == 0)
        {

            if (frontElementRotation > 1f && frontElementRotation < 359f)
            {
                if (frontElementRotation < 180f)
                {
                    rotationPivotForFrontElement.Rotate(Vector3.up * -2 * (rotationSpeed * Time.deltaTime));
                    //Debug.Log("D");
                }

                else
                {
                    rotationPivotForFrontElement.Rotate(Vector3.up * 2 * (rotationSpeed * Time.deltaTime));
                    //Debug.Log("U");
                }
            }

        }
        else
        {
            if(frontElementRotation > 180)
            {
                if(360 - maxRotationForFrontElement < frontElementRotation)
                {
                    rotationPivotForFrontElement.Rotate(Vector3.up * -steer * (rotationSpeed * Time.deltaTime));
                }
            }
            else
            {
                if(frontElementRotation < maxRotationForFrontElement)
                {
                    rotationPivotForFrontElement.Rotate(Vector3.up * -steer * (rotationSpeed * Time.deltaTime));
                }
            }
            
        }

        #endregion

        //compute vectors
        var forward = Vector3.Scale(new Vector3(1, 0, 1), transform.forward);

        //Rotational force 
        GetComponent<Rigidbody>().AddForceAtPosition(steer * transform.right * SteerPower * (forward.magnitude / MaxSpeed) / 100f, Motor.position);
        //GetComponent<Rigidbody>().AddForceAtPosition(steer * transform.right * SteerPower / 100f, Motor.position);
        var targetVel = Vector3.zero;

        //forward/backward movement
        if (Input.GetKey(KeyCode.W))
        {
            PhysicsHelper.ApplyForceToReachVelocity(GetComponent<Rigidbody>(), forward * MaxSpeed, Power);
        }
        if (Input.GetKey(KeyCode.S))
        {
            PhysicsHelper.ApplyForceToReachVelocity(GetComponent<Rigidbody>(), forward * -MaxSpeed, Power);
        }
    }
}
