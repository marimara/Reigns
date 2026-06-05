using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class MissionBoardUI : MonoBehaviour
{
    [Title("References")]
    [SerializeField] private MissionPosterUI missionPoster;
    
    [SerializeField] private GameObject missionContent;

    [SerializeField] private CharacterSlotUI[] characterSlots;
    
    [Title("Screens")]
    [SerializeField] private GameObject missionBoard;
    [SerializeField] private ResultScreenUI resultScreen;
    
    private int currentMissionIndex;
    
    
    private void Start()
    {
        RefreshMission();
    }

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
        
        RefreshMission();

        slot.Clear();

        Debug.Log(
            $"{assignment.Character.CharacterName} sent to {assignment.Mission.MissionName}");
    }
    public void OpenBoard()
    {
        RefreshMission();

        missionBoard.SetActive(true);

        if (GuildManager.Instance.CompletedMissions.Count > 0)
        {
            resultScreen.ShowNextMission();
        }
    }
    private void RefreshMission()
    {
        if (MissionManager.Instance.AvailableMissions.Count == 0)
        {
            missionContent.SetActive(false);
            return;
        }

        missionContent.SetActive(true);

        if (currentMissionIndex >=
            MissionManager.Instance.AvailableMissions.Count)
        {
            currentMissionIndex = 0;
        }

        missionPoster.SetMission(
            MissionManager.Instance
                .AvailableMissions[currentMissionIndex]);
    }
    public void NextMission()
    {
        if (MissionManager.Instance.AvailableMissions.Count == 0)
            return;

        currentMissionIndex++;

        if (currentMissionIndex >= MissionManager.Instance.AvailableMissions.Count)
        {
            currentMissionIndex = 0;
        }

        RefreshMission();
    }
    public void PreviousMission()
    {

        if (MissionManager.Instance.AvailableMissions.Count == 0)
            return;

        currentMissionIndex--;

        if (currentMissionIndex < 0)
        {
            currentMissionIndex =
                MissionManager.Instance.AvailableMissions.Count - 1;
        }

        RefreshMission();
    }
    public void ForceRefresh()
    {
        RefreshMission();
    }
    
    
}