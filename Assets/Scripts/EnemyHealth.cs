using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 100;
    // Start is called before the first frame update
    public void dealDamage(int damageAmmount)
    {
        health = health - damageAmmount;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health < 0){
            //kill ship
            Destroy(gameObject);
        }
    }
}
