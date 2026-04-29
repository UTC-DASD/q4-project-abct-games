using UnityEngine;

public enum UpgradeType
{
    Good,
    Bad
}

public enum StatType
{
    healthMod,
    damageMod,
    currencyDropRate,
    healRate
}

[System.Serializable] // Makes it visible in the inspector
public class Upgrade
{
    public string name;           // e.g. "+2 casts per round"
    public string description;    // e.g. "Gives you 2 extra casts for the round"
    public UpgradeType type;      // Good or Bad
    public StatType statToModify; // Which stat this affects
    public float value;           // +2, -1, 0.5, etc.
    public float duration = 0f; // 0 = permanent, >0 = temporary in seconds
}