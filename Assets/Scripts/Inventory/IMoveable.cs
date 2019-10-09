using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;


//THe IMoveable interface is used on GameObects that can be moved when they are click
public interface IMoveable
{

    //A Moveable object must have a icon to be moved
    Sprite MyIcon
    {
        get;
    }

}


