
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Mound : MonoBehaviour
{
    public List<MeshRenderer> _balls;

    public int ballRatio;
    private int maxBalls = 28;

    private void Start()
    {
        foreach(MeshRenderer mr in transform.GetComponentsInChildren<MeshRenderer>())
        {
            if(mr != null)
                _balls.Add(mr);
        }

        ballRatio = 100;
    }

    private void Update()
    {
        UpdateBalls();
        // test
        //if (Input.GetKeyDown("space"))
        //{
        //    RemoveBalls();
        //}
    }

    public void UpdateBalls()
    {
        if (ballRatio < 0)
            return;
        int curBalls = (int) ballRatio * maxBalls / 100;
        for (int i = maxBalls-1; i >= 0; i--)
        {
            _balls[i].enabled = i<curBalls;
        }
    }

    public void set(double value)
    {
        ballRatio = (int) value;
    }
}
