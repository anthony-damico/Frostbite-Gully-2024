using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionChickenCoop : SceneTransition
{

    public ChickenCoopScript chickenCoop; //Reference to the ChickenCoop
    public AnimalValue animalValue;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        animalValue.listOfAnimals.Clear();
<<<<<<< Updated upstream
=======
        animalValue.listOfAnimals.TrimExcess(); //Set the list back to size 0
>>>>>>> Stashed changes
        animalValue.listOfAnimals.AddRange(chickenCoop.chickensInCoop);
        SceneTransfer(collider);
    }

}
