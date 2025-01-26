using UnityEngine;

public class ShopButton : UIButton
{

    private BuildManager _buildManager;

    // Im lazy
    public int shopValue;

    private void Start()
    {
        base.Start();

        _buildManager = BuildManager.instance;
    }

    public override void MousePointerDown()
    {
        if(_buildManager.currencyTotal >= shopValue)
        {
            base.MousePointerDown();
        }
    }

    public override void MousePointerUp()
    {
        if (_buildManager.currencyTotal >= shopValue)
        {
            base.MousePointerUp();
            // set buy hint 
        }
    }
}
