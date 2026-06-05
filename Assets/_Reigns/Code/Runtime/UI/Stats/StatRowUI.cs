using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class StatRowUI : MonoBehaviour
{
    [Range(0, 10)]
    [OnValueChanged(nameof(Refresh))]
    [SerializeField]
    private int baseValue = 5;

    [OnValueChanged(nameof(Refresh))]
    [SerializeField]
    private int modifier = 0;

    [SerializeField] private Transform squaresContainer;
    
    [SerializeField] private Color normalColor =
        Color.black;

    [SerializeField] private Color bonusColor =
        Color.limeGreen;

    [SerializeField] private Color penaltyColor =
        Color.red;
    
    

    private void Start()
    {
        Refresh();
    }
    

    public void Refresh()
    {
        int currentValue =
            Mathf.Clamp(
                baseValue + modifier,
                0,
                10);

        for (int i = 0;
             i < squaresContainer.childCount;
             i++)
        {
            Transform square =
                squaresContainer.GetChild(i);

            Transform check =
                square.Find("Check");

            if (check == null)
                continue;

            Image image =
                check.GetComponent<Image>();

            int visibleSquares =
                modifier < 0
                    ? baseValue
                    : currentValue;

            bool active =
                i < visibleSquares;

            check.gameObject.SetActive(active);

            if (!active)
                continue;

            if (modifier < 0 &&
                i >= currentValue &&
                i < baseValue)
            {
                image.color =
                    penaltyColor;
            }
            else if (modifier > 0 &&
                     i >= baseValue)
            {
                image.color =
                    bonusColor;
            }
            else
            {
                image.color =
                    normalColor;
            }
        }
    }

    public void SetValue(
        int newValue,
        int newModifier = 0)
    {
        baseValue =
            Mathf.Clamp(newValue, 0, 10);

        modifier =
            newModifier;

        Refresh();
    }
}