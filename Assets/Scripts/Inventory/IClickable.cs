using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

//IClickable is used everytime the user can click somethign in the inventory with the mouse (As long as the interface is created)

public interface IClickable
{

    //
    Image MyIcon
    {
        get;
        set;
    }

    //THis count decides how many items are left on the slot
    int MyCount
    {
        get;
    }

    Text MyStackText
    {
        get;
    }

}