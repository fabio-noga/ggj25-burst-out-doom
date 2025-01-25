using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private SplineContainer _spline;
    [SerializeField]
    private GameObject _enemy;

    [SerializeField]
    private int waitTime = 1;

    [SerializeField]
    private int amount;

    private void Start()
    {
        // Temp
        SpawnerRoutine(3, 1);
    }

    void SpawnEnemy()
    {
        GameObject enemy = GameObject.Instantiate(_enemy);
        enemy.GetComponent<SplineAnimate>().Container = _spline;
    }

    public void StartSpawningCoRoutine(int amountToSpawn, int waitTime)
    {
        StartCoroutine(SpawnerRoutine(amountToSpawn, waitTime));
    }

    IEnumerator SpawnerRoutine(int amountToSpawn, int waitTime)
    {
        for(int i = 0; i < amountToSpawn; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(waitTime);
        }
        yield return null;
    }
}
