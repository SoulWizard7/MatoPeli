using UnityEngine;

public class BasePickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WormController.pickup.Invoke();
            Destroy(gameObject);
        }
    }
}
