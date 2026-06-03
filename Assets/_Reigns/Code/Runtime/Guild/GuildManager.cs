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
}