using UnityEngine;

public class Node : MonoBehaviour
{
    // Linked list nodes, head node starts in WormController script
    
    public Node next;
    public Vector3 curPos;

    public void Move()
    {
        if (next != null) next.curPos = transform.position;
        transform.position = curPos;
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
