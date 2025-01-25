using UnityEngine;

public class Plot : MonoBehaviour
{
    public Color hoverColor;

    private Renderer rend;
    private Color startColor;

    private GameObject turret;
    private BuildManager buildManager;

    private void OnMouseEnter()
    {
        rend.material.color = hoverColor;    
    }

    private void OnMouseDown()
    {   
        if(buildManager.GetTurretToBuild() == null)
        {
            if (turret == null)
                return;
            sellTurret();
            return;
        }
        if(turret != null)
            return;
        buildTurret();
    }

    private void sellTurret()
    {
        Turret turretParams = turret.GetComponent<Turret>();
        buildManager.addCurrency((int) (turretParams.price * 0.8)); //80% of the price back
        Destroy(turret);
        turret = null;
    }

    private void buildTurret()
    {
        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        int price = turretToBuild.GetComponent<Turret>().price;
        if (!buildManager.canBuy(price))
            return;
        turret = (GameObject)Instantiate(turretToBuild, transform.position, transform.rotation);

        Turret turretParams = turret.GetComponent<Turret>();
        buildManager.subCurrency(turretParams.price); //80% of the price back
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
