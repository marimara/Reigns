using Sirenix.OdinInspector;
using UnityEngine;

public enum MissionType
{
    Combat,
    Exploration,
    Social,
    Arcane,
    Survival
}

[CreateAssetMenu(menuName = "Reigns/Mission Data")]
public class MissionData : ScriptableObject
{
    // =========================
    // Identity
    // =========================

    [BoxGroup("Identity")]
    public string MissionName;

    [BoxGroup("Identity")]
    public MissionType MissionType;

    [BoxGroup("Identity")]
    [PreviewField(80)]
    public Sprite MissionImage;

    [BoxGroup("Identity")]
    [TextArea(4, 8)]
    public string Description;

    // =========================
    // Mission
    // =========================

    [BoxGroup("Mission")]
    [MinValue(1)]
    public int DaysRequired = 1;

    [BoxGroup("Mission")]
    [MinValue(1)]
    public int Slots = 1;
    [BoxGroup("Mission")]
    [MinValue(1)]
    public int MinDay = 1;

    // =========================
    // Rewards
    // =========================

    [BoxGroup("Rewards")]
    [MinValue(0)]
    public int GoldReward;

    // =========================
    // Requirements
    // =========================

    [BoxGroup("Requirements")]
    [PropertySpace(SpaceBefore = 10)]
    [Title("Hidden Requirements")]
    
    [BoxGroup("Requirements")]
    [Range(0, 20)]
    public int Strength;

    [BoxGroup("Requirements")]
    [Range(0, 20)]
    public int Agility;

    [BoxGroup("Requirements")]
    [Range(0, 20)]
    public int Magic;

    [BoxGroup("Requirements")]
    [Range(0, 20)]
    public int Defense;

    [BoxGroup("Requirements")]
    [Range(0, 20)]
    public int Charisma;

    [BoxGroup("Requirements")]
    [Range(0, 20)]
    public int Intellect;

    [BoxGroup("Requirements")]
    [Range(0, 20)]
    public int Sanity;

    // =========================
    // Debug
    // =========================

    [BoxGroup("Debug")]
    [ShowInInspector, ReadOnly]
    public int TotalRequirement =>
        Strength +
        Agility +
        Magic +
        Defense +
        Charisma +
        Intellect +
        Sanity;
}