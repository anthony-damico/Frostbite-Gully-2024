using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalScript : MonoBehaviour
{
    [SerializeField]
    public Guid animalUuid = Guid.NewGuid();
    public string animalName;
}
