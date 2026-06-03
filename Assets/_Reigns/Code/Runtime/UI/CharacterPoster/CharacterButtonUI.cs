using UnityEngine;
using UnityEngine.UI;

public class CharacterButtonUI : MonoBehaviour
{
    [SerializeField] private Image portrait;

    private CharacterData characterData;

    public void Setup(CharacterData data)
    {
        characterData = data;

        if (portrait != null)
            portrait.sprite = data.Portrait;
    }

    public CharacterData GetCharacter()
    {
        return characterData;
    }

    public void OnClicked()
    {
        Debug.Log(characterData.CharacterName);
    }
}