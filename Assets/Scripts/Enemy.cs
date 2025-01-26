using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Enemy : SlideObject
{
    public EnemyClass type = EnemyClass.SMALL;

    private Quaternion _fixedRotation = Quaternion.Euler(45.0f, 0.0f, 0.0f);

    public Slider _slider;

    private AudioSource _audioSource;
    
    [SerializeField]
    private AudioResource _bossSfx;
    [SerializeField]
    private AudioResource _normalSfx;

    /*public Enemy(EnemyClass type) 
    {
        this.type = type;
        switch (type)
        {
            case EnemyClass.BIG:
                setLife(500); break;
            case EnemyClass.BOSS:
                setLife(2000); break;
            default:
                setLife(100); break;
        }
    }*/
    /*private void Start()
    {
         buildManager = BuildManager.instance;
    }*/
    private void Start()
    {
        base.Start();
        _slider.maxValue = _maxHp;
    }

    protected void Update()
    {
        base.Update();
        _slider.transform.rotation = _fixedRotation;

        if(_slider.value != _hp)
            _slider.value = _hp;
    }

    protected override void OnReachSplineEnd()
    {
        base.OnReachSplineEnd();


        // retirar score, vida whatever
        if (type == EnemyClass.BOSS)
        {
            PlayAudioSource(_bossSfx);
            buildManager.subCurrency(10000);
            _gameMaster.SubTapiocaHp(100);
        }
        else
        {
            PlayAudioSource(_normalSfx);
            buildManager.subCurrency(20);
            _gameMaster.SubTapiocaHp(10);
        }

        buildManager.enemyCounter--;
        Destroy(gameObject);
    }

    public int getLife()
    {
        return _hp;
    }
    public void setLife(int life)
    {
        _hp = life;
    }


    public void subLife(int hp)
    {
        _hp -= hp;

        if (getLife() <= 0)
        {
            Destroy(gameObject);
            buildManager.enemyCounter--;
            buildManager.addCurrency(getPrice());
        }

    }

    public int getPrice()
    {
        switch(type)
        {
            case EnemyClass.BIG:
                return 50;
            case EnemyClass.BOSS:
                return 200;
        }
        return 20;
    }

    public enum EnemyClass
    {
        SMALL, BIG, BOSS
    }

    void PlayAudioSource(AudioResource audioClip)
    {
        if(_audioSource == null)
        {
            _audioSource = GetComponent<AudioSource>();
        }
        _audioSource.resource = audioClip;

        _audioSource.Play();
    }
}

public class SmallEnemy : Enemy
{
    public SmallEnemy()
    {
        type = EnemyClass.SMALL;
    }
}

public class BigEnemy : Enemy
{
    public BigEnemy()
    {
        type = EnemyClass.BIG;
    }
}

public class BossEnemy : Enemy
{
    public BossEnemy()
    {
        type = EnemyClass.BOSS;
    }
}


