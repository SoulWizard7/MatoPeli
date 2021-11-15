using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Transform otherPortal;
    private bool canTeleport = true;
    private Collider _collider;
    private LineRenderer _teleportLine;

    private void Start()
    {
        _teleportLine = GetComponentInChildren<LineRenderer>();
        Vector3 offset = new Vector3(0, 0.02f, 0);

        if (_teleportLine != null)
        {
            _teleportLine.enabled = false;
            _teleportLine.SetPosition(0, transform.position + offset);
            _teleportLine.SetPosition(1, otherPortal.position + offset);
        }
        else
        { 
            _teleportLine = otherPortal.GetComponentInChildren<LineRenderer>();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canTeleport)
        {
            _collider = other;
            _teleportLine.enabled = true;
            TickManager.tick.AddListener(TeleportNow);
        }
    }

    private void TeleportNow()
    {
        HighScore.score.Invoke(1);
        otherPortal.GetComponent<Teleport>().canTeleport = false;
        _collider.transform.position = otherPortal.position;
        TickManager.tick.RemoveListener(TeleportNow);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !canTeleport)
        {
            if (other.GetComponent<Node>().next == null)
            {
                Destroy(gameObject);
                Destroy(otherPortal.gameObject);
            }
        }
    }
}
