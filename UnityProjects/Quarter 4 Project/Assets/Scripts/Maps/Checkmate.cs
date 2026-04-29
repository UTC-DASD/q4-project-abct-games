using UnityEngine;
using UnityEngine.SceneManagement;
public class Checkmate : MonoBehaviour
{
    public GameObject king;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(king == null)
        {
            SceneManager.LoadScene(4);
        }
    }
}
