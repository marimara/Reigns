using UnityEngine;

public class StatRowUI : MonoBehaviour
{
    [Range(0, 10)]
    [SerializeField] private int value = 5;

    [SerializeField] private Transform squaresContainer;

    private void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        for (int i = 0; i < squaresContainer.childCount; i++)
        {
            Transform square = squaresContainer.GetChild(i);

            Transform check = square.Find("Check");

            if (check != null)
            {
                check.gameObject.SetActive(i < value);
            }
        }
    }

    public void SetValue(int newValue)
    {
        value = Mathf.Clamp(newValue, 0, 10);
        Refresh();
    }
}