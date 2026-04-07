using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class CardFlipper : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite frontSprite;
    public Sprite backSprite;
    
    [Header("Settings")]
    public float duration = 2f;
    public float maxDepth = 0.2f;
    private bool isFaceUp = true;
    private SpriteRenderer spriteRenderer;
    private Vector3 initialScale;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialScale = transform.localScale;
        // Start with the front sprite
        spriteRenderer.sprite = frontSprite;
        FlipCard(); // Start flipping immediately
    }
    
    
        
    
    // Call this method from a button click or other event
    public void FlipCard()
    {
        StopAllCoroutines(); // Prevent overlapping flips
        StartCoroutine(FlipRoutine());
    }

    private IEnumerator FlipRoutine()
    {
        float totalRotation = 0f;
        float halfDuration = duration * 0.5f;
        
        while (true)
        {
            // Rotate from front face to edge
            float elapsed = 0f;
            float startRotation = totalRotation;
            while (elapsed < halfDuration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / halfDuration;
                totalRotation = startRotation + Mathf.Lerp(0, 90, t);
                transform.rotation = Quaternion.Euler(0, totalRotation, 0);
                ApplyDepth(totalRotation);
                yield return null;
            }
            
            // Swap to back sprite at the halfway point
            spriteRenderer.sprite = backSprite;
            
            // Rotate to the back-facing position
            elapsed = 0f;
            startRotation = totalRotation;
            while (elapsed < halfDuration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / halfDuration;
                totalRotation = startRotation + Mathf.Lerp(0, 90, t);
                transform.rotation = Quaternion.Euler(0, totalRotation, 0);
                ApplyDepth(totalRotation);
                yield return null;
            }
            
            // Continue rotating while back sprite is visible
            elapsed = 0f;
            startRotation = totalRotation;
            while (elapsed < halfDuration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / halfDuration;
                totalRotation = startRotation + Mathf.Lerp(0, 90, t);
                transform.rotation = Quaternion.Euler(0, totalRotation, 0);
                ApplyDepth(totalRotation);
                yield return null;
            }
            
            // Swap back to front sprite before the final quarter-turn
            spriteRenderer.sprite = frontSprite;
            
            // Finish the rotation back to the front-facing position
            elapsed = 0f;
            startRotation = totalRotation;
            while (elapsed < halfDuration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / halfDuration;
                totalRotation = startRotation + Mathf.Lerp(0, 90, t);
                transform.rotation = Quaternion.Euler(0, totalRotation, 0);
                ApplyDepth(totalRotation);
                yield return null;
            }
            
            totalRotation %= 360f;
        }
    }

    private void ApplyDepth(float rotationDegrees)
    {
        float depthFactor = Mathf.Abs(Mathf.Cos(rotationDegrees * Mathf.Deg2Rad));
        float widthScale = Mathf.Lerp(1f - maxDepth, 1f, depthFactor);
        transform.localScale = new Vector3(initialScale.x * widthScale, initialScale.y, initialScale.z);
    }
}