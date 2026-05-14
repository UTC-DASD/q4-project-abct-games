using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ShopCardUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("UI Elements")]
    public Image iconImage;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI priceText;
    public Button buyButton;
    public Image backgroundImage;

    [Header("Scale Animation")]
    public float hoverScale = 1.05f;
    public float animationDuration = 0.12f;

    public Shop ItemData { get; private set; }

    private Action<Shop> onClick;
    private Vector3 baseScale;
    private Coroutine scaleCoroutine;
    private GameObject instantiatedPrefab;

    private void Awake()
    {
        baseScale = transform.localScale;
        if (buyButton != null)
        {
            buyButton.onClick.RemoveAllListeners();
            buyButton.onClick.AddListener(OnButtonClicked);
        }
    }

    public void Setup(Shop item, Action<Shop> onClickCallback)
    {
        ItemData = item;
        onClick = onClickCallback;

        // Handle prefab or icon display
        if (item.prefab != null)
        {
            // Instantiate prefab instead of showing icon
            if (instantiatedPrefab != null)
                Destroy(instantiatedPrefab);

            instantiatedPrefab = Instantiate(item.prefab, iconImage.transform.parent);
            instantiatedPrefab.transform.localPosition = Vector3.zero;
            instantiatedPrefab.transform.localScale = Vector3.one;

            if (iconImage != null)
                iconImage.gameObject.SetActive(false);
        }
        else
        {
            // Use icon sprite
            if (instantiatedPrefab != null)
            {
                Destroy(instantiatedPrefab);
                instantiatedPrefab = null;
            }

          //  if (iconImage != null)
            //{
            //    iconImage.sprite = item.icon;
            //    iconImage.gameObject.SetActive(item.icon != null);
           // }
        }

        if (titleText != null)
            titleText.text = item.name;

        if (descriptionText != null)
            descriptionText.text = item.description;

        if (priceText != null)
            priceText.text = $"Price: {Mathf.RoundToInt(item.price)}";
    }

    public void SetCardState(bool canBuy, Color affordableColor, Color unaffordableColor)
    {
        if (buyButton != null)
            buyButton.interactable = canBuy;
        backgroundImage.color = canBuy ? affordableColor : unaffordableColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AnimateScale(baseScale * hoverScale);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        AnimateScale(baseScale);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        InvokeClick();
    }

    private void OnButtonClicked()
    {
        InvokeClick();
    }

    private void InvokeClick()
    {
        if (ItemData == null)
            return;

        onClick?.Invoke(ItemData);
    }

    private void AnimateScale(Vector3 targetScale)
    {
        if (scaleCoroutine != null)
            StopCoroutine(scaleCoroutine);

        scaleCoroutine = StartCoroutine(ScaleRoutine(targetScale));
    }

    private IEnumerator ScaleRoutine(Vector3 targetScale)
    {
        Vector3 startScale = transform.localScale;
        float time = 0f;

        while (time < animationDuration)
        {
            time += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, targetScale, time / animationDuration);
            yield return null;
        }

        transform.localScale = targetScale;
    }
}
