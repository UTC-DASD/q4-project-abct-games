using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Health healthCheck;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(healthCheck.currentHealth <= 0)
        {
            SceneManager.LoadScene(5);
        }
    }
}
