using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(
    menuName = "Reigns/Condition Data")]
public class ConditionData : ScriptableObject
{
    [BoxGroup("Identity")]
    public string ConditionName;

    [BoxGroup("Identity")]
    public MissionType MissionType;

    [BoxGroup("Identity")]
    public bool IsPositive;

    [BoxGroup("Duration")]
    [MinValue(1)]
    public int DurationDays = 3;

    [BoxGroup("Modifiers")]
    public int Strength;

    [BoxGroup("Modifiers")]
    public int Agility;

    [BoxGroup("Modifiers")]
    public int Magic;

    [BoxGroup("Modifiers")]
    public int Defense;

    [BoxGroup("Modifiers")]
    public int Charisma;

    [BoxGroup("Modifiers")]
    public int Intellect;

    [BoxGroup("Modifiers")]
    public int Sanity;
}