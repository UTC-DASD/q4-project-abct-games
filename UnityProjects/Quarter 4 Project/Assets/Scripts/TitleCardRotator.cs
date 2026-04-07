using UnityEngine;

public class CardFlip : MonoBehaviour
{
    public float flipSpeed = 5f;
    private bool flipping = false;
    private float targetRotation = 0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // trigger flip
        {
            Flip();
        }

        if (flipping)
        {
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.Euler(0, targetRotation, 0),
                Time.deltaTime * flipSpeed
            );

            if (Quaternion.Angle(transform.rotation, Quaternion.Euler(0, targetRotation, 0)) < 0.1f)
            {
                flipping = false;
            }
        }
    }

    public void Flip()
    {
        flipping = true;

        if (targetRotation == 0f)
            targetRotation = 180f;
        else
            targetRotation = 0f;
    }
}