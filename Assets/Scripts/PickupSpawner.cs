using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ApplePrefab;
    [SerializeField] private GameObject TeleportPrefab;
    private StartSetup _startSetup;
    
    public int ticksBetweenFruitSpawn = 8;
    private int _fruitTickCount = 5;
    public int ticksBetweenTeleportSpawn = 8;
    private int _teleportTickCount = 5;
    private void Awake()
    {
        _startSetup = GetComponent<StartSetup>();
    }

    private void Start()
    {
        TickManager.tick.AddListener(TicksUntilSpawn);
    }
    private void TicksUntilSpawn()
    {
        _fruitTickCount--;
        _teleportTickCount--;
        if (_fruitTickCount == 0)
        {
            SpawnApple();
            _fruitTickCount = ticksBetweenFruitSpawn;
        }

        if (_teleportTickCount == 0)
        {
            SpawnTeleport();
            _teleportTickCount = ticksBetweenTeleportSpawn;
        }
    }

    private void SpawnTeleport()
    {
        GameObject teleport = Instantiate(TeleportPrefab, RandomPos(), Quaternion.identity, transform);
        teleport.transform.GetChild(0).gameObject.transform.position = RandomPos();
    }

    private void SpawnApple()
    {
        Vector3 randomPos = RandomPos();
        Instantiate(ApplePrefab, randomPos, Quaternion.identity, transform);
    }

    private Vector3 RandomPos()
    {
        int _boardSize = WormController.boardSize;
        
        Vector3 randomPos = new Vector3(
            Random.Range(-_boardSize, _boardSize),
            0,
            Random.Range(-_boardSize, _boardSize));

        
        Node current = _startSetup.head;
        
        // Check if randomPos is on body
        while (current.next != null)
        {
            if (current.position == randomPos)
            {
                Debug.Log("had to get new randompos");
                return RandomPos();
            }
            current = current.next;
        }

        // Check if randomPos is on another pickup
        Transform[] allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            if (child.transform.position == randomPos)
            {
                return RandomPos();
            }
        }
        
        return randomPos;
    }

    public void GameOver()
    {
        Rigidbody[] allChildren = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody child in allChildren)
        {
            child.useGravity = true;
            child.GetComponent<Collider>().isTrigger = false;
        }
    }
}
