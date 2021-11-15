using UnityEngine;

public class StartSetup : WormController
{
    public Color headColor;

    public override void Awake()
    {
        base.Awake();
        SetupWorm();
    }

    void SetupWorm()
    {
        head = Instantiate(WormNodePrefab, Vector3.zero, Quaternion.identity).GetComponent<Node>();
        head.next = Instantiate(WormNodePrefab, Vector3.back, Quaternion.identity).GetComponent<Node>();
        head.next.next = Instantiate(WormNodePrefab, Vector3.back + Vector3.back, Quaternion.identity).GetComponent<Node>();
        
        head.curPos = Vector3.zero;
        head.next.curPos = Vector3.back;
        head.next.next.curPos = Vector3.back + Vector3.back;

        head.gameObject.transform.localScale = new Vector3(1, 1, 1);
        head.GetComponent<MeshRenderer>().material.color = headColor;
    }
}
