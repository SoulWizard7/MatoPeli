using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Node : MonoBehaviour
{
    public Node next;
    public Vector3 position;

    public void Move()
    {
        if (next != null) next.position = transform.position;
        transform.position = position;
        if (next == null) return;
        next.Move();
    }

    public void CreateRigidbody()
    {
        Rigidbody rb = gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
        rb.AddTorque(new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)), ForceMode.Impulse);
        if (next == null) return;
        next.CreateRigidbody();
    }
}
