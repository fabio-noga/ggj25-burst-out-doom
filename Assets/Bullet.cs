using UnityEngine;

public class Bullet : MonoBehaviour
{

    private GameObject target;
    public float speed = 70f;
    public GameObject impactEffect;

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

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        GameObject effect = Instantiate(impactEffect, transform.position, transform.rotation);
        CheckLife(target);
        Destroy(gameObject);
        //Destroy(target);
        Destroy(effect,2f);
    }

    void CheckLife(GameObject _enemy)
    {
        EnemyMovement enemy = _enemy.GetComponent<EnemyMovement>();
        enemy.life -= 10;
        Debug.Log(enemy.life);
        if (enemy.life <= 0)
            Destroy(_enemy);
    }
}
