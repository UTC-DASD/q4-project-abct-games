using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public GameObject CoinSave;

    public int coinAmount;
    public Shop Shop;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        DontDestroyOnLoad(CoinSave);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Buy()
    {
        if (coinAmount >= Shop.price)
        {
            coinAmount -= Shop.price;
        }
    }
    
}

