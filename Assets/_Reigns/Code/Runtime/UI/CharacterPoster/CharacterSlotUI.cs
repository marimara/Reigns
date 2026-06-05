using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class CharacterSlotUI : MonoBehaviour
{
    public static Action OnCharacterChanged;
    
    [Title("References")]
    [SerializeField] private CharacterPosterUI posterUI;

    [Title("Current")]
    [ShowInInspector, ReadOnly]
    private CharacterData assignedCharacter;

    public CharacterData AssignedCharacter => assignedCharacter;

    public bool IsEmpty => assignedCharacter == null;

    public void SetCharacter(
        CharacterData character)
    {
        assignedCharacter = character;

        posterUI.SetCharacter(character);

        OnCharacterChanged?.Invoke();
    }

    public void Clear()
    {
        assignedCharacter = null;

        posterUI.Clear();

        OnCharacterChanged?.Invoke();
    }
    public void OnClicked()
    {
        CharacterSelectionPanelUI.Instance.Open(this);
    }
}