using UnityEngine;

public class BasePickup : MonoBehaviour
{
    // made this with inheritance in mind, but never made any different pickups other than the teleport
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WormController.pickup.Invoke();
            Destroy(gameObject);
        }
    }
}
