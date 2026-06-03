using Sirenix.OdinInspector;
using UnityEngine;

public class MissionTester : MonoBehaviour
{
    [InlineEditor]
    public CharacterData Character;

    [InlineEditor]
    public MissionData Mission;

    [Button(ButtonSizes.Large)]
    public void TestMission()
    {
        MissionResult result =
            MissionResolver.Resolve(
                Character,
                Mission);

        Debug.Log(
            $"Success: {result.Success}\n" +
            $"Chance: {result.SuccessChance:P}\n" +
            $"Gold: {result.GoldEarned}");
    }
}