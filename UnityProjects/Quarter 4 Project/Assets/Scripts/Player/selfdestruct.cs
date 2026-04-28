using UnityEngine;
using System.Collections;

public class selfdestruct : MonoBehaviour
{
    public float selfDestructCountdown = 15f;
    private PlayerController playerAbilities;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerAbilities = FindAnyObjectByType<PlayerController>();
        StartCoroutine(SelfDestruct());
    }
    void Update()
    {
         if (tag=="Platform")
        {
            
            selfDestructCountdown = playerAbilities.platformDestroyDelay;
            Debug.Log("Platform tag detected, countdown = " + selfDestructCountdown);
        }
        
    }
    private IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(selfDestructCountdown);
        Destroy(gameObject);
    }
}

  
