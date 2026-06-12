using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
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
        CheckQuestUnlocks();
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
        GuildManager.OnMissionCompleted +=
            CheckMissionUnlocks;
    }

    private void OnDisable()
    {
        TimeManager.OnDayAdvanced -=
            CheckDayUnlocks;
        GuildManager.OnMissionCompleted -=
            CheckMissionUnlocks;
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
    private void CheckMissionUnlocks(
        MissionData completedMission)
    {
        foreach (CharacterData character
                 in database.Characters)
        {
            if (character.UnlockType !=
                CharacterUnlockType.Mission)
            {
                continue;
            }

            if (character.RequiredMission !=
                completedMission)
            {
                continue;
            }

            UnlockCharacter(character);
        }
    }
    private void CheckQuestUnlocks()
    {
        foreach (CharacterData character
                 in database.Characters)
        {
            if (character.UnlockType !=
                CharacterUnlockType.QuestState)
            {
                continue;
            }

            var state =
                QuestLog.GetQuestState(
                    character.RequiredQuest);

            if (state !=
                character.RequiredQuestState)
            {
                continue;
            }

            UnlockCharacter(character);
        }
    }
    private void OnQuestStateChange(
        string questName)
    {
        CheckQuestUnlocks();
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