using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    [SerializeField]
    private List<StoryEvent> storyEvents;

    private void Awake()
    {
        Instance = this;
    }

    public void CheckLocationEvents(LocationID locationID)
    {
        storyEvents.Sort(
            (a, b) => b.Priority.CompareTo(a.Priority)
        );
        foreach (var storyEvent in storyEvents)
        {
            if (storyEvent.LocationID != locationID)
                continue;
            if (storyEvent.PlayOnlyOnce)
            {
                bool alreadyPlayed =
                    DialogueLua.GetVariable(
                        $"Event_{storyEvent.EventID}"
                    ).asBool;

                if (alreadyPlayed)
                    continue;
            }
            if (storyEvent.RequireQuest)
            {
                var state =
                    QuestLog.GetQuestState(storyEvent.QuestName);

                if (state != storyEvent.RequiredQuestState)
                    continue;
            }
            
            if (storyEvent.PlayOnlyOnce)
            {
                DialogueLua.SetVariable(
                    $"Event_{storyEvent.EventID}",
                    true
                );
            }
            if (storyEvent.RequireDay)
            {
                if (TimeManager.Instance.CurrentDay
                    != storyEvent.RequiredDay)
                    continue;
            }
            DialogueManager.StartConversation(
                storyEvent.ConversationTitle
            );
            return;
        }
    }
    [Button]
    public void TestGuildHall()
    {
        CheckLocationEvents(LocationID.GuildHall_Bar);
    }
}