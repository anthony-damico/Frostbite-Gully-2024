using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_03 : MonoBehaviour
{
    public DialogUIManager dialogUI;    //Reference to the DialogUIManager
    public Character npc;               //This is the information about a NPC Including name and portrait
    public bool playerInRange;          //A check against the Box Collider 2D to check that the player is in range. Could be replace with Raycast at a later date

    private void Start()
    {
        dialogUI = GameObject.Find("DialogUIManager").GetComponent<DialogUIManager>(); //Complete the reference to the DialogUIManager
    }


    //Runs when you press spacebar or A on the xbox controller. Part of the New Input System
    public void OnAction()
    {
        if(playerInRange == true) //If the player is in range, execute the code inside the IF statement
        {
            GetCharacterInfo(npc);
            dialogUI.OpenDialogUI();
            StartCoroutine(dialogUI.WriteTextv2("This is my first text"));
            //dialogUI.WriteText($@"This is my first text");
            //dialogUI.WriteText($@"This is my Second text");
            //dialogUI.CloseDialogUI();
        }
        
    }



    public void GetCharacterInfo(Character currentNPC)
    {
        dialogUI.namePlateText.text = currentNPC.characterName;
        dialogUI.portraitImage.sprite = currentNPC.characterPortrait;
    }

    //This Collider will check if the player is in range to activate the sign
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Player is in range of NPC");
        }
    }

    //This colider will check to see if the player is not in range, therefore can't activate the sign
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            dialogUI.CloseDialogUI(); //This set the dialogbox to inctive (hidden) if not in range
            //Debug.Log("Player not in range");
        }
    }

}
