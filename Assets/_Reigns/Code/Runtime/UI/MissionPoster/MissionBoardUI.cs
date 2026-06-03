using Sirenix.OdinInspector;
using UnityEngine;

public class MissionBoardUI : MonoBehaviour
{
    [Title("References")]
    [SerializeField] private MissionPosterUI missionPoster;

    [SerializeField] private CharacterSlotUI[] characterSlots;

    private CharacterSlotUI GetFirstFilledSlot()
    {
        foreach (CharacterSlotUI slot in characterSlots)
        {
            if (!slot.IsEmpty)
                return slot;
        }

        return null;
    }
    public void OnSendMissionClicked()
    {
        CharacterSlotUI slot = GetFirstFilledSlot();

        if (slot == null)
        {
            Debug.Log("No character selected");
            return;
        }

        MissionData mission =
            missionPoster.MissionData;

        float successChance =
            MissionResolver.CalculateSuccessChance(
                slot.AssignedCharacter,
                mission);

        MissionAssignment assignment =
            new MissionAssignment(
                slot.AssignedCharacter,
                mission,
                successChance);

        GuildManager.Instance.AddMission(
            assignment);

        slot.Clear();

        Debug.Log(
            $"{assignment.Character.CharacterName} sent to {assignment.Mission.MissionName}");
    }
}