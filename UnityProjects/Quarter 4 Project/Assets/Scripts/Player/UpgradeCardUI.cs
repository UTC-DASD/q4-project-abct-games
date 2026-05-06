using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class UpgradeCardUI : MonoBehaviour
{
    [Header("Highlight")]
    public Image highlightBorder;   // Assign in prefab
    public float highlightScale = 1.1f;

    public Upgrade UpgradeData { get; private set; }

    private Vector3 originalScale;
    private System.Action<Upgrade> clickCallback;
public Upgrade directUpgrade; // assign in prefab
    void Awake()
    {
        originalScale = transform.localScale;

        if (highlightBorder != null)
            highlightBorder.enabled = false;
    }

   public void Setup(Upgrade runtimeUpgrade, System.Action<Upgrade> onClick)
{
    clickCallback = onClick;

    // If prefab has a direct upgrade, use that
    UpgradeData = directUpgrade != null ? directUpgrade : runtimeUpgrade;
}

   public void SetHighlighted(bool state)
{
    StopAllCoroutines();
    StartCoroutine(AnimateHighlight(state));
}

IEnumerator AnimateHighlight(bool state)
{
    if (highlightBorder != null)
        highlightBorder.enabled = state;

    float duration = 0.15f;
    float time = 0f;

    Vector3 start = transform.localScale;
    Vector3 target = state 
        ? originalScale * highlightScale 
        : originalScale;

    while (time < duration)
    {
        time += Time.deltaTime;
        transform.localScale = Vector3.Lerp(start, target, time / duration);
        yield return null;
    }

    transform.localScale = target;
}
    public void OnClick()
    {
        Debug.Log("Andrew, is it working?");
        clickCallback?.Invoke(UpgradeData);
    }

    public void Update () {
        if (Input.GetMouseButtonDown(0)) {
            OnClick();
        }
    }
}