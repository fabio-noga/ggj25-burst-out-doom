using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public enum EnemyClass
{
    SMALL, BIG, GHOST
}
public class Enemy : SlideObject
{
    public static readonly Dictionary<EnemyClass, (int life, int damage, float speed)> _stats = new()
    {
        { EnemyClass.SMALL, (life: 200, damage: 100, speed:1f) },
        { EnemyClass.BIG, (life: 500, damage: 1000, speed:1.65f) },
        { EnemyClass.GHOST, (life: 100, damage: 50, speed:.5f) }
    };

    private EnemyClass type = EnemyClass.SMALL;

    private Quaternion _fixedRotation = Quaternion.Euler(45.0f, 0.0f, 0.0f);

    public Slider _slider;

    private AudioSource _audioSource;
    
    [SerializeField]
    private AudioResource _bossSfx;
    [SerializeField]
    private AudioResource _normalSfx;
    private int damage;

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
    private new void Start()
    {
        base.Start();
        _slider.maxValue = _maxHp;
    }

    protected new void Update()
    {
        base.Update();
        _slider.transform.rotation = _fixedRotation;

        if(_slider.value != _hp)
            _slider.value = _hp;
    }

    protected override void OnReachSplineEnd()
    {
        base.OnReachSplineEnd();


        PlayAudioSource(_normalSfx);
        buildManager.subCurrency(damage);
        _gameMaster.SubTapiocaHp(damage);

        buildManager.enemyCounter--;
        Destroy(gameObject);
    }

    public int getLife()
    {
        return _hp;
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
            case EnemyClass.GHOST:
                return 200;
        }
        return 20;
    }

    public void setType(EnemyClass type)
    {
        this.type = type;
        setLife();
        setDamage();
        setSpeed();
    }

    private void setLife()
    {
        _hp = _stats[type].life;
    }
    private void setDamage()
    {
        damage = _stats[type].damage;
    }

    private void setSpeed()
    {
        base.setSpeed(_stats[type].speed);
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


