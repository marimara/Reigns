using UnityEngine;

public class BoardUnlocker : MonoBehaviour
{
    public GameObject boardButton;
    public GameObject boardHighlight;

    public void UnlockBoard()
    {
        boardButton.SetActive(true);
        boardHighlight.SetActive(true);
    }
}