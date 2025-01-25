using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Splines;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private SplineContainer _spline;
    [SerializeField]
    private GameObject _smallEnemy;
    [SerializeField]
    private GameObject _bigEnemy;
    [SerializeField]
    private GameObject _bossEnemy;


    [SerializeField]
    private int waitTime = 1;

    [SerializeField]
    private int amount;

    private void Start()
    {
        // Temp
        if(_spline == null)
        {
            _spline = GameObject.Find("Path").GetComponent<SplineContainer>();
        }

        List<string> roundList = new List<string>
        {
            "sssbssbsgs 1"
        };

        StartSpawningCoRoutine(roundList);
    }
    
    void SpawnEnemy(char type)
    {
        GameObject enemy;
        switch (type)
        {
            case 'b': enemy = GameObject.Instantiate(_bigEnemy); break;
            case 'g': enemy = GameObject.Instantiate(_bossEnemy); break;
            default: enemy = GameObject.Instantiate(_smallEnemy); break;
        }
        enemy.GetComponent<SplineAnimate>().Container = _spline;
    }

    public void StartSpawningCoRoutine(List<string> enemyList)
    {
        StartCoroutine(SpawnerRoutine(enemyList));
    }

    IEnumerator SpawnerRoutine(List<string> enemyList)
    {
        foreach (string enemieRaw in enemyList)
        {
            string enemies = enemieRaw.Split(' ')[0];
            int waitTime = Int32.Parse(enemieRaw.Split(' ')[1]);
            foreach (char enemy in enemies)
            {
                SpawnEnemy(enemy);
                yield return new WaitForSeconds(waitTime);
            }
            yield return null;
        }
    }
}
