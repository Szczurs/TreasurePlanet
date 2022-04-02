using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStabilizer : MonoBehaviour
{
    public float stability = 0.3f;
    public float speed = 2.0f;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 predictedUp = Quaternion.AngleAxis(
            gameObject.GetComponent<Rigidbody>().angularVelocity.magnitude * Mathf.Rad2Deg * stability / speed,
            gameObject.GetComponent<Rigidbody>().angularVelocity
        ) * transform.up;
        Vector3 torqueVector = Vector3.Cross(predictedUp, Vector3.up);
        gameObject.GetComponent<Rigidbody>().AddTorque(torqueVector * speed * speed);
    }



}
