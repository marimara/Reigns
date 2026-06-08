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
    public int GetStrengthModifier(
        CharacterData character)
    {
        int total = 0;

        List<CharacterCondition> conditions =
            GetConditions(character);

        if (conditions == null)
            return 0;

        foreach (CharacterCondition condition
                 in conditions)
        {
            total +=
                condition.Data.Strength;
        }

        return total;
    }
}