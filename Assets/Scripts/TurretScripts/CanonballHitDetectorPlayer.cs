using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonballHitDetectorPlayer : MonoBehaviour
{
    public Transform enemy;
    //public float raycastLength = 100f;
    public float distanceToTarget;

    public float proximityFuseExplosion = 10f;

    public int damage = 10;

    // Start is called before the first frame update
    void Start()
    {
        //enemy = GameObject.FindWithTag("Enemy").transform;
    }

    void OnTriggerEnter(Collider hitInfo)
    {
        EnemyHealth enemy = hitInfo.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.dealDamage(damage);
        }
        Destroy(gameObject);
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

        /*
        distanceToTarget = Vector3.Distance(enemy.position, transform.position);
        if(distanceToTarget < proximityFuseExplosion)
        {
            Debug.Log("Player hit proximity fuse");
            enemy.GetComponent<EnemyHealth>().dealDamage(1);
            Destroy(gameObject);
        }

        RaycastHit hit;

        if(Vector3.Distance(transform.position, enemy.position) < proximityFuseExplosion)
        {
            if(Physics.Raycast(transform.position, (enemy.position - transform.position), out hit, proximityFuseExplosion))
            {
                if(hit.transform == enemy)
                {
                    //enemy can see the player
                    //return true;
                    Debug.Log("Player hit");
                }
            }
        }
        //return false;
        */
    }
}
