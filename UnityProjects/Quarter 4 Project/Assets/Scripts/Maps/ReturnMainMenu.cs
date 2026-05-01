using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class ReturnMainMenu : MonoBehaviour
{
    public GameObject card;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.escapeKey.IsPressed())
        {
            card.SetActive(true);
            StartCoroutine(ReturnMain());
        }
    }

    private IEnumerator ReturnMain()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
        Debug.Log("return");
    }
}
