using UnityEngine;
using UnityEngine.UI;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Helper script to create and set up the Shop Card prefab with all necessary UI components.
/// Use the menu option: Tools > Shop > Create Shop Card Prefab
/// </summary>
public class ShopCardPrefabSetup : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("Tools/Shop/Create Shop Card Prefab")]
    public static void CreateShopCardPrefab()
    {
        // Create root GameObject
        GameObject cardRoot = new GameObject("ShopCard");
        RectTransform cardRect = cardRoot.AddComponent<RectTransform>();
        cardRect.sizeDelta = new Vector2(200, 250);

        // Add background image
        Image backgroundImage = cardRoot.AddComponent<Image>();
        backgroundImage.color = Color.white;

        // Add ShopCardUI component
        ShopCardUI cardUI = cardRoot.AddComponent<ShopCardUI>();

        // Create Icon child
        GameObject iconObj = new GameObject("Icon");
        iconObj.transform.SetParent(cardRoot.transform, false);
        RectTransform iconRect = iconObj.AddComponent<RectTransform>();
        iconRect.anchoredPosition = new Vector2(0, 60);
        iconRect.sizeDelta = new Vector2(80, 80);
        Image iconImage = iconObj.AddComponent<Image>();
        iconImage.color = new Color(0.8f, 0.8f, 0.8f);

        // Create Title text
        GameObject titleObj = new GameObject("Title");
        titleObj.transform.SetParent(cardRoot.transform, false);
        RectTransform titleRect = titleObj.AddComponent<RectTransform>();
        titleRect.anchoredPosition = new Vector2(0, 20);
        titleRect.sizeDelta = new Vector2(180, 40);
        TextMeshProUGUI titleText = titleObj.AddComponent<TextMeshProUGUI>();
        titleText.text = "Item Name";
        titleText.fontSize = 24;
        titleText.alignment = TextAlignmentOptions.Center;

        // Create Description text
        GameObject descObj = new GameObject("Description");
        descObj.transform.SetParent(cardRoot.transform, false);
        RectTransform descRect = descObj.AddComponent<RectTransform>();
        descRect.anchoredPosition = new Vector2(0, -20);
        descRect.sizeDelta = new Vector2(180, 50);
        TextMeshProUGUI descText = descObj.AddComponent<TextMeshProUGUI>();
        descText.text = "Description here";
        descText.fontSize = 14;
        descText.alignment = TextAlignmentOptions.Center;
        descText.wordWrappingRatios = 1;

        // Create Price text
        GameObject priceObj = new GameObject("Price");
        priceObj.transform.SetParent(cardRoot.transform, false);
        RectTransform priceRect = priceObj.AddComponent<RectTransform>();
        priceRect.anchoredPosition = new Vector2(0, -60);
        priceRect.sizeDelta = new Vector2(100, 30);
        TextMeshProUGUI priceText = priceObj.AddComponent<TextMeshProUGUI>();
        priceText.text = "Price: 10";
        priceText.fontSize = 18;
        priceText.alignment = TextAlignmentOptions.Center;

        // Create Buy Button
        GameObject buttonObj = new GameObject("BuyButton");
        buttonObj.transform.SetParent(cardRoot.transform, false);
        RectTransform buttonRect = buttonObj.AddComponent<RectTransform>();
        buttonRect.anchoredPosition = new Vector2(0, -110);
        buttonRect.sizeDelta = new Vector2(160, 40);
        Image buttonImage = buttonObj.AddComponent<Image>();
        buttonImage.color = new Color(0.2f, 0.7f, 0.2f);
        Button button = buttonObj.AddComponent<Button>();

        // Add button text
        GameObject buttonTextObj = new GameObject("Text");
        buttonTextObj.transform.SetParent(buttonObj.transform, false);
        RectTransform buttonTextRect = buttonTextObj.AddComponent<RectTransform>();
        buttonTextRect.anchoredPosition = Vector2.zero;
        buttonTextRect.sizeDelta = new Vector2(160, 40);
        TextMeshProUGUI buttonText = buttonTextObj.AddComponent<TextMeshProUGUI>();
        buttonText.text = "Buy";
        buttonText.fontSize = 18;
        buttonText.alignment = TextAlignmentOptions.Center;

        // Assign UI references to ShopCardUI
        cardUI.iconImage = iconImage;
        cardUI.titleText = titleText;
        cardUI.descriptionText = descText;
        cardUI.priceText = priceText;
        cardUI.buyButton = button;
        cardUI.backgroundImage = backgroundImage;

        // Create and save the prefab
        string prefabPath = "Assets/Prefabs/ShopCard.prefab";
        PrefabUtility.SaveAsPrefabAsset(cardRoot, prefabPath);
        
        // Clean up the temporary GameObject
        DestroyImmediate(cardRoot);

        EditorUtility.DisplayDialog("Success", $"Shop Card prefab created at {prefabPath}", "OK");
        AssetDatabase.Refresh();
    }
#endif
}