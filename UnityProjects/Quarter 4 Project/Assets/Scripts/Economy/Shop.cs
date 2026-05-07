using UnityEngine;
[System.Serializable] // Makes it visible in the inspector
public class Shop
{
    public string name;           // e.g. "+2 casts per round"
    public string description;    // e.g. "Gives you 2 extra casts for the round"

    public float price; // 0 = permanent, >0 = temporary in seconds
}
