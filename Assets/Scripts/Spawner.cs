using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
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

    [SerializeField]
    private List<string> roundList;
    public bool isRoundGoing = false;

    private int roundsLeft;
    private int roundsTotal;

    BuildManager buildManager;

    AudioSource _audioSource;

    public AudioResource _roundSfx;
    public AudioResource _spawnSfx;

    private void Start()
    {
        // Temp
        if(_spline == null)
        {
            _spline = GameObject.Find("Path").GetComponent<SplineContainer>();
        }

        roundList = new List<string>
        {
            "sssssbb 1000",
            "ssbssbss 900",
            "ssbgssb 800",
            "bbbssbggg 700",
            "sbbsbbggg 600",
            "bbbgggbbb 500"
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
        // temp hack
        if (countdown < 999)
        {
            countdown -= Time.deltaTime;
            countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
            timeText.SetText(string.Format("{0:00.00}", countdown));
        }
    }

    void SpawnEnemy(char type)
    {
        GameObject enemy;
        switch (type)
        {
            case 'b': 
                enemy = GameObject.Instantiate(_bigEnemy);
                enemy.GetComponent<Enemy>().setType(EnemyClass.BIG);
                break;
            case 'g': 
                enemy = GameObject.Instantiate(_bossEnemy);
                enemy.GetComponent<Enemy>().setType(EnemyClass.GHOST);
                break;
            default: 
                enemy = GameObject.Instantiate(_smallEnemy);
                enemy.GetComponent<Enemy>().setType(EnemyClass.SMALL);
                break;
        }
        buildManager.enemyCounter++;
        enemy.GetComponent<SplineAnimate>().Container = _spline;
        enemy.transform.SetPositionAndRotation(new Vector3(-100.0f, 0f, 0f), Quaternion.identity);
        PlayAudioSource(_spawnSfx);
    }

    public void StartSpawningCoRoutine(List<string> enemyList)
    {
        string round = "";
        if (roundsTotal - roundsLeft >= roundsTotal)
        {
            round = generateRound();

            // game WIN routine
            // go to menu
            // countdown = 9999
            
        }
        else
        {
            round = enemyList.ElementAt(roundsTotal - roundsLeft);
        }
        Debug.Log(round);
        StartCoroutine(SpawnerRoutine(round));
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
        roundsLeft--;
        isRoundGoing = false;
        if(roundsLeft < 0)
        {
            //GameMaster.instance.Win();
            //countdown = 99999;
        }
        else
        {
            countdown = timeBetweenWaves;
            StartCoroutine(PlayRoundAudioCoroutine());
        }
    }

    private string generateRound()
    {
        string result = "";
        System.Random r = new System.Random();
        int size = r.Next(10, 15);
        for (int i = 0; i <= size; i++)
        {
            int randomVal = r.Next(0, 10);
            if (randomVal < 5) result += "s";
            if (randomVal > 4 && randomVal < 8) result += "b";
            if (randomVal > 7) result += "g";
        }

        return result + " 500";
    }

    IEnumerator PlayRoundAudioCoroutine()
    {
        for (int i = 0; i >= 5; i++){
            yield return new WaitForSeconds(0.1f);
            PlayAudioSource(_roundSfx);
        }
        yield return null;
    }

    void PlayAudioSource(AudioResource audioClip)
    {
        if (_audioSource == null)
        {
            _audioSource = GetComponent<AudioSource>();
        }
        _audioSource.resource = audioClip;
        _audioSource.Play();
    }
}
