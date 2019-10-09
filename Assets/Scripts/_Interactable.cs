using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// The Purpose of this scipt will be a base class that all objects the player can interact with will derive from this script

public class _Interactable : MonoBehaviour
{

    public float radius = 3; //This will specify how close the player needs to be near an object before they can interact with it. Such as picking up an item or attacking an enemy
    public Vector3 size;

    [SerializeField]
    Transform playerTransform; //Is the player?


    //The below method will visually repensent the radius in the scene
    private void OnDrawGizmosSelected()
    {

        size = new Vector3(2, 2);

        Gizmos.color = Color.yellow; //Sets the colour of the radius to yellow
        Gizmos.DrawWireCube(transform.position, size);
        Gizmos.DrawWireSphere(transform.position, radius);


    }

    // Start is called before the first frame update
    void Start()
    {
        if (playerTransform == null)
        {
            playerTransform = GameObject.Find("Player").transform;
        }
    }


    public virtual void Interact() //This method will allow me to call this method in the base class but will allow me to overwirte this method in a enemy script or inventory script
    {
        // This Method is meant to be overwritten as needed
        Debug.Log("PLAYER IS INTERACTING WITH " + transform.name);
    }


    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Z))
        {
            float distance = Vector3.Distance(playerTransform.position, transform.position); //Finds the difference between the player and the interactable object
            if(distance <= radius) //If the difference between the player is less then the radius, this means the player is within an interactable range
            {
                Interact();
            }

        }

    }
}
