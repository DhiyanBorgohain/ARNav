# HUDController.cs
```csharp
using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    [Header("References")]
    public Camera arCamera;                       // AR camera rendering the scene
    public RectTransform arrowUI;                 // UI element for turn arrow
    public TextMeshProUGUI etaText;               // UI text for ETA
    public TextMeshProUGUI turnDistanceText;      // UI text for turn distance

    // Internal state
    private Vector3 nextTurnWorldPosition = Vector3.zero;
    private float etaSeconds = 0f;
    private float nextTurnDistance = 0f;

    void Update()
    {
        // Position the arrow UI over the world location of the next turn
        if (nextTurnWorldPosition != Vector3.zero)
        {
            Vector3 screenPos = arCamera.WorldToScreenPoint(nextTurnWorldPosition);
            arrowUI.position = screenPos;
        }

        // Update text fields
        etaText.text = $"ETA: {FormatTime(etaSeconds)}";
        turnDistanceText.text = $"{nextTurnDistance:F1} m to next turn";
    }

    /// <summary>
    /// Called by the navigation provider to update HUD data.
    /// </summary>
    public void SetNavigationData(Vector3 worldPos, float eta, float distance)
    {
        nextTurnWorldPosition = worldPos;
        etaSeconds = eta;
        nextTurnDistance = distance;
    }

    /// <summary>
    /// Formats seconds into mm:ss format.
    /// </summary>
    private string FormatTime(float totalSeconds)
    {
        int mins = Mathf.FloorToInt(totalSeconds / 60f);
        int secs = Mathf.FloorToInt(totalSeconds % 60f);
        return $"{mins:D2}:{secs:D2}";
    }
}
