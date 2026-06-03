using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class GuildManager : MonoBehaviour
{
    public static GuildManager Instance { get; private set; }

    [Title("Guild")]
    [ShowInInspector, ReadOnly]
    public int CurrentDay { get; private set; } = 1;

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

    public IReadOnlyList<MissionAssignment> ActiveMissions
        => activeMissions;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
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
    [Button(ButtonSizes.Large)]
    public void AdvanceDay()
    {
        CurrentDay++;

        Debug.Log(
            $"Advanced to Day {CurrentDay}");
        
        List<MissionAssignment> completedMissions =
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

                completedMissions.Add(mission);
            }
        }
        foreach (MissionAssignment mission
                 in completedMissions)
        {
            activeMissions.Remove(mission);

            Debug.Log(
                $"{mission.Character.CharacterName} returned from mission");
        }
    }
    private void ResolveMission(
        MissionAssignment mission)
    {
        float roll = Random.value;

        mission.WasSuccessful =
            roll <= mission.SuccessChance;

        mission.Finished = true;

        Debug.Log(
            $"{mission.Character.CharacterName} | " +
            $"{mission.Mission.MissionName} | " +
            $"Success: {mission.WasSuccessful}");
    }
}