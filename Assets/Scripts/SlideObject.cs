using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class SlideObject : MonoBehaviour
{
    protected BuildManager buildManager;
    public SplineAnimate _splineAnim { get; set; }

    public float _duration = 1.0f;

    [SerializeField]
    protected int _hp = 100;

    protected bool _isDead = false;

    // Game Manager reference

    [SerializeField]
    private bool _reset = false;

    void OnStartBehavior()
    {
        _splineAnim = GetComponent<SplineAnimate>();
        _splineAnim.Loop = SplineAnimate.LoopMode.Once;
        _splineAnim.Duration = _duration;
    }

    void ResetSplinePath()
    {
        _splineAnim.Restart(true);
    }
    
    void PlaySplinePath()
    {
        _splineAnim.Play();
    }

    public bool CheckSplineEnd()
    {
        if(_splineAnim.ElapsedTime >= _duration)
        {
            return true;
        }
        return false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OnStartBehavior();
        PlaySplinePath();
        buildManager = BuildManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        // Temp
        if(_reset == true)
        {
            _reset = false;
            ResetSplinePath();
        }
        if(CheckSplineEnd() == true){
            OnReachSplineEnd();
        }
    }

    public void ReceiveDamage()
    {
        _hp--;
        CheckDestroy();
    }

    private void CheckDestroy()
    {
        if(_hp <= 0)
        {
            _isDead = true;
            _splineAnim.Pause();
            StartCoroutine(DestroyProcess());
        }
    }

    private void DestroyThis()
    {
        Destroy(gameObject);
    }

    IEnumerator DestroyProcess()
    {
        yield return new WaitForSeconds(3);

        DestroyThis();

        yield return null;
    }

    protected virtual void OnReachSplineEnd() { }
}
