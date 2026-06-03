using System;
using Sirenix.OdinInspector;
using UnityEngine;

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
    }

    [Button(ButtonSizes.Large)]
    public void AdvanceDay()
    {
        CurrentDay++;

        Debug.Log(
            $"Advanced to Day {CurrentDay}");

        OnDayAdvanced?.Invoke();
    }
}