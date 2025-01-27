using System;
using TMPro;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    public TMP_Text currencyText;
    public double currencyTotal;
    public double currencyMax;

    public int enemyCounter = 0;

    private void Awake()
    {
        if (instance != null)
            return;
        instance = this;
    }

    private void Start()
    {
        currencyMax = currencyTotal;
    }

    private GameObject turretToBuild;

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

    public void SetTurretToBuild(GameObject turret)
    {
        turretToBuild = turret;
    }

    public void SetTurretToSell()
    {
        turretToBuild = null;
    }

    public void addCurrency(int price)
    {
        currencyTotal += price;
        setTapioca();
        checkMax();
    }

    public void subCurrency(int price)
    {
        currencyTotal -= price;
        setTapioca();
        if (currencyTotal < 0)
        {
            currencyTotal = 0;
            GameMaster.instance.GameOver();
        }
    }

    public void setTapioca() 
    {
        GameMaster.instance.setTapioca(Mathf.FloorToInt((float)(currencyTotal * 100 / currencyMax)));
    }

    private void Update()
    {
        updateCurrency();
    }

    private void updateCurrency()
    {
        currencyText.SetText(currencyTotal+"");
    }

    public bool canBuy(int price)
    {
        return currencyTotal - price >= 0;
    }

    private void checkMax()
    {
        currencyMax = Math.Max(currencyMax,currencyTotal);
    }
}
