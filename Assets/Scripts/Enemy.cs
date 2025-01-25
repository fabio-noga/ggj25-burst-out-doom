using System;
using UnityEngine;

public class Enemy : SlideObject
{
    BuildManager buildManager = BuildManager.instance;
    public EnemyClass type = EnemyClass.SMALL;

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

    protected override void OnReachSplineEnd()
    {
        base.OnReachSplineEnd();

        // retirar score, vida whatever
        if (type == EnemyClass.BOSS)
            buildManager.subCurrency(10000);
        else 
            buildManager.subCurrency(20);

        Debug.Log("END");
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
            Debug.Log(getPrice());
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
}
