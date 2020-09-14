using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenCoopScript : BuildingScript
{

    public List<ChickenScript> chickensInCoop = new List<ChickenScript>();
    public int coopSize;
    public ChickenScript chickenGameObject; //Map in the editor


    void addChickenToCoop(ChickenScript chicken)
    {

        if(chickensInCoop.Count <= coopSize)
        {
            if (placeChickenInCoop(chicken) == true)
            {
                return;
            }
        }

    }
    
    bool placeChickenInCoop(ChickenScript chicken)
    {
        chickensInCoop.Add(chicken);
        return true;
    }

}
