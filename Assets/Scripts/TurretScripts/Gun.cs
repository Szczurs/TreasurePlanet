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
        GameObject cannonball = Instantiate(shotPrefab, gunPoint.position, Quaternion.identity);
        Rigidbody rb = cannonball.AddComponent<Rigidbody>();

        //Instantiate(shotPrefab, gunPoint.position, gunPoint.rotation);
        
        //Rigidbody rb = shotPrefab.GetComponent<Rigidbody>();

        rb.velocity = Power * gunPoint.forward;
    }

    public void Fire()
    {
        firing = true;
    }
}
