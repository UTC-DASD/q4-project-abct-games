using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopCardUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("UI Elements")]
    public Image iconImage;
    public Text titleText;
    public Text descriptionText;
    public Text priceText;
    public Button buyButton;
    public Image backgroundImage;

    [Header("Scale Animation")]
    public float hoverScale = 1.05f;
    public float animationDuration = 0.12f;

    public Shop ItemData { get; private set; }

    private Action<Shop> onClick;
    private Vector3 baseScale;
    private Coroutine scaleCoroutine;

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

        if (iconImage != null)
        {
            iconImage.sprite = item.icon;
            iconImage.gameObject.SetActive(item.icon != null);
        }

        if (titleText != null)
            titleText.text = item.name;

        if (descriptionText != null)
            descriptionText.text = item.description;

        if (priceText != null)
            priceText.text = $"{Mathf.RoundToInt(item.price)}";
    }

    public void SetCardState(bool canBuy, Color affordableColor, Color unaffordableColor)
    {
        if (buyButton != null)
            buyButton.interactable = canBuy;

        if (backgroundImage != null)
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
