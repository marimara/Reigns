using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Reigns/Character Data")]
public class CharacterData : ScriptableObject
{
    [BoxGroup("Identity")]
    public string CharacterName;

    [BoxGroup("Identity")]
    [PreviewField(80)]
    public Sprite Portrait;

    [BoxGroup("Stats")]
    [Range(0, 10)]
    public int Strength;

    [BoxGroup("Stats")]
    [Range(0, 10)]
    public int Agility;

    [BoxGroup("Stats")]
    [Range(0, 10)]
    public int Magic;

    [BoxGroup("Stats")]
    [Range(0, 10)]
    public int Defense;

    [BoxGroup("Stats")]
    [Range(0, 10)]
    public int Charisma;

    [BoxGroup("Stats")] 
    [Range(0, 10)] 
    public int Intellect;

    [BoxGroup("Stats")]
    [Range(0, 10)]
    public int Sanity;
    
    [BoxGroup("Unlock")]
    public CharacterUnlockType UnlockType;

    [BoxGroup("Unlock")]
    [ShowIf(nameof(ShowDayUnlock))]
    public int RequiredDay = 1;

    [BoxGroup("Unlock")]
    [ShowIf(nameof(ShowMissionUnlock))]
    public MissionData RequiredMission;
    
    private bool ShowDayUnlock()
    {
        return UnlockType ==
               CharacterUnlockType.Day;
    }

    private bool ShowMissionUnlock()
    {
        return UnlockType ==
               CharacterUnlockType.Mission;
    }
}