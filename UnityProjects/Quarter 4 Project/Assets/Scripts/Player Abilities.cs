using System.Data.Common;
using NUnit.Framework;
using UnityEngine;
using System.Collections;

public class PlayerAbilities : MonoBehaviour
{
    public PlayerController PlayerController;
    public GameObject platformPrefab;
    public float canCreatePlatform = 0;

     void Start()
    {
        PlayerController = GetComponent<PlayerController>();
    }
    // Update is called once per frame
    void Update()
    {
    
        if (PlayerController != null && PlayerController.isGrounded == false && Input.GetKeyDown(KeyCode.S) && canCreatePlatform >= 1)
        {
            StartCoroutine(SpawnPlatformRoutine());
            PlayerController.canMove = false;
        }
    }
    private IEnumerator SpawnPlatformRoutine()
    {
        GameObject spawned = Instantiate(platformPrefab, transform.position + new Vector3(0, -1, 0), Quaternion.identity);
        yield return new WaitForSecondsRealtime(3f);
        if (spawned != null)
        {
            Destroy(spawned);
        }
    }
    public void DestroyPlatform()
    {
        Destroy(spawned)
    }
}
