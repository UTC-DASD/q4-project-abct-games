using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float delayBeforeStart = 0f; // Time in seconds before rotation starts
    public float xRotation = 0f;
    public float yRotation = 30f;
    public float zRotation = 0f;
    // Update is called once per frame
    void Update()
    {
        if (delayBeforeStart > 0)
        {
            delayBeforeStart -= Time.deltaTime; // Countdown the delay
            return; // Exit the method until the delay has elapsed
        }
        transform.Rotate(new Vector3(xRotation, yRotation, zRotation) * Time.deltaTime);
    }
}
