using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{

    public GameObject dialogBox; //This is the link the dialog Box game object to the sign game object
    public Text dialogText; //This is a link to the Text box that is linked to the DialogBox Game Object
    public string dialog; //This is text we want to show
    public bool playerInRange;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) == true && playerInRange == true)
        {
            if(dialogBox.activeInHierarchy) //Checks to see if the dialogbox is active
            {
                dialogBox.SetActive(false);
            }
            else
            {
                dialogBox.SetActive(true);
                dialogText.text = dialog;
            }
        }
    }

    //This Collider will check if the player is in range to activate the sign
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.CompareTag("Player"))
        {
            playerInRange = true;
            //Debug.Log("Player is in range");
        }
    }

    //This colider will check to see if the player is not in range, therefore can't activate the sign
    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if(collision.CompareTag("Player"))
        {
            playerInRange = false;
            dialogBox.SetActive(false); //This set the dialogbox to inctive (hidden) if not in range
            //Debug.Log("Player not in range");
        }
    }

}
