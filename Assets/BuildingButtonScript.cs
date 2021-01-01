using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButtonScript : MonoBehaviour
{
    [SerializeField]
    public Text buildingNameText;

    [SerializeField]
    public string buildingUuid;

    [SerializeField]
    public string animalName;

    [SerializeField]
    public int currentBuildingCapacity;

    [SerializeField]
    public int maxBuildingCapacity;

    [SerializeField]
    public CreateAnimalScript createAnimalScript;

    [SerializeField]
    public PurchaseManager purchaseManager;


    public void Start()
    {
        createAnimalScript = GetComponent<CreateAnimalScript>();
        purchaseManager = GameObject.Find("PurchaseManager").GetComponent<PurchaseManager>();
    }


    public void SerializeAnimal()
    {
        if(currentBuildingCapacity < maxBuildingCapacity)
        {
            createAnimalScript.CreateChicken(createAnimalScript.chickenPrefab, buildingUuid, animalName, 0, 100);
            purchaseManager.animalAddedText.text = animalName + " has been Added to " + buildingNameText.text;
            purchaseManager.ContinueShopping();
        }
        else
        {
            //Choose another coop
            Debug.Log(buildingNameText.text + " is full, please pick another building");
        }
    }


}
