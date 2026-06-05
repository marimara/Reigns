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
    private int GetFilledSlotCount()
    {
        int count = 0;

        foreach (CharacterSlotUI slot in characterSlots)
        {
            if (!slot.gameObject.activeSelf)
                continue;

            if (!slot.IsEmpty)
                count++;
        }

        return count;
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
        
        int selectedCharacters =
            GetFilledSlotCount();

        if (selectedCharacters <
            mission.Slots)
        {
            Debug.Log(
                "Not enough characters selected");

            return;
        }

        float successChance =
            MissionResolver.CalculateSuccessChance(
                slot.AssignedCharacter,
                mission);
        
        CharacterData characterA =
            characterSlots[0].AssignedCharacter;

        CharacterData characterB = null;

        if (mission.Slots >= 2)
        {
            characterB =
                characterSlots[1]
                    .AssignedCharacter;
        }

        MissionAssignment assignment =
            new MissionAssignment(
                characterA,
                characterB,
                mission,
                successChance);

        GuildManager.Instance.AddMission(
            assignment);
        
        RefreshMission();

        slot.Clear();

        Debug.Log(
            $"{assignment.CharacterA.CharacterName} sent to {assignment.Mission.MissionName}");
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
        
        UpdateSlotVisibility();
        
        UpdateSuccessChance();
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
    private void OnEnable()
    {
        CharacterSlotUI.OnCharacterChanged +=
            UpdateSuccessChance;
    }

    private void OnDisable()
    {
        CharacterSlotUI.OnCharacterChanged -=
            UpdateSuccessChance;
    }
    private void UpdateSuccessChance()
    {
        CharacterSlotUI slot =
            GetFirstFilledSlot();

        if (slot == null)
        {
            missionPoster.SetSuccessChance(0);
            return;
        }

        float chance =
            MissionResolver.CalculateSuccessChance(
                slot.AssignedCharacter,
                missionPoster.MissionData);

        missionPoster.SetSuccessChance(
            chance);
    }
    private void UpdateSlotVisibility()
    {
        if (missionPoster.MissionData == null)
            return;

        int requiredSlots =
            missionPoster.MissionData.Slots;

        characterSlots[0]
            .gameObject.SetActive(true);

        characterSlots[1]
            .gameObject.SetActive(
                requiredSlots >= 2);
    }
    
}