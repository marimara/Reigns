using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    menuName = "Reigns/Condition Database")]
public class ConditionDatabase : ScriptableObject
{
    public List<ConditionData> Conditions =
        new();
}