public class CharacterCondition
{
    public ConditionData Data;

    public int DaysRemaining;

    public CharacterCondition(
        ConditionData data)
    {
        Data = data;
        DaysRemaining =
            data.DurationDays;
    }
}