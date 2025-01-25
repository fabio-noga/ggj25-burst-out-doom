using JetBrains.Annotations;
using UnityEngine;

public class Turret : MonoBehaviour
{

    private GameObject target;

    [Header("Attributes")]
    public float range = 15f;
    private float turnSpeed = 10f;
    public float fireRate = 1f;
    public int bulletDamage = 20;
    public float bulletSpeed = 20f;
    public int price = 100;

    [Header("Debug")]
    public string enemyTag = "enemy";
    public GameObject bulletPrefab;
    public Transform firePoint;

    private float fireCountdown = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemies = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemies < shortestDistance)
            {
                shortestDistance = distanceToEnemies;
                nearestEnemy = (GameObject) enemy; 
            }
        }
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy;
        } else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) 
            return;

        Vector3 dir = target.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        //Vector3 rotation = lookRotation.eulerAngles;
        Vector3 rotation = Quaternion.Lerp(gameObject.transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        gameObject.transform.rotation = Quaternion.Euler(0f,rotation.y,0f);

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;

        }
        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        bullet.build(bulletDamage, bulletSpeed);
        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
