using UnityEngine;

public class Rotator : MonoBehaviour
{
   public float xRotation = 0f;
   public float yRotation = 30f;
   public float zRotation = 0f;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate (new Vector3 (xRotation ,yRotation, zRotation) * Time.deltaTime);
    }
}
