using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using DG.Tweening;


public class GuildManager : MonoBehaviour
{
    public static GuildManager Instance { get; private set; }

    [ShowInInspector, ReadOnly]
    public int Gold { get; private set; }
    
    [Button]
    private void Add100Gold()
    {
        Gold += 100;
    }

    [Title("Active Missions")]
    [ShowInInspector, ReadOnly] private List<MissionAssignment> activeMissions = new();
    
    [Title("Completed Missions")]
    [ShowInInspector, ReadOnly] private List<MissionAssignment> completedMissions = new();
    
    [Title("Conditions")]
    [SerializeField] private ConditionDatabase conditionDatabase;

    public IReadOnlyList<MissionAssignment> ActiveMissions
        => activeMissions;
    public IReadOnlyList<MissionAssignment> CompletedMissions
        => completedMissions;
    
    public MissionAssignment ConsumeCompletedMission()
    {
        if (completedMissions.Count == 0)
            return null;

        MissionAssignment mission =
            completedMissions[0];

        completedMissions.RemoveAt(0);

        return mission;
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
    private void OnEnable()
    {
        TimeManager.OnDayAdvanced += HandleDayAdvanced;
    }

    private void OnDisable()
    {
        TimeManager.OnDayAdvanced -= HandleDayAdvanced;
    }
    
    public void AddMission(
        MissionAssignment assignment)
    {
        activeMissions.Add(assignment);

        MissionManager.Instance
            .RegisterActiveMission(
                assignment.Mission);
    }
    public bool IsCharacterBusy(
        CharacterData character)
    {
        foreach (MissionAssignment mission in activeMissions)
        {
            if (mission.CharacterA == character)
                return true;

            if (mission.CharacterB == character)
                return true;
        }

        return false;
    }
    private ConditionData GetRandomCondition(
        MissionType missionType,
        bool positive)
    {
        List<ConditionData> valid =
            new();

        foreach (ConditionData condition
                 in conditionDatabase.Conditions)
        {
            if (condition.MissionType != missionType)
                continue;

            if (condition.IsPositive != positive)
                continue;

            valid.Add(condition);
        }

        if (valid.Count == 0)
            return null;

        return valid[
            Random.Range(
                0,
                valid.Count)];
    }
    private void ResolveMission(
        MissionAssignment mission)
    {
        float roll = Random.value;

        mission.WasSuccessful =
            roll <= mission.SuccessChance;

        mission.Finished = true;

        mission.GoldEarned =
            mission.WasSuccessful
                ? mission.Mission.GoldReward
                : 0;
        
        mission.GrantedCondition =
            GetRandomCondition(
                mission.Mission.MissionType,
                mission.WasSuccessful);
        
        if (mission.GrantedCondition != null)
        {
            CharacterConditionManager
                .Instance
                .AddCondition(
                    mission.CharacterA,
                    mission.GrantedCondition);
        }

        if (mission.WasSuccessful)
        {
            Gold += mission.GoldEarned;
        }

        Debug.Log(
            $"{mission.CharacterA.CharacterName} | " +
            $"{mission.Mission.MissionName} | " +
            $"Success: {mission.WasSuccessful} | " +
            $"Gold Earned: {mission.GoldEarned}");
    }
    private void HandleDayAdvanced()
    {
        List<MissionAssignment> missionsToRemove =
            new();

        foreach (MissionAssignment mission
                 in activeMissions)
        {
            mission.DaysRemaining--;

            Debug.Log(
                $"{mission.CharacterA.CharacterName} | " +
                $"{mission.Mission.MissionName} | " +
                $"{mission.DaysRemaining} days remaining");

            if (mission.DaysRemaining <= 0 &&
                !mission.Finished)
            {
                ResolveMission(mission);

                missionsToRemove.Add(mission);
            }
        }

        foreach (MissionAssignment mission
                 in missionsToRemove)
        {
            activeMissions.Remove(mission);
            
            MissionManager.Instance
                .UnregisterActiveMission(
                    mission.Mission);

            completedMissions.Add(mission);
            
            MissionManager.Instance
                .RegisterCompletedMission(
                    mission.Mission);

            Debug.Log(
                $"{mission.CharacterA.CharacterName} returned from mission");
        }
    }
}