using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    private Animator _animator;

    private bool mIsLoaded = false;
    public Transform CannonBall_Spawn;

    public GameObject Cannonball;

    public ParticleSystem PS_Smoke;

    public float Power = 12.0f;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        //Manual select spawn transform in inspector
        //CannonBall_Spawn = transform.Find("CannonBallSpawn");    
        PS_Smoke = transform.Find("PS_Smoke").GetComponent<ParticleSystem>();
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.G))
        {
            ShootCannonBall();
        }

        if(Input.GetKey(KeyCode.R))
        {
            transform.Rotate(Vector3.up,15, Space.World);
        }
    }

    private void ShootCannonBall()
    {
        GameObject cannonball = Instantiate(Cannonball, CannonBall_Spawn.position, Quaternion.identity);
        Rigidbody rb = cannonball.AddComponent<Rigidbody>();

        rb.velocity = Power * CannonBall_Spawn.forward;

        StartCoroutine(RemoveCannonBall_Rigidody(rb, 3.0f));

        _animator.SetTrigger("tr_shoot");

        PS_Smoke.Play();

    }

    IEnumerator RemoveCannonBall_Rigidody(Rigidbody rb, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(rb);
    }
}
