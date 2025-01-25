using TMPro;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    public TextMeshPro currencyText;
    private double currencyTotal;

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

    public void addCurrency(int price)
    {

    }

    public void subCurrency(int price)
    {

    }

    private void Update()
    {
        updateCurrency();
    }

    private void updateCurrency()
    {
        //currencyText.text = currencyTotal+"";
    }

    private void Start()
    {
        currencyTotal = 1000;
    }
}
