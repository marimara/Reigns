using System;
using Sirenix.OdinInspector;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    public static event Action OnDayAdvanced;

    [Title("Time")]
    [ShowInInspector, ReadOnly]
    public int CurrentDay { get; private set; } = 1;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        SyncDialogueVariables();
    }

    [Button(ButtonSizes.Large)]
    public void AdvanceDay()
    {
        CurrentDay++;

        SyncDialogueVariables();

        Debug.Log($"Advanced to Day {CurrentDay}");

        OnDayAdvanced?.Invoke();
    }
    private void SyncDialogueVariables()
    {
        DialogueLua.SetVariable("Actual_Day", CurrentDay);
    }
}