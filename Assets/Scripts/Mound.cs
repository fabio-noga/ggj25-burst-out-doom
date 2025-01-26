
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Mound : MonoBehaviour
{
    public List<MeshRenderer> _balls;

    private int _countOfBalls;

    private void Start()
    {
        foreach(MeshRenderer mr in transform.GetComponentsInChildren<MeshRenderer>())
        {
            if(mr != null)
                _balls.Add(mr);
        }

        _countOfBalls = _balls.Count;
    }

    private void Update()
    {
        // test
        //if (Input.GetKeyDown("space"))
        //{
        //    RemoveBalls();
        //}
    }

    public void RemoveBalls()
    {
        if(_countOfBalls > 0){
            for (int i = _countOfBalls-1; i > (_countOfBalls/2)-1; i--)
            {
                _balls[i].enabled = false;
            }
            _countOfBalls -= _countOfBalls / 2;
        }
    }
}
