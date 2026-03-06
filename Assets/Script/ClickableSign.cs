using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableSign : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private FactoryLine line;

    public void SetLine(FactoryLine l) => line = l;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (line != null)
            line.ManualClick();
    }
}