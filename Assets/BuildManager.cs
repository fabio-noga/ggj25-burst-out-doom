using TMPro;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    public TMP_Text currencyText;
    public double currencyTotal;

    public int enemyCounter = 0;

    private void Awake()
    {
        if (instance != null)
            return;
        instance = this;
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
    }

    public void subCurrency(int price)
    {
        currencyTotal -= price;
        if(currencyTotal < 0)
        {
            currencyTotal = 0;
        }
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
}
