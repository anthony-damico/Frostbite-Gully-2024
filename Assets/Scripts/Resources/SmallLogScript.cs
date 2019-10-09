﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallLogScript : MonoBehaviour
{
    //Manager References
    EquipmentManager equipmentManager; //Creates a reference to the EquipmentManager so i can see what tool the player has equipped

    //Variables
    [SerializeField]
    int _healthMax;         //How much health the rock can have -- This might change to be based on the Axe hitting it.

    [SerializeField]
    int _healthCurrent;     //How much health the rock has remaining

    [SerializeField]
    GameObject _plotObject1;     //1 of the 4 plots sitting underneath this gameObect. This gameObject needs to be manipulated on the DestroyRock() function

    // Start is called before the first frame update
    void Start()
    {
        equipmentManager = EquipmentManager.instance; //Completes the reference back to the EquipmentManager.cs script using the singleton

        _healthCurrent = _healthMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (_healthCurrent <= 0)
        {
            DestroyObject();
        }
    }

    public void DamageObject(RaycastHit2D hit)
    {
        if (equipmentManager.currentEquipment.equipmentType == ToolType.axe)
        {
            _healthCurrent = _healthCurrent - 1; //Reduce the rocks health by 1
            Debug.Log("Log: SmallLogScript.cs. " + hit.collider.name + " took 1 damage");
        }
        else
        {
            Debug.Log("Log: SmallLogScript.cs. A Axe is not Equip");
        }

    }

    //THis method is called form the GridScript.cs when a Object is Instantiated. This will load the plot(s) sitting underneath the rock into rock
    //The purpose of this is to modifed the plots underneath the rock when the rock breaks
    public void GetPlotInfoFromGridScript(GameObject plotObject1)
    {
        _plotObject1 = plotObject1;
    }

    void DestroyObject()
    {
        Destroy(gameObject); //Remove the gameobject from the scene (Destroy this rock)
        _plotObject1.GetComponent<CropManagerController>().isEmpty = true; //Makes the underlying plot empty again. So new resources or plots can be generate. Can maybe replace GetComponent<CropManagerController>() with a variable later if this needs to be more scaleable
    }

}
