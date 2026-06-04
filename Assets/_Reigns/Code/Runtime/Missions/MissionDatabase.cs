using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "MissionDatabase",
    menuName = "Reigns/Missions/Mission Database")]
public class MissionDatabase : ScriptableObject
{
    public List<MissionData> Missions = new();
}