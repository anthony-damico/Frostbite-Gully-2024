using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChickenStatsSerialization", menuName = "SceneData/ChickenStatsSerialization", order = 2)]
public class ChickenStatsSerialization : ScriptableObject
{
    public List<ChickenStats> chickenList = new List<ChickenStats>();
}

[System.Serializable]
public class ChickenStats
{
    public int animalUuid;
    public string animalName;
    public string animalAge;
    public string animalHealth;
}