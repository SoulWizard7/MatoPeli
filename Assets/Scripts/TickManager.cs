using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TickManager : MonoBehaviour
{
    public float tickTime = .5f;
    private float tempTick;
    
    [NonSerialized] public static UnityEvent tick = new UnityEvent();
    
    private void Start()
    {
        tempTick = tickTime;
    }
    
    void Update()
    {
        tempTick -= Time.deltaTime;
        if (tempTick <= 0)
        {
            tick.Invoke();
            tempTick = tickTime;
        }
    }
}
