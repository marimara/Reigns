using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class CharacterPosterUI : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private CharacterData characterData;

    [Header("Identity")]
    [SerializeField] private Image portraitImage;
    [SerializeField] private TMP_Text nameText;
    
    [Header("Stats")]
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
            return;

        nameText.text = characterData.CharacterName;
        portraitImage.sprite = characterData.Portrait;
        
        strengthRow.SetValue(characterData.Strength);
        agilityRow.SetValue(characterData.Agility);
        magicRow.SetValue(characterData.Magic);
        defenseRow.SetValue(characterData.Defense);
        sanityRow.SetValue(characterData.Sanity);
        charismaRow.SetValue(characterData.Charisma);
        intellectRow.SetValue(characterData.Intellect);
        
    }
}