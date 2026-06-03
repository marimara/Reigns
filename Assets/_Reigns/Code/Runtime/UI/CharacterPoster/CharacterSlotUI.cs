using Sirenix.OdinInspector;
using UnityEngine;

public class CharacterSlotUI : MonoBehaviour
{
    [Title("References")]
    [SerializeField] private CharacterPosterUI posterUI;

    [Title("Current")]
    [ShowInInspector, ReadOnly]
    private CharacterData assignedCharacter;

    public CharacterData AssignedCharacter => assignedCharacter;

    public bool IsEmpty => assignedCharacter == null;

    public void SetCharacter(CharacterData character)
    {
        assignedCharacter = character;

        posterUI.SetCharacter(character);
    }

    public void Clear()
    {
        assignedCharacter = null;

        posterUI.Clear();
    }
    public void OnClicked()
    {
        CharacterSelectionPanelUI.Instance.Open(this);
    }
}