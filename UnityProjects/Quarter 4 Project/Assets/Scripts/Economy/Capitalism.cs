using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class Capitalism : MonoBehaviour
{
    [Header("Shop Data")]
    public CoinScript coinScript;
    public List<Shop> shop = new List<Shop>();

    [Header("UI References")]
    public Transform shopGridParent;
    public TextMeshProUGUI coinBalanceText;

    [Header("Card Styling")]
    public Color affordableColor = Color.grey;
    public Color unaffordableColor = Color.gray;

    //TRYSTAN what did you do. 
    // i broked it

public bool whatisgoingonhelpmehelpmehelpmenightmarenightmarenightmare = true;    private GameObject lastPurchasedObject;    private void Start()
    {
        if (coinScript == null)
            Debug.LogWarning("Capitalism: coinScript is not assigned.");

        if (shopGridParent == null)
            Debug.LogWarning("Capitalism: shopGridParent is not assigned.");

        PopulateShopGrid();
        RefreshUI();
    }

    private void Update()
    {
        RefreshCoinText();
    }

    private void PopulateShopGrid()
    {
        if (shopGridParent == null)
            return;

        foreach (Transform child in shopGridParent)
        {
            Destroy(child.gameObject);
        }

        foreach (Shop item in shop)
        {
            if (item.cardPrefab == null)
            {
                Debug.LogWarning($"Capitalism: Shop item '{item.name}' does not have a cardPrefab assigned.");
                continue;
            }

            GameObject cardInstance = Instantiate(item.cardPrefab, shopGridParent, false);
            ShopCardUI cardUI = cardInstance.GetComponent<ShopCardUI>();

            if (cardUI != null)
            {
                cardUI.Setup(item, PurchaseItem);
                cardUI.SetCardState(IsAffordable(item), affordableColor, unaffordableColor);
            }
            else
            {
                Debug.LogWarning($"Capitalism: cardPrefab for '{item.name}' does not contain a ShopCardUI component.");
            }
        }
    }

    private void RefreshUI()
    {
        RefreshCoinText();
        RefreshShopCards();
    }

    private void RefreshCoinText()
    {
        if (coinBalanceText == null || coinScript == null)
            return;

        coinBalanceText.text = $"Coins: {coinScript.coinAmount}";
    }

    private void RefreshShopCards()
    {
        if (shopGridParent == null)
            return;

        ShopCardUI[] cards = shopGridParent.GetComponentsInChildren<ShopCardUI>();
        foreach (ShopCardUI card in cards)
        {
            if (card.ItemData == null)
                continue;

            bool affordable = IsAffordable(card.ItemData);
            card.SetCardState(affordable, affordableColor, unaffordableColor);
        }
    }

    public bool IsAffordable(Shop item)
    {
        if (coinScript == null || item == null)
            return false;

        return coinScript.coinAmount >= Mathf.RoundToInt(item.price);
    }

    public void PurchaseItem(Shop item)
    {
        if (coinScript == null || item == null)
            return;

        int itemPrice = Mathf.RoundToInt(item.price);
        if (coinScript.coinAmount < itemPrice)
        {
            Debug.Log($"Not enough coins to purchase: {item.name}");
            return;
        }

        coinScript.coinAmount -= itemPrice += 10;
        Debug.Log($"Purchased: {item.name} for {itemPrice} coins.");
        RefreshUI();
        ApplyItemEffect(item);
    }
    public void buyItem(int index)
    {
        if (index >= 0 && index < shop.Count)
        {
            PurchaseItem(shop[index]);
        }
        else
        {
            Debug.LogWarning("Invalid shop item index.");
        }

    }
    private void ApplyItemEffect(Shop item)
    {
        // TODO: apply the purchased item effect in your game logic.
        // For now it only logs a purchase.
        Debug.Log($"Applying shop effect for {item.name}.");
    }
}
