using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    menuName = "Reigns/Character Database")]
public class CharacterDatabase : ScriptableObject
{
    public List<CharacterData> Characters =
        new();
}