using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSelectionManager : MonoBehaviour
{
    [Header("Card Setup")]
    public Transform cardParent;

    // 👇 LIST OF DIFFERENT CARD PREFABS
    public List<GameObject> possibleCardPrefabs;

    [Header("Cycle Settings")]
    public float cycleTime = 1f;

    private List<UpgradeCardUI> activeCards = new List<UpgradeCardUI>();
    private int currentHighlight = 0;
    private bool selectionMade = false;

    private System.Action onSelectionComplete;
    private Coroutine cycleRoutine;

    public void StartSelection(List<Upgrade> upgrades, System.Action onComplete)
    {
        selectionMade = false;
        onSelectionComplete = onComplete;
        currentHighlight = 0;

        ClearCards();

        foreach (Upgrade up in upgrades)
        {
            // Pick random card prefab style
            GameObject prefab = possibleCardPrefabs[
                Random.Range(0, possibleCardPrefabs.Count)
            ];

            GameObject cardGO = Instantiate(prefab, cardParent);
            UpgradeCardUI cardUI = cardGO.GetComponent<UpgradeCardUI>();

            cardUI.Setup(up, OnCardClicked);
            cardUI.SetHighlighted(false);

            activeCards.Add(cardUI);
        }

        if (cycleRoutine != null)
            StopCoroutine(cycleRoutine);

        cycleRoutine = StartCoroutine(AutoCycleCards());
    }

    IEnumerator AutoCycleCards()
    {
        while (!selectionMade && activeCards.Count > 0)
        {
            foreach (var card in activeCards)
                card.SetHighlighted(false);

            activeCards[currentHighlight].SetHighlighted(true);

            yield return new WaitForSeconds(cycleTime);

            currentHighlight = (currentHighlight + 1) % activeCards.Count;
        }
    }

    void OnCardClicked(Upgrade chosen)
    {
        if (selectionMade)
            return;

        if (activeCards[currentHighlight].UpgradeData != chosen)
            return;

        selectionMade = true;

        if (cycleRoutine != null)
            StopCoroutine(cycleRoutine);

        //UpgradeManager upgradeManager = FindObjectOfType<UpgradeManager>();
        //if (upgradeManager != null)
          //  upgradeManager.ApplyUpgrade(chosen);

        ClearCards();
        onSelectionComplete?.Invoke();
    }

    void ClearCards()
    {
        Debug.Log("Andrew it's working!");

        int childCount = cardParent.transform.childCount;
        for (int j = 0; j < childCount; j++) 
        {
            Destroy(cardParent.transform.GetChild(j).gameObject);
        }

        activeCards.Clear();
    }
}