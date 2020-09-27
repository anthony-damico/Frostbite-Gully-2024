using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionAnimalBuilding : SceneTransition
{
    public BuildingScript animalBuilding; //Reference to the ChickenCoop

    public void OnTriggerEnter2D(Collider2D collider)
    {
        playerGlobalPosition.buildingUuid = animalBuilding.buildingUuid;
        SceneTransfer(collider);
    }

}
