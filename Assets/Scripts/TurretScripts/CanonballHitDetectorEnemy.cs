using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonballHitDetectorEnemy : MonoBehaviour
{
    public Transform mainPlayer;
    //public float raycastLength = 100f;
    public float distanceToTarget;

    public float proximityFuseExplosion = 10f;
    // Start is called before the first frame update
    void Start()
    {
        mainPlayer = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.forward, out hit, raycastLength);
        Debug.DrawRay(transform.position, Vector3.forward, Color.green);
            //hit.transform.SendMessage("HitByRay");
            if(hit.transform == mainPlayer.transform)
            {
                Debug.Log("player hit by cannon");
            }
        
        RaycastHit hit;
        */

        distanceToTarget = Vector3.Distance(mainPlayer.position, transform.position);
        if(distanceToTarget < proximityFuseExplosion)
        {
            Debug.Log("Player hit proximity fuse");
            mainPlayer.GetComponent<ShipHealth>().dealDamage(1);
            Destroy(gameObject);
        }

        RaycastHit hit;

        if(Vector3.Distance(transform.position, mainPlayer.position) < proximityFuseExplosion)
        {
            if(Physics.Raycast(transform.position, (mainPlayer.position - transform.position), out hit, proximityFuseExplosion))
            {
                if(hit.transform == mainPlayer)
                {
                    //enemy can see the player
                    //return true;
                    Debug.Log("Player hit");
                }
            }
        }
        //return false;
    }
}
