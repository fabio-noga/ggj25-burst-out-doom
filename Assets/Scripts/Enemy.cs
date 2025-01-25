using UnityEngine;

public class Enemy : SlideObject
{
    private void Update()
    {
    }

    protected override void OnReachSplineEnd()
    {
        base.OnReachSplineEnd();

        // retirar score, vida whatever
    }
}
