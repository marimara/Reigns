using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class MissionPosterUI : MonoBehaviour
{
    [Header("Data")]
    [InlineEditor]
    [SerializeField] private MissionData missionData;
    public MissionData MissionData => missionData;

    [Header("UI")]
    [SerializeField] private TMP_Text missionNameText;
    [SerializeField] private Image missionImage;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private TMP_Text daysText;
    [SerializeField] private TMP_Text slotsText;
    [SerializeField] private TMP_Text rewardText;
    [SerializeField] private TMP_Text successChanceText;

    private void Start()
    {
        Refresh();
    }

    [Button]
    public void Refresh()
    {
        if (missionData == null)
            return;

        missionNameText.text = missionData.MissionName;

        missionImage.sprite = missionData.MissionImage;

        descriptionText.text = missionData.Description;

        daysText.text = $"Days: {missionData.DaysRequired}";
        slotsText.text = $"Slots: {missionData.Slots}";
        rewardText.text = $"Gold: {missionData.GoldReward}";
        successChanceText.text = "Chance of Success: 0%";
    }

    public void SetMission(MissionData newMission)
    {
        missionData = newMission;
        Refresh();
    }
    public void SetSuccessChance(
        float chance)
    {
        int percent =
            Mathf.RoundToInt(chance * 100f);

        successChanceText.text =
            $"Chance of Success: {percent}%";
    }
}