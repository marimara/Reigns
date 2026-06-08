using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [ShowInInspector, ReadOnly]
    private List<CharacterData> unlockedCharacters =
        new();
    public IReadOnlyList<CharacterData>
        UnlockedCharacters =>
        unlockedCharacters;
    
    public static CharacterManager Instance
    {
        get;
        private set;
    }

    [SerializeField]
    private CharacterDatabase database;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        UnlockStartingCharacters();
    }
    
    public void UnlockCharacter(
        CharacterData character)
    {
        if (character == null)
            return;

        if (unlockedCharacters.Contains(
                character))
            return;

        unlockedCharacters.Add(character);

        Debug.Log(
            $"Character Unlocked: " +
            $"{character.CharacterName}");
    }
    private void UnlockStartingCharacters()
    {
        foreach (CharacterData character
                 in database.Characters)
        {
            if (character.UnlockType !=
                CharacterUnlockType.Starting)
            {
                continue;
            }

            UnlockCharacter(character);
        }
    }
    private void OnEnable()
    {
        TimeManager.OnDayAdvanced +=
            CheckDayUnlocks;
    }

    private void OnDisable()
    {
        TimeManager.OnDayAdvanced -=
            CheckDayUnlocks;
    }
    private void CheckDayUnlocks()
    {
        foreach (CharacterData character
                 in database.Characters)
        {
            if (character.UnlockType !=
                CharacterUnlockType.Day)
            {
                continue;
            }

            if (TimeManager.Instance.CurrentDay <
                character.RequiredDay)
            {
                continue;
            }

            UnlockCharacter(character);
        }
    }
    [Button]
    private void PrintUnlockedCharacters()
    {
        foreach (CharacterData character
                 in unlockedCharacters)
        {
            Debug.Log(
                character.CharacterName);
        }
    }
}