using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class CharacterPosterUI : MonoBehaviour
{
    [Title("Data")]
    [SerializeField] private CharacterData characterData;

    [Title("States")]
    [SerializeField] private GameObject characterContent;
    [SerializeField] private GameObject selectCharacter;

    [Title("Identity")]
    [SerializeField] private Image portraitImage;
    [SerializeField] private TMP_Text nameText;
    
    [Title("Stats")]
    [SerializeField] private StatRowUI strengthRow;
    [SerializeField] private StatRowUI agilityRow;
    [SerializeField] private StatRowUI magicRow;
    [SerializeField] private StatRowUI defenseRow;
    [SerializeField] private StatRowUI sanityRow;
    [SerializeField] private StatRowUI charismaRow;
    [SerializeField] private StatRowUI intellectRow;
    

    private void Start()
    {
        Refresh();
    }

    [Button]
    public void Refresh()
    {
        if (characterData == null)
        {
            characterContent.SetActive(false);
            selectCharacter.SetActive(true);
            return;
        }

        nameText.text = characterData.CharacterName;
        portraitImage.sprite = characterData.Portrait;
        
        strengthRow.SetValue(
            characterData.Strength,
            CharacterConditionManager
                .Instance
                .GetModifier(
                    characterData,
                    CharacterStatType.Strength));
        agilityRow.SetValue(
            characterData.Agility,
            CharacterConditionManager
                .Instance
                .GetModifier(
                    characterData,
                    CharacterStatType.Agility));
        magicRow.SetValue(
            characterData.Magic,
            CharacterConditionManager
                .Instance
                .GetModifier(
                    characterData,
                    CharacterStatType.Magic));
        defenseRow.SetValue(
            characterData.Defense,
            CharacterConditionManager
                .Instance
                .GetModifier(
                    characterData,
                    CharacterStatType.Defense));
        sanityRow.SetValue(
            characterData.Sanity,
            CharacterConditionManager
                .Instance
                .GetModifier(
                    characterData,
                    CharacterStatType.Sanity));
        charismaRow.SetValue(
            characterData.Charisma,
            CharacterConditionManager
                .Instance
                .GetModifier(
                    characterData,
                    CharacterStatType.Charisma));
        intellectRow.SetValue(
            characterData.Intellect,
            CharacterConditionManager
                .Instance
                .GetModifier(
                    characterData,
                    CharacterStatType.Intellect));
        
    }
    [Button]
    public void SetCharacter(CharacterData data)
    {
        characterData = data;
        characterContent.SetActive(true);
        selectCharacter.SetActive(false);
        Refresh();
    }
    [Button]
    public void Clear()
    {
        characterData = null;

        characterContent.SetActive(false);
        selectCharacter.SetActive(true);
    }
}