using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActionBarSetupScript : MonoBehaviour
{

    [SerializeField] private ActionBarScript[] _actionslots; //This is a list of all Actionslots

    // Start is called before the first frame update
    void Start()
    {
        int index = 0;
        _actionslots = GetComponentsInChildren<ActionBarScript>();

        foreach (ActionBarScript actionSlot in _actionslots)
        {
            actionSlot.MyActionInventorySlot = InventoryScriptV2.instance._slots[index];
            index++;
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
