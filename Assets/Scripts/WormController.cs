using System;
using UnityEngine;
using UnityEngine.Events;

public class WormController : MonoBehaviour
{
    private Vector3 _lastDirection = Vector3.forward;
    public static readonly int boardSize = 10;

    [SerializeField] protected GameObject WormNodePrefab;
    [SerializeField] private Transform matoBodyParent;    
    [NonSerialized] public static UnityEvent pickup = new UnityEvent();
    [NonSerialized] public static Node head;
    private Vector3 HeadPosition 
    {
        get => head.transform.position;
        set => head.transform.position = value;
    }
    
    private LineRenderer _lineBody;
    private float outOfBoundsOffset = 0.3f;

    public virtual void Awake()
    {
        _lineBody = transform.GetChild(0).GetComponent<LineRenderer>();
    }

    private void Start()
    {
        TickManager.tick.AddListener(MoveWorm);
        pickup.AddListener(PickUpFruit);
    }

    private void PickUpFruit()
    {
        HighScore.score.Invoke(1);
        
        Node current = head.next.next;

        while (current.next != null)
        {
            current = current.next;
        }

        Node newNode = Instantiate(WormNodePrefab, current.curPos, Quaternion.identity, matoBodyParent).GetComponent<Node>();
        current.next = newNode;
        newNode.curPos = current.curPos;
        int point = _lineBody.positionCount++;
        _lineBody.SetPosition(point, newNode.transform.position);
    }

    private void MoveWorm ()
    {
        if (OutOfBounds() || CrashIntoSelf())
        {
            GameOver();
            return;
        }
        
        head.next.curPos = HeadPosition;

        if (HeadPosition + PlayerController.Direction() != head.next.transform.position)
        {
            HeadPosition += PlayerController.Direction();
            _lastDirection = PlayerController.Direction();
        }
        else { HeadPosition += _lastDirection; }

        head.next.Move();
        UpdateBody();
    }

    private void UpdateBody()
    {
        Node current = head;
        int count = 1;

        for (int i = 0; i < count; i++)
        {
            _lineBody.SetPosition(i, current.transform.position);
            if (current.next == null) break;
            current = current.next;
            count++;
        }
    }

    private void GameOver()
    {
        head.GetComponent<MeshRenderer>().material.color = Color.red;
        head.CreateRigidbody();
        
        GetComponent<GameMenus>().GameOver();
    }
    
    private bool CrashIntoSelf()
    {
        Node current = head.next.next;
        if (current == null) return false;

        while (current.next != null)
        {
            if (current.curPos == HeadPosition)
            {
                return true;
            }
            current = current.next;
        }
        return false;
    }

    private bool OutOfBounds()
    {
        return HeadPosition.x > boardSize + outOfBoundsOffset ||
               HeadPosition.x < -(boardSize + outOfBoundsOffset) ||
               HeadPosition.z > boardSize + outOfBoundsOffset||
               HeadPosition.z < -(boardSize + outOfBoundsOffset);
    }
}
