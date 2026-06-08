using System.Collections.Generic;
using UnityEngine;

public class CharacterConditionManager : MonoBehaviour
{
    private Dictionary<
            CharacterData,
            List<CharacterCondition>>
        activeConditions =
            new();
    public static CharacterConditionManager Instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        Instance = this;
    }
    public List<CharacterCondition>
        GetConditions(
            CharacterData character)
    {
        if (character == null)
            return null;

        if (!activeConditions.TryGetValue(
                character,
                out List<CharacterCondition> conditions))
        {
            return null;
        }

        return conditions;
    }
    public void AddCondition(
        CharacterData character,
        ConditionData condition)
    {
        if (character == null)
            return;

        if (condition == null)
            return;

        if (!activeConditions.ContainsKey(character))
        {
            activeConditions.Add(
                character,
                new List<CharacterCondition>());
        }

        activeConditions[character]
            .Add(
                new CharacterCondition(
                    condition));
    }
    public int GetModifier(
        CharacterData character,
        CharacterStatType stat)
    {
        List<CharacterCondition> conditions =
            GetConditions(character);

        if (conditions == null)
            return 0;

        int total = 0;

        foreach (CharacterCondition condition
                 in conditions)
        {
            total +=
                GetConditionStatValue(
                    condition.Data,
                    stat);
        }

        return total;
    }
    public int GetCurrentStat(
        CharacterData character,
        CharacterStatType stat)
    {
        if (character == null)
            return 0;

        return
            GetBaseStatValue(
                character,
                stat)
            +
            GetModifier(
                character,
                stat);
    }
    private int GetConditionStatValue(
        ConditionData condition,
        CharacterStatType stat)
    {
        return stat switch
        {
            CharacterStatType.Strength =>
                condition.Strength,

            CharacterStatType.Agility =>
                condition.Agility,

            CharacterStatType.Magic =>
                condition.Magic,

            CharacterStatType.Defense =>
                condition.Defense,

            CharacterStatType.Charisma =>
                condition.Charisma,

            CharacterStatType.Intellect =>
                condition.Intellect,

            CharacterStatType.Sanity =>
                condition.Sanity,

            _ => 0
        };
    }
    private int GetBaseStatValue(
        CharacterData character,
        CharacterStatType stat)
    {
        return stat switch
        {
            CharacterStatType.Strength =>
                character.Strength,

            CharacterStatType.Agility =>
                character.Agility,

            CharacterStatType.Magic =>
                character.Magic,

            CharacterStatType.Defense =>
                character.Defense,

            CharacterStatType.Charisma =>
                character.Charisma,

            CharacterStatType.Intellect =>
                character.Intellect,

            CharacterStatType.Sanity =>
                character.Sanity,

            _ => 0
        };
    }
}