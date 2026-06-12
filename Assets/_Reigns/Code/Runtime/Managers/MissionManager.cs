using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance
    {
        get;
        private set;
    }

    [Title("Database")]
    [SerializeField]
    private MissionDatabase missionDatabase;

    [ShowInInspector, ReadOnly]
    private List<MissionData> availableMissions = new();
    [Title("History")]
    [ShowInInspector, ReadOnly]
    private List<MissionData> completedStoryMissions
        = new();
    [Title("Active")]
    [ShowInInspector, ReadOnly]
    private List<MissionData> activeMissionTypes
        = new();

    public IReadOnlyList<MissionData>
        AvailableMissions => availableMissions;
    public IReadOnlyList<MissionData>
        CompletedStoryMissions
        => completedStoryMissions;
    
    public bool HasCompletedMission(
        MissionData mission)
    {
        if (mission == null)
            return false;

        return completedStoryMissions
            .Contains(mission);
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    private void Start()
    {
        RefreshAvailableMissions();
    }
    [Button]
    public void RefreshAvailableMissions()
    {
        availableMissions.Clear();

        foreach (MissionData mission
                 in missionDatabase.Missions)
        {
            Debug.Log(
                $"{mission.MissionName} | " +
                $"Day:{TimeManager.Instance.CurrentDay} | " +
                $"MinDay:{mission.MinDay}");
            
            if (TimeManager.Instance.CurrentDay <
                mission.MinDay)
                continue;
            
            if (mission.RequiredMission != null &&
                !HasCompletedMission(
                    mission.RequiredMission))
            {
                continue;
            }
            
            if (!mission.Repeatable &&
                completedStoryMissions.Contains(mission))
                continue;
            
            if (activeMissionTypes.Contains(mission))
                continue;
            
            Debug.Log(
                $"AVAILABLE: {mission.MissionName}");

            availableMissions.Add(mission);
        }
    }
    private void OnEnable()
    {
        TimeManager.OnDayAdvanced += HandleDayAdvanced;
    }

    private void OnDisable()
    {
        TimeManager.OnDayAdvanced -= HandleDayAdvanced;
    }
    private void HandleDayAdvanced()
    {
        RefreshAvailableMissions();
    }
    public void RegisterCompletedMission(
        MissionData mission)
    {
        if (mission == null)
            return;

        if (mission.Repeatable)
            return;

        if (completedStoryMissions.Contains(mission))
            return;

        completedStoryMissions.Add(mission);

        RefreshAvailableMissions();
    }
    public void RegisterActiveMission(
        MissionData mission)
    {
        if (mission == null)
            return;

        if (activeMissionTypes.Contains(mission))
            return;

        activeMissionTypes.Add(mission);

        RefreshAvailableMissions();
    }
    public void UnregisterActiveMission(
        MissionData mission)
    {
        if (mission == null)
            return;

        activeMissionTypes.Remove(mission);

        RefreshAvailableMissions();
    }
}