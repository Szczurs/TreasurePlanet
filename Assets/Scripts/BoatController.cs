using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BoatController : MonoBehaviour
{
    //visible Properties
    public float startElevation;
    public Transform motor; //place motor game object
    public float steerPower = 500f;
    public float engineForwadThrustPower = 5f;
    public float engineReverseThrustPowerInPercent = 20f; //Default 20% of forward thrust
    public float maxSpeed = 10f;
    public float drag = 0.1f;

    public Transform rotationPivotForFrontElement;

    public float maxRotationForFrontElement = 30f;

    public float frontElementRotation;

    public float rotationSpeed = 2.0f;

    public float levelingAltitudeSpeed = 1f;

    public int steer;

    public float currentRoll;

    public float maxRoll = 15f;

    public float currentPitch;

    public float maxNeutralAngle = 1f;

    private float angleOffset = 5f; //offset to stabilize ship

    // Start is called before the first frame update
    void Start()
    {
        //startElevation = transform.position.y;
    }

    public void Awake()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        transform.rotation = motor.localRotation;
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
        currentRoll = transform.localRotation.eulerAngles.z;
        currentPitch = transform.localRotation.eulerAngles.x;
        
         //X axis stabilization

        if(currentPitch > maxNeutralAngle && (currentPitch < (360 - maxNeutralAngle)))
        {
            transform.Rotate(Vector3.right * -2 * (rotationSpeed * Time.deltaTime));
        }
        else
        {
            if(currentPitch > (360 - maxNeutralAngle))
            {
                transform.Rotate(Vector3.right * 2 * (rotationSpeed * Time.deltaTime));
            }
            
        }

        //keep constant elevation

        if(Mathf.Abs(transform.position.y - startElevation) > 0.01f)
        {
            //transform.position = (transform.position).normalized * levelingAltitudeSpeed * Time.deltaTime;
            transform.position = new Vector3 (transform.position.x, startElevation, transform.position.z);
        }

        if (steer == 0)
        {
            if (frontElementRotation > maxNeutralAngle && frontElementRotation < (360f - maxNeutralAngle))
            {
                if (frontElementRotation < 180f)
                {
                    rotationPivotForFrontElement.Rotate(Vector3.up * -2 * (rotationSpeed * Time.deltaTime));
                }

                else
                {
                    //turn left
                    rotationPivotForFrontElement.Rotate(Vector3.up * 2 * (rotationSpeed * Time.deltaTime));
                    
                    
                }

                if(currentRoll < (360f - maxNeutralAngle) && currentRoll > maxRoll + angleOffset)
                {
                    transform.Rotate(Vector3.forward * 2 * (rotationSpeed * Time.deltaTime));
                    //Debug.Log("U");
                    //Debug.Log("D");
                }
                else
                {
                    //roll is on 0-maxRoll
                    if(currentRoll > maxNeutralAngle)
                    { 
                        transform.Rotate(Vector3.forward * -2 * (rotationSpeed * Time.deltaTime));
                        //Debug.Log("U");
                    }
                }
            }
        }
        else
        {
            //turn left
            if(frontElementRotation > 180)
            {
                if(360 - maxRotationForFrontElement < frontElementRotation)
                {
                    rotationPivotForFrontElement.Rotate(Vector3.up * -steer * (rotationSpeed * Time.deltaTime));
                    if(currentRoll > (360 - maxRoll) || currentRoll < maxRoll) //apply max roll
                    {
                        transform.Rotate(Vector3.forward * steer * (rotationSpeed * Time.deltaTime));
                    }
                }
            }
            else
            {
                //turn right
                if(frontElementRotation < maxRotationForFrontElement)
                {
                    rotationPivotForFrontElement.Rotate(Vector3.up * -steer * (rotationSpeed * Time.deltaTime));
                    if(currentRoll > (360 - maxRoll) || currentRoll < maxRoll) //apply max roll
                    {
                        transform.Rotate(Vector3.forward * steer * (rotationSpeed * Time.deltaTime));
                    }
                }
            }
        }

        #endregion


        //compute vectors
        var forward = Vector3.Scale(new Vector3(1, 0, 1), transform.forward);

        //Rotational force 
        GetComponent<Rigidbody>().AddForceAtPosition(steer * transform.right * steerPower * (forward.magnitude / maxSpeed), motor.position);
        //GetComponent<Rigidbody>().AddForceAtPosition(steer * transform.right * SteerPower / 100f, Motor.position);
        var targetVel = Vector3.zero;

        //forward/backward movement
        if (Input.GetKey(KeyCode.W))
        {
            PhysicsHelper.ApplyForceToReachVelocity(GetComponent<Rigidbody>(), forward * maxSpeed, engineForwadThrustPower);
        }
        if (Input.GetKey(KeyCode.S))
        {
            PhysicsHelper.ApplyForceToReachVelocity(GetComponent<Rigidbody>(), forward * -maxSpeed, engineForwadThrustPower * (engineReverseThrustPowerInPercent/100f));
        }
    }
}
