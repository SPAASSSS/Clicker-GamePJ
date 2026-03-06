using UnityEngine;
using UnityEngine.InputSystem;


public class GlobalClickSFX : MonoBehaviour
{
    [Tooltip("If true, won't play click when game is paused.")]
    public bool ignoreWhenPaused = false;

    private void Update()
    {
        if (ignoreWhenPaused && GameManager.Instance != null && GameManager.Instance.IsPaused)
            return;

        if (IsGlobalClickDown())
        {
            if (UISoundPlayer.Instance != null)
                UISoundPlayer.Instance.PlayClick();
        }
    }

    private bool IsGlobalClickDown()
    {
        if (Input.GetMouseButtonDown(0)) return true;

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            return true;

        return false;
    }
}