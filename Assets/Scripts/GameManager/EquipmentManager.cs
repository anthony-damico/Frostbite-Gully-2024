using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;

public class EquipmentManager : MonoBehaviour
{

    #region Singleton
    public static EquipmentManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public Equipment currentEquipment; //This is the current Item Equip

    private PlayerMovement playerMovement; //This is a reference to the PlayerMovement Script


    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.FindObjectOfType(typeof(PlayerMovement)) as PlayerMovement;    //This complete the reference to the PlayerMovement Script
    }

    void Update()
    {
        updateAnimations();     //Update Animations based on the item equip
    }

    public void Equip(Equipment newItem)
    {
        currentEquipment = newItem;
    }


    //Pass Animations to the playerMovement script for the Tool / Item Equip
    void updateAnimations()
    {
        if (currentEquipment != null) //This means an item is equip
        {
            playerMovement.clipDown = currentEquipment.clipDown;
            playerMovement.clipLeft = currentEquipment.clipLeft;
            playerMovement.clipUp = currentEquipment.clipUp;
            playerMovement.clipRight = currentEquipment.clipRight;
        }
    }



}
