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
        if(buildManager.GetTurretToBuild() == null)
            return;
        rend.material.color = hoverColor;    
    }

    private void OnMouseDown()
    {
        if (turret != null || buildManager.GetTurretToBuild() == null)
            return;

        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        turret = (GameObject) Instantiate(turretToBuild, transform.position, transform.rotation);
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
