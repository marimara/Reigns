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
    public static float CalculateSuccessChance(
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
    public static float CalculateTeamSuccessChance(
        CharacterData a,
        CharacterData b,
        MissionData mission)
    {
        if (a == null && b == null)
            return 0f;
        
        float total = 0f;
        int relevantStats = 0;

        AddScore(
            TeamStrength(a, b),
            mission.Strength,
            ref total,
            ref relevantStats);

        AddScore(
            TeamAgility(a, b),
            mission.Agility,
            ref total,
            ref relevantStats);

        AddScore(
            TeamMagic(a, b),
            mission.Magic,
            ref total,
            ref relevantStats);

        AddScore(
            TeamDefense(a, b),
            mission.Defense,
            ref total,
            ref relevantStats);

        AddScore(
            TeamCharisma(a, b),
            mission.Charisma,
            ref total,
            ref relevantStats);

        AddScore(
            TeamIntellect(a, b),
            mission.Intellect,
            ref total,
            ref relevantStats);

        AddScore(
            TeamSanity(a, b),
            mission.Sanity,
            ref total,
            ref relevantStats);

        if (relevantStats == 0)
            return 1f;

        return total / relevantStats;
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
    private static int TeamStrength(
        CharacterData a,
        CharacterData b)
    {
        return (a?.Strength ?? 0)
               + (b?.Strength ?? 0);
    }

    private static int TeamAgility(
        CharacterData a,
        CharacterData b)
    {
        return (a?.Agility ?? 0)
               + (b?.Agility ?? 0);
    }

    private static int TeamMagic(
        CharacterData a,
        CharacterData b)
    {
        return (a?.Magic ?? 0)
               + (b?.Magic ?? 0);
    }

    private static int TeamDefense(
        CharacterData a,
        CharacterData b)
    {
        return (a?.Defense ?? 0)
               + (b?.Defense ?? 0);
    }

    private static int TeamCharisma(
        CharacterData a,
        CharacterData b)
    {
        return (a?.Charisma ?? 0)
               + (b?.Charisma ?? 0);
    }

    private static int TeamIntellect(
        CharacterData a,
        CharacterData b)
    {
        return (a?.Intellect ?? 0)
               + (b?.Intellect ?? 0);
    }

    private static int TeamSanity(
        CharacterData a,
        CharacterData b)
    {
        return (a?.Sanity ?? 0)
               + (b?.Sanity ?? 0);
    }
}