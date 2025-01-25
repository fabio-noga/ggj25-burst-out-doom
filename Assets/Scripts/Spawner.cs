using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using TMPro;
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

    public float timeBetweenWaves = 5f;
    private float countdown;
    public TMP_Text timeText;

    private List<string> roundList;
    public bool isRoundGoing = false;

    private int roundsTotal;
    private int roundsLeft;

    BuildManager buildManager;

    AudioSource _audioSource;

    private void Start()
    {
        // Temp
        if(_spline == null)
        {
            _spline = GameObject.Find("Path").GetComponent<SplineContainer>();
        }

        roundList = new List<string>
        {
            "sssbssbsgs 1000",
            "sbsbsgggs 500"
        };

        roundsLeft = roundList.Count;
        roundsTotal = roundsLeft;

        countdown = timeBetweenWaves;
        buildManager = BuildManager.instance;
    }
    private void Update()
    {
        if (isRoundGoing)
        {
            if (buildManager.enemyCounter == 0)
            {
                resetRound();
            }
            return;
        }

        if(countdown <= 0f)
        {
            isRoundGoing=true;
            StartSpawningCoRoutine(roundList);
        }

        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        timeText.SetText(string.Format("{0:00.00}", countdown));
    }

    void SpawnEnemy(char type)
    {
        GameObject enemy;
        switch (type)
        {
            case 'b': 
                enemy = GameObject.Instantiate(_bigEnemy);
                enemy.GetComponent<Enemy>().setSpeed(2f);
                break;
            case 'g': 
                enemy = GameObject.Instantiate(_bossEnemy);
                enemy.GetComponent<Enemy>().setSpeed(.3f);
                break;
            default: enemy = GameObject.Instantiate(_smallEnemy); break;
        }
        buildManager.enemyCounter++;
        enemy.GetComponent<SplineAnimate>().Container = _spline;
        PlayAudioSpawnEnemy();
    }

    public void StartSpawningCoRoutine(List<string> enemyList)
    {
        StartCoroutine(SpawnerRoutine(enemyList.ElementAt(roundsTotal - roundsLeft)));
    }

    IEnumerator SpawnerRoutine(string enemieRaw)
    {
        string enemies = enemieRaw.Split(' ')[0];
        int waitTime = Int32.Parse(enemieRaw.Split(' ')[1]);
        foreach (char enemy in enemies)
        {
            SpawnEnemy(enemy);
            yield return new WaitForSeconds(waitTime/1000f);
        }
        yield return null;
    }

    private void resetRound()
    {
        if (roundsLeft == 0)
            return;
        roundsLeft--;
        isRoundGoing = false;
        countdown = timeBetweenWaves;
    }

    void PlayAudioSpawnEnemy()
    {
        if (_audioSource == null)
        {
            _audioSource = GetComponent<AudioSource>();
        }
        _audioSource.Play();
    }
}
