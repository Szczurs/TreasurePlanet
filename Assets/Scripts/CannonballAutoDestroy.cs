using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballAutoDestroy : MonoBehaviour
{
    public float delay = 10.0f; //This impiles a delay of 2 seconds.

 void Start()
 {
     StartCoroutine(SelfDestruct());
 }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
