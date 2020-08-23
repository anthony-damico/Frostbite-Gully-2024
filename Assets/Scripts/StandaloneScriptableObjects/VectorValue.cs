using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VectorValue", menuName = "Vectors/VectorValue", order = 1)]
public class VectorValue : ScriptableObject
{
    public Vector2 initialValue;
    public Vector2 previousValue;
    public bool usePreviousValue;
}
