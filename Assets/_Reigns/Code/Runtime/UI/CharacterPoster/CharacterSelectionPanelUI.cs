using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class CharacterSelectionPanelUI : MonoBehaviour
{
    [Title("References")]
    [SerializeField] private CanvasGroup canvasGroup;
    
    public static CharacterSelectionPanelUI Instance { get; private set; }

    [ShowInInspector, ReadOnly]
    private CharacterSlotUI currentSlot;

    private void Awake()
    {
        Instance = this;

        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void Open(CharacterSlotUI slot)
    {
        currentSlot = slot;

        canvasGroup.DOKill();

        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        canvasGroup.DOFade(1f, 0.25f);
    }

    public void Close()
    {
        currentSlot = null;

        canvasGroup.DOKill();

        canvasGroup.DOFade(0f, 0.25f)
            .OnComplete(() =>
            {
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            });
    }
}