using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameObject target;
    public float speed = 70f;
    public GameObject impactEffect;
    private Boolean hitted = false;
    public int damage = 1;

    public void Seek(GameObject _target)
    {
        target = _target;
    }


    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.transform.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (!hitted && dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        hitted = true;
        GameObject effect = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effect, 2f);
        if(target != null)
            CheckLife(target);
        Destroy(gameObject);
        //Destroy(target);
    }

    void CheckLife(GameObject _enemy)
    {
        Enemy enemy = _enemy.GetComponent<Enemy>();
        enemy.subLife(damage);
    }

    public void build(int damage, float speed)
    {
        this.speed = speed;
        this.damage = damage;
    }
}
