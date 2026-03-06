using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButtonHoverSFX : MonoBehaviour, IPointerEnterHandler
{
    private Selectable _selectable;

    private void Awake()
    {
        _selectable = GetComponent<Selectable>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (UISoundPlayer.Instance == null) return;
        if (_selectable != null && !_selectable.IsInteractable()) return;

        UISoundPlayer.Instance.PlayHover();
    }
}