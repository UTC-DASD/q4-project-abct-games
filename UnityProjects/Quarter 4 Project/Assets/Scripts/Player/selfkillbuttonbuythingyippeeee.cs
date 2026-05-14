using UnityEngine;
using System.Collections;

public class anakin : MonoBehaviour

{
    public float selfDestructCountdown = .1f;
    private PlayerController playerAbilities;
    public Capitalism capitalism;
    public bool killthemallandmehelpnightmarenightmarenightmare = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        if (killthemallandmehelpnightmarenightmarenightmare == true)
        {
            StartCoroutine(SelfDestruct());
        }
    }
    public void StartSelfDestruct()
    {
        StartCoroutine(SelfDestruct());
    }
    public IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(selfDestructCountdown);

        // Destroy all children first
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        Destroy(gameObject);
    }
}


