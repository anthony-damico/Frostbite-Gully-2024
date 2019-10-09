using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;


public delegate void UpdateStackEvent(); //This is the event delgate

//This iis simply going to be doing whatever a normal stack does
//it is being created asa generic class so it can be used outside the inventory
//a generic datatype is a type that can be told what to be whenever it is instantiated. Eg it can be instantiated as a INT, STRING, ITEM etc
//T means type is generic.
public class ObservableStack<T> : Stack<T> //The ObservableStack inherits from the Stack Type
{
    //The below are public events. Whenever the event fucntion is called, <Need>
    public event UpdateStackEvent OnPush; //THis happens when you push something into a stack
    public event UpdateStackEvent OnPop; //THis happens when you pop (use) something into a stack
    public event UpdateStackEvent OnClear; //THis happens when you remove something into a stack

    //THis is giving items and moving onto base?? Not sure what this means. 
    public ObservableStack(ObservableStack<T> items) : base(items)
    {

    }

    //This is a constructor of some type
    public ObservableStack()
    {

    }


    public new void Push(T item)
    {
        base.Push(item);//This is the base function from the type Stack

        if (OnPush != null) //If something is listening to OnPush, we can use OnPush
        {
            OnPush();
        }
    }

    public new T Pop()
    {
        T item = base.Pop();//This is the base function from the type Stack

        if (OnPop != null) //If something is listening to OnPop, we can use OnPop
        {
            OnPop();
        }

        return item;
    }

    public new void Clear()
    {
        base.Clear();//This is the base function from the type Stack

        if (OnClear != null) //If something is listening to OnClear, we can use OnClear
        {
            OnClear();
        }
    }

}