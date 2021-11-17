using System;
using UnityEngine;
using UnityEngine.Events;

// This script is just a timer that PickupSpawner and WormController listen to. Basicly the speed of the game.
// tickTime var could be altered if game should be faster but I felt there was no need as gameplay was already
// difficult.
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
