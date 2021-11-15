using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ApplePrefab;
    [SerializeField] private GameObject TeleportPrefab;
    
    [SerializeField] private int ticksBetweenFruitSpawn = 8;
    private int _fruitTickCount = 5;
    
    [SerializeField] private int ticksBetweenTeleportSpawn = 8;
    private int _teleportTickCount = 8;

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

        
        Node current = WormController.head;
        
        // Check if randomPos is on body
        while (current.next != null)
        {
            if (current.curPos == randomPos)
            {
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
