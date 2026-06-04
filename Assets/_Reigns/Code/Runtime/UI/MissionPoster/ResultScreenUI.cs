using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using DG.Tweening;

public class ResultScreenUI : MonoBehaviour
{
    public static ResultScreenUI Instance
    {
        get;
        private set;
    }
    [Title("Screen")]
    [SerializeField] private GameObject root;

    [Title("Mission")]
    [SerializeField] private Image missionImage;
    [SerializeField] private TMP_Text missionName;
    [SerializeField] private TMP_Text missionDescription;

    [Title("Result")]
    [SerializeField] private GameObject approvedStamp;
    [SerializeField] private GameObject failedStamp;
    [SerializeField] private TMP_Text rewardText;
    [SerializeField] private GameObject continueButton;
    
    [Title("Debug")]
    [SerializeField]
    private GuildManager guildManager;
    
    private void Awake()
    {
        Instance = this;
    }

    public void Show(MissionAssignment mission)
    {
        if (mission == null)
        {
            Debug.LogWarning("Mission is null.");
            return;
        } 
        
        root.SetActive(true);

        missionImage.sprite =
            mission.Mission.MissionImage;

        missionName.text =
            mission.Mission.MissionName;

        missionDescription.text =
            mission.Mission.Description;
        
        approvedStamp.SetActive(false);
        failedStamp.SetActive(false);

        rewardText.gameObject.SetActive(false);
        continueButton.SetActive(false);

        rewardText.text =
            mission.WasSuccessful
                ? $"+{mission.GoldEarned} Gold"
                : "No Reward";
        
        DOVirtual.DelayedCall(
            0.75f,
            () =>
            {
                if (mission.WasSuccessful)
                {
                    AnimateStamp(approvedStamp);
                }
                else
                {
                    AnimateStamp(failedStamp);
                }
            });
        DOVirtual.DelayedCall(
            1.50f,
            () =>
            {
                rewardText.gameObject.SetActive(true);

                rewardText.alpha = 0;

                rewardText.DOFade(
                    1,
                    0.25f);
            });
        DOVirtual.DelayedCall(
            2.0f,
            () =>
            {
                continueButton.SetActive(true);
            });
    }
    
    [Button]
    public void Hide()
    {
        root.SetActive(false);
    }
    
    public void ShowNextMission()
    {
        MissionAssignment mission =
            guildManager.ConsumeCompletedMission();

        if (mission == null)
        {
            Hide();
            return;
        }

        Show(mission);
    }
    
    private void AnimateStamp(GameObject stamp)
    {
        RectTransform rect =
            stamp.GetComponent<RectTransform>();

        CanvasGroup canvas =
            stamp.GetComponent<CanvasGroup>();

        rect.localScale = Vector3.one * 2f;
        canvas.alpha = 0f;

        stamp.SetActive(true);

        Sequence sequence = DOTween.Sequence();

        sequence.Append(
            rect.DOScale(1f, 0.25f)
                .SetEase(Ease.OutBack));

        sequence.Join(
            canvas.DOFade(1f, 0.15f));
    }
}