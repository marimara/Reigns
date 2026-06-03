using UnityEngine;

public static class MissionResolver
{
    public static MissionResult Resolve(
        CharacterData character,
        MissionData mission)
    {
        MissionResult result = new();

        float chance = CalculateSuccessChance(character, mission);

        result.SuccessChance = chance;

        result.Success = Random.value <= chance;

        result.GoldEarned = result.Success
                ? mission.GoldReward
                : 0;

        return result;
    }

    private static float GetRatio(
        int characterStat,
        int requiredStat)
    {
        if (requiredStat <= 0)
            return 1f;

        return Mathf.Clamp01(
            (float)characterStat / requiredStat);
    }
    private static float CalculateSuccessChance(
        CharacterData character,
        MissionData mission)
    {
        if (MeetsRequirements(character, mission))
            return 1f;
        
        float total = 0f;
        int relevantStats = 0;

        AddScore(
            character.Strength,
            mission.Strength,
            ref total,
            ref relevantStats);

        AddScore(
            character.Agility,
            mission.Agility,
            ref total,
            ref relevantStats);

        AddScore(
            character.Magic,
            mission.Magic,
            ref total,
            ref relevantStats);

        AddScore(
            character.Defense,
            mission.Defense,
            ref total,
            ref relevantStats);

        AddScore(
            character.Charisma,
            mission.Charisma,
            ref total,
            ref relevantStats);

        AddScore(
            character.Intellect,
            mission.Intellect,
            ref total,
            ref relevantStats);

        AddScore(
            character.Sanity,
            mission.Sanity,
            ref total,
            ref relevantStats);

        if (relevantStats == 0)
            return 1f;

        return total / relevantStats;
    }
    private static void AddScore(
        int characterStat,
        int requiredStat,
        ref float total,
        ref int relevantStats)
    {
        if (requiredStat <= 0)
            return;

        relevantStats++;

        total += Mathf.Clamp01(
            (float)characterStat / requiredStat);
    }
    private static bool MeetsRequirements(
        CharacterData character,
        MissionData mission)
    {
        return
            character.Strength >= mission.Strength &&
            character.Agility >= mission.Agility &&
            character.Magic >= mission.Magic &&
            character.Defense >= mission.Defense &&
            character.Charisma >= mission.Charisma &&
            character.Intellect >= mission.Intellect &&
            character.Sanity >= mission.Sanity;
    }
}