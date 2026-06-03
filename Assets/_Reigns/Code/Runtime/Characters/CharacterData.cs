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
    public int Sanity;
}