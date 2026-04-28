using System.Data.Common;
using NUnit.Framework;
using UnityEngine;
using System.Collections;

public class PlayerAbilities : MonoBehaviour
{
    public PlayerController PlayerController;
    public GameObject platformPrefab;
    public float canCreatePlatform = 0;
    public float platformDestroyDelay = 3f;

     void Start()
    {
        PlayerController = GetComponent<PlayerController>();
    }
    // Update is called once per frame
    void Update()
    {
    
        if (PlayerController != null && PlayerController.isGrounded == false && Input.GetKeyDown(KeyCode.S) && canCreatePlatform >= 1)
        {
            Instantiate(platformPrefab, transform.position + new Vector3(0, -1, 0), Quaternion.identity);
            PlayerController.canMove = false;
        }
    }
  
}
