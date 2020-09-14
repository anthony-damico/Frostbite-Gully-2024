using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTest : MonoBehaviour
{

    //Raycast and LayerMask Variables
    private int layermask1 = 1 << 9;                    //Plot Layer
    private int layermask2 = 1 << 10;                   //BigResource Layer
    private int layermask3 = 1 << 11;                   //SmallResource Layer
    private LayerMask requiredLayersMask;               //Not sure what this is yet?


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //This Collider will check if the player is in range to activate the sign
    private void OnTriggerEnter2D(Collider2D collision)
    {
        requiredLayersMask = layermask1 | layermask2 | layermask3; //Logic copied from here: https://answers.unity.com/questions/1499447/how-to-distinguish-which-layer-has-been-hit-by-ray.html


        if (collision.CompareTag("ToolHighlight"))
        {
            //collision.IsTouchingLayers(layerMask: 1 << 10)
            if (collision.IsTouchingLayers(layerMask: 1 << LayerMask.NameToLayer("BigResource"))) //BigResource Layer
            {
                Debug.Log("Hit " + this.name + " which is a Big Resource");
            }

            else if (collision.IsTouchingLayers(layerMask: 1 << LayerMask.NameToLayer("SmallResource"))) //SmallResource Layer
            {
                Debug.Log("Hit " + this.name + " which is Small Resource");
            }

            else if (collision.IsTouchingLayers(layerMask: 1 << LayerMask.NameToLayer("Plot"))) //Plot Layer
            {
                Debug.Log("Hit " + this.name + " which is a plot");
            }
        }
    }

}
