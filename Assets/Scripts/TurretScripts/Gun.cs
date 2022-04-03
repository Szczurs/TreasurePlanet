using UnityEngine;
public class Gun : MonoBehaviour
{
    public GameObject shotPrefab;
    public Transform[] gunPoints;
    public float fireRate;

    public float Power = 120f;

    bool firing;
    float fireTimer;

    int gunPointIndex;

    void Update()
    {
        if (firing)
        {
            while (fireTimer >= 1 / fireRate)
            {
                SpawnShot();
                fireTimer -= 1 / fireRate;
            }

            fireTimer += Time.deltaTime;
            firing = false;
        }
        else
        {
            if (fireTimer < 1 / fireRate)
            {
                fireTimer += Time.deltaTime;
            }
            else
            {
                fireTimer = 1 / fireRate;
            }
        }
    }

    void SpawnShot()
    {
        var gunPoint = gunPoints[gunPointIndex++];
        gunPointIndex %= gunPoints.Length;
        //GameObject cannonball = Instantiate(shotPrefab, gunPoint.position, Quaternion.identity);
        //Rigidbody rb = cannonball.AddComponent<Rigidbody>();
        /*
        GameObject cannonball = Instantiate(Cannonball, CannonBallSpawn.position, Quaternion.identity);
        Rigidbody rb = cannonball.AddComponent<Rigidbody>();

        rb.velocity = Power * CannonBallSpawn.forward;
        */
        GameObject canonball = Instantiate(shotPrefab, gunPoint.position, gunPoint.rotation);
        canonball.GetComponent<Rigidbody>().velocity = Power * gunPoint.forward;
        //Rigidbody rb = shotPrefab.GetComponent<Rigidbody>();
        //shotPrefab.GetComponent<Rigidbody>().velocity = Power * gunPoint.forward;
        //rb.velocity = Power * gunPoint.forward;
    }

    public void Fire()
    {
        firing = true;
    }
}
