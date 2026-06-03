public class MissionAssignment
{
    public CharacterData Character;
    public MissionData Mission;

    public int DaysRemaining;

    public float SuccessChance;

    public bool Finished;
    
    public bool WasSuccessful;

    public MissionAssignment(
        CharacterData character,
        MissionData mission,
        float successChance)
    {
        Character = character;
        Mission = mission;

        SuccessChance = successChance;

        DaysRemaining = mission.DaysRequired;
    }
}