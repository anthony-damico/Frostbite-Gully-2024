using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VectorValue", menuName = "Vectors/VectorValue", order = 1)]
public class VectorValue : ScriptableObject
{
    public Vector2 initialValue; //XY value moving to on new scene
    public Vector2 previousValue; //XY Value returning to on previous scene
    public bool usePreviousValue; //Filter to identify if using previous value
    public string buildingUuid; //The building ID being passed through to the new scene
}
