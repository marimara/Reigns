using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class CharacterSelectionPanelUI : MonoBehaviour
{
    [Title("References")]
    [SerializeField] private CanvasGroup canvasGroup;
    
    [SerializeField] private CharacterButtonUI characterButtonPrefab;
    [SerializeField] private Transform characterListParent;

    [SerializeField] private CharacterData[] availableCharacters;
    
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

        BuildList();

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
    private void BuildList()
    {
        foreach (Transform child in characterListParent)
        {
            Destroy(child.gameObject);
        }

        foreach (CharacterData character in availableCharacters)
        {
            CharacterButtonUI button =
                Instantiate(characterButtonPrefab,
                    characterListParent);

            button.Setup(character);
        }
    }
    public void SelectCharacter(CharacterData character)
    {
        if (currentSlot == null)
            return;

        currentSlot.SetCharacter(character);

        Close();
    }
}