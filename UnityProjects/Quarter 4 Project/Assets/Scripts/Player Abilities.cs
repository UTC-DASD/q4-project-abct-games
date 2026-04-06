using System.Data.Common;
using NUnit.Framework;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    public PlayerController PlayerController;
    public GameObject platformPrefab;

     void Start()
    {
        PlayerController = GetComponent<PlayerController>();
    }
    // Update is called once per frame
    void Update()
    {
     if (PlayerController.isGrounded == false && Input.GetKeyDown(KeyCode.S))
        {
            Instantiate(platformPrefab, transform.position + new Vector3(0, -1, 0), Quaternion.identity);
        }  
    }
}
