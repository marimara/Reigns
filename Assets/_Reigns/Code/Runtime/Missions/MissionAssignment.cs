public class MissionAssignment
{
    public CharacterData CharacterA;
    public CharacterData CharacterB;
    public MissionData Mission;

    public int DaysRemaining;

    public float SuccessChance;

    public bool Finished;
    
    public bool WasSuccessful;
    
    public int GoldEarned;

    public MissionAssignment(
        CharacterData characterA,
        CharacterData characterB,
        MissionData mission,
        float successChance)
    {
        CharacterA = characterA;
        CharacterB = characterB;
        Mission = mission;

        SuccessChance = successChance;

        DaysRemaining = mission.DaysRequired;
    }
}