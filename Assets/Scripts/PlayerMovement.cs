using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the player
    private Vector3 forward, right; // Movement directions

    void Start()
    {
        // Setting up movement directions according to isometric perspective
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);

        right = Camera.main.transform.right;
        right.y = 0;
        right = Vector3.Normalize(right);
    }

    void Update()
    {
        // Getting input axes for movement
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (direction.magnitude > 0.1f) // Ensure some input is pressed
        {
            Move(direction);
        }
    }

    void Move(Vector3 direction)
    {
        // Translating movement direction to isometric space
        Vector3 rightMovement = right * direction.x;
        Vector3 upMovement = forward * direction.z;

        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

        // Moving the player
        transform.position += heading * moveSpeed * Time.deltaTime;

        // Optionally rotate player to face movement direction
        transform.forward = heading;
    }
}
