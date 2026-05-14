using UnityEngine;
[System.Serializable] // Makes it visible in the inspector
public class Shop
{
    public GameObject cardPrefab;  // Prefab for the card layout
    //public Sprite icon;           // (Optional) Icon sprite to display

    public GameObject prefab;     // Prefab to instantiate when bought (e.g. a buff or item)
    public string name;           // e.g. "+2 casts per round"
    public string description;    // e.g. "Gives you 2 extra casts for the round"

    public int price; // 0 = permanent, >0 = temporary in seconds
}
