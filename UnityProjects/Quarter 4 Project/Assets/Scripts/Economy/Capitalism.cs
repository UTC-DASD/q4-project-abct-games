using System.Collections.Generic;
using UnityEditor.Purchasing;
using UnityEngine;

public class Capitalism : MonoBehaviour
{
    public CoinScript coinScript; // Reference to the CoinScript
    public List<Shop> shop = new List<Shop>(); // List of shop items
    public int coinbal;
    public float projectile;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    coinbal = coinScript.coinAmount; // Initialize coin balance from CoinScript
   projectile = GetComponent<Shop>().price;// Update shop items based on initial coin balance
    }

    // Update is called once per frame
    void Update()
    {
        if (coinbal < projectile)
        {
            throw new System.Exception("Not enough coins to purchase this item.");

        }
        if (coinbal >= projectile && Input.GetMouseButtonDown(0))
        {
          PurchaseHighlightedItem();
        }
    }   
    public void PurchaseHighlightedItem()
    {
        // Assuming the first item in the shop list is the highlighted item
        Shop highlightedItem = shop[0]; // You can implement logic to determine which item is highlighted

        if (coinbal >= highlightedItem.price)
        {
            coinbal -= (int)highlightedItem.price; // Deduct the price from the coin balance
            Debug.Log("Purchased: " + highlightedItem.name);
            // Implement additional logic for applying the item's effects here
        }
        else
        {
            Debug.Log("Not enough coins to purchase: " + highlightedItem.name);
        }
    }
}
