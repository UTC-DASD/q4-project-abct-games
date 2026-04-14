using UnityEngine;

public class ElevatorTrigger : MonoBehaviour
{
    public Elevator elevator;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            elevator.StartRising();
            Debug.Log("Active");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        elevator.StopRising();
        Debug.Log("Left");
    }
}
