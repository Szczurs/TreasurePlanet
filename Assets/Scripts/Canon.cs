using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    private Animator _animator;

    private bool mIsLoaded = false;
    public List<Transform> CannonballLeftSpawns;

    public List<Transform> CannonballRightSpawns;

    public GameObject Cannonball;

    public GameObject canon;

    public ParticleSystem PS_Smoke;

    public float Power = 12.0f;

    public bool leftSideCanonActive = true;

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
        if(Input.GetKeyUp(KeyCode.Q)){
            leftSideCanonActive = false;
        }

        if(Input.GetKeyUp(KeyCode.E)){
            leftSideCanonActive = true;
        }

        if(Input.GetKeyUp(KeyCode.G))
        {
            ShootCannonBall();
        }

        if(Input.GetKeyUp(KeyCode.R))
        {
            canon.transform.Rotate(Vector3.up,15, Space.World);
        }
    }

    private void ShootCannonBall()
    {
        List<Transform> activeCannonList = null;
        if(leftSideCanonActive == true)
        {
            activeCannonList = CannonBallLeftSpawns;
        }
        else
        {
            activeCannonList = CannonBallRightSpawns;
        }

        foreach(var CannonBallSpawn in activeCannonList)
        {
        GameObject cannonball = Instantiate(Cannonball, CannonBallSpawn.position, Quaternion.identity);
        Rigidbody rb = cannonball.AddComponent<Rigidbody>();

        rb.velocity = Power * CannonBallSpawn.forward;

        StartCoroutine(RemoveCannonBall_Rigidody(rb, 3.0f));

        _animator.SetTrigger("tr_shoot");

        PS_Smoke.Play();
        }

    }

    IEnumerator RemoveCannonBall_Rigidody(Rigidbody rb, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(rb);
    }
}
