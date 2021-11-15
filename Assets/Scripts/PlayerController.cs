using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static Vector2Int direction = Vector2Int.up;
    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (horizontal < 0) direction = Vector2Int.left; 
        else if (horizontal > 0) direction = Vector2Int.right;
        else if (vertical < 0) direction = Vector2Int.down;
        else if (vertical > 0) direction = Vector2Int.up;
    }

    public static Vector3 Direction()
    {
        Vector3 movement = new Vector3(direction.x, 0, direction.y);
        return movement;
    }
}
