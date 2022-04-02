using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    public GameObject rocketPrefab;
    public GameObject spawnPosition;

    public float speed = 1f;

    public float proximityFuseExplosionRange = 1f;

    public Transform target;

    public float maxDistanceToDetectPlayer = 50f;

    public int rocketDamage = 10;

    public Transform turret;

    private bool canShoot = true;

    public int shootInterval = 5;


    bool isDirectlyVisible()
    {
        RaycastHit hit;

        if(Vector3.Distance(transform.position, target.position) < maxDistanceToDetectPlayer)
        {
            if(Physics.Raycast(transform.position, (target.position - transform.position), out hit,maxDistanceToDetectPlayer))
            {
                if(hit.transform == target)
                {
                    //enemy can see the player
                    return true;
                }
            }
        }
        return false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(canShoot == true){
            StartCoroutine(shootWait());
        }

        if(isDirectlyVisible() )
        {
            turret.transform.LookAt(target.transform);
        }
       
    }

    public IEnumerator sendHoming(GameObject rocket)
    {
        while (Vector3.Distance(target.transform.position, rocket.transform.position)> proximityFuseExplosionRange)
        {
            rocket.transform.position += (target.transform.position - rocket.transform.position).normalized * speed * Time.deltaTime;
            rocket.transform.LookAt(target.transform);
            yield return null;
        }
        Destroy(rocket);
        //apply damage
        target.GetComponent<ShipHealth>().dealDamage(rocketDamage);
    }

    public void shootRocket()
    {
       if(isDirectlyVisible())
       {
          GameObject rocket = Instantiate(rocketPrefab, spawnPosition.transform.position, rocketPrefab.transform.rotation);
          rocket.transform.LookAt(target.transform);
          StartCoroutine(sendHoming(rocket));
       }
    }

    public IEnumerator shootWait() {
        {
            canShoot = false;
            yield return new WaitForSeconds(shootInterval);
            shootRocket();
            canShoot = true;
        }
    }
}

