using UnityEngine;
using UnityEngine.UI;

public class UIButtonHoverAutoBinder : MonoBehaviour
{
    [Tooltip("Also add hover SFX to inactive buttons.")]
    public bool includeInactive = true;

    private void Start()
    {
        BindAllButtons();
    }

    public void BindAllButtons()
    {
        var buttons = FindObjectsOfType<Button>(includeInactive);
        int added = 0;

        foreach (var b in buttons)
        {
            if (b == null) continue;

            // Add hover script if missing
            if (b.GetComponent<UIButtonHoverSFX>() == null)
            {
                b.gameObject.AddComponent<UIButtonHoverSFX>();
                added++;
            }
        }

        Debug.Log($"UIButtonHoverAutoBinder: Added hover SFX to {added} buttons.");
    }
}