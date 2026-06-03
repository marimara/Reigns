using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

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
    [ShowInInspector, ReadOnly]
    private List<MissionAssignment> activeMissions = new();
    
    [Title("Completed Missions")]
    [ShowInInspector, ReadOnly]
    private List<MissionAssignment> completedMissions = new();

    public IReadOnlyList<MissionAssignment> ActiveMissions
        => activeMissions;
    public IReadOnlyList<MissionAssignment> CompletedMissions
        => completedMissions;

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
    }
    public bool IsCharacterBusy(
        CharacterData character)
    {
        foreach (MissionAssignment mission in activeMissions)
        {
            if (mission.Character == character)
                return true;
        }

        return false;
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

        if (mission.WasSuccessful)
        {
            Gold += mission.GoldEarned;
        }

        Debug.Log(
            $"{mission.Character.CharacterName} | " +
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
                $"{mission.Character.CharacterName} | " +
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

            completedMissions.Add(mission);

            Debug.Log(
                $"{mission.Character.CharacterName} returned from mission");
        }
    }
}