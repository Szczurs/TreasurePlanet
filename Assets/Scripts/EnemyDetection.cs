using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    //Track enemy
    public Transform mainPlayer; 

    public float distanceToMainPlayer;

    public bool detectedMainPlayer = false;

    public float maxDistanceToDetectPlayer = 200f;

    bool distanceDetection()
    {
        if(mainPlayer)
        {
           distanceToMainPlayer = Vector3.Distance(mainPlayer.position,transform.position);
           if(distanceToMainPlayer < maxDistanceToDetectPlayer)
           {
               return true;
           }
        }

        return false;
    }

    bool isDirectlyVisible()
    {
        RaycastHit hit;
        if(mainPlayer != null){
            if(Vector3.Distance(transform.position, mainPlayer.position) < maxDistanceToDetectPlayer)
            {
                if(Physics.Raycast(transform.position, (mainPlayer.position - transform.position), out hit,maxDistanceToDetectPlayer))
                {
                    if(hit.transform == mainPlayer)
                    {
                        //enemy can see the player
                        return true;
                    }
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
        /*
        if(distanceDetection()){
            detectedMainPlayer = true;
        }
        */
        detectedMainPlayer = isDirectlyVisible();

    }
}
