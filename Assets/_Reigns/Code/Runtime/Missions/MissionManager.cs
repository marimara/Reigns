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

    public IReadOnlyList<MissionData>
        AvailableMissions => availableMissions;

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
            if (TimeManager.Instance.CurrentDay <
                mission.MinDay)
                continue;

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
}