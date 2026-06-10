using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverHighlight :
    MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler
{
    public Image highlight;

    public void OnPointerEnter(PointerEventData eventData)
    {
        var c = highlight.color;
        c.a = 1f;
        highlight.color = c;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        var c = highlight.color;
        c.a = 0f;
        highlight.color = c;
    }
}