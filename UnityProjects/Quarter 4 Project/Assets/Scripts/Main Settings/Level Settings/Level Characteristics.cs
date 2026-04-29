using UnityEngine;

public class LevelCharacteristics : MonoBehaviour
{
    public bool Reversed;
    public float healthMod = 1f;
    public float damageMod = 1f;
    public float currencyDropRate = 1f;
    public float healRate = 1f;
public void ApplyUpgrade(Upgrade up)
{
    ApplyStat(up, true);

    if (up.duration > 0)
    {
        StartCoroutine(RemoveTemporaryUpgrade(up));
    }
}
    public void ApplyStat(Upgrade up, bool apply)
    {
        if (Reversed != true)
        {
        float val = apply ? up.value : -up.value;

        switch (up.statToModify)
        {
            case StatType.healthMod:
                healthMod += val;
                break;

            case StatType.damageMod:
                damageMod += val;
                break;

            case StatType.currencyDropRate:
                currencyDropRate += val;
                break;

            case StatType.healRate:
                healRate += val;
                break;
             
        }
        }
        if (Reversed == true)
        {
            float val = apply ? up.value : -up.value;

            switch (up.statToModify)
            {
                case StatType.healthMod:
                    healthMod -= val;
                    break;

                case StatType.damageMod:
                    damageMod -= val;
                    break;

                case StatType.currencyDropRate:
                    currencyDropRate -= val;
                    break;

                case StatType.healRate:
                    healRate -= val;
                    break;

            }
        }
    }
    public IEnumerator RemoveTemporaryUpgrade(Upgrade up)
    {
        yield return new WaitForSeconds(up.duration);
        ApplyStat(up, false);
        Debug.Log($"Temporary upgrade {up.name} has expired");
    }

    // Update is called once per frame

}
