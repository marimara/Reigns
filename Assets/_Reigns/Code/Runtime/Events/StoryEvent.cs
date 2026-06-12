using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using PixelCrushers.DialogueSystem;

[CreateAssetMenu(menuName = "Reigns/Story Event")]
public class StoryEvent : ScriptableObject
{
    [ReadOnly] [SerializeField] private string eventID;
    public string EventID => eventID;
    
    [SerializeField]private DialogueDatabase Database;
    
    public LocationID LocationID;
    
    [ValueDropdown(nameof(GetConversations))]
    public string ConversationTitle;

    public int Priority;

    public bool PlayOnlyOnce = true;
    
    [Title("Day Requirements")]

    public bool RequireDay;

    [ShowIf(nameof(RequireDay))]
    [MinValue(1)]
    public int RequiredDay = 1;
    
    [Title("Quest Requirements")]

    public bool RequireQuest;
    
    [ShowIf(nameof(RequireQuest))]
    [ValueDropdown(nameof(GetQuests))]
    public string QuestName;
    
    [ShowIf(nameof(RequireQuest))]
    public QuestState RequiredQuestState;
    
    
    private IEnumerable<string> GetConversations()
    {
        if (Database == null)
            yield break;

        foreach (var conversation in Database.conversations)
        {
            yield return conversation.Title;
        }
    }
    private void GenerateEventID()
    {
        if (string.IsNullOrWhiteSpace(ConversationTitle))
        {
            eventID = string.Empty;
            return;
        }

        string cleanConversation =
            ConversationTitle.Replace(" ", "");

        eventID =
            $"{LocationID}_{cleanConversation}";
    }
    [Button]
    private void RegenerateEventID()
    {
        GenerateEventID();
    }
    private IEnumerable<string> GetQuests()
    {
        if (Database == null)
            yield break;

        foreach (var item in Database.items)
        {
            if (item.IsItem)
                continue;

            yield return item.Name;
        }
    }
}

public enum LocationID
{
    GuildHall,
    GuildHall_Bar,
    GuildHall_MissionBoard,
    Guild_Entrance,
    Dormitory
}