using UnityEngine;

public class Elevator : MonoBehaviour
{
    public float targetY = 5f;      // The height the platform will reach
    public float moveSpeed = 2f;    // How fast it rises
    private bool isMoving = false;  // Triggered state

    void Update()
    {
        if (isMoving)
        {
            // Move smoothly toward the target height
            float newY = Mathf.MoveTowards(transform.position.y, targetY, moveSpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }

    public void StartRising()
    {
        isMoving = true;
    }
}

