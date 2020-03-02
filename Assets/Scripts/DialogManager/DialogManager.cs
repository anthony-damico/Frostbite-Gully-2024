using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{

    public Dialog dialog;                   //The Dialog
    public Dialog currentDialog;           //The dialog that is currently being run
    public GameObject dialogBox;            //This is a link to the DialogUIManager/DialogUICanvas to turn the DialogUI on and Off as needed
    public TextMeshProUGUI dialogText;      //This is the text that is that is displayed in the DialogUI. It will be updated during the ProgressDialog() method
    public TextMeshProUGUI namePlateText;   //This is the text that is that is displayed in the NamePlate UI Element. It will be updated during the ProgressDialog() and StartDialog() methods
    public Image portraitImage;             //The image displayed as part of the DialogUI
    [SerializeField] int index = 0;         //This is the current count/posisiton in the currentDialog array (the scriptable object attached to this)
    public bool playerInRange;              //A check against the Box Collider 2D to check that the player is in range. Could be replace with Raycast at a later date


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Part of the new input system. When you press an action button, the code inside OnAction() is executed
    private void OnAction()
    {
        if (playerInRange == true)
        {
            if (dialogBox.activeInHierarchy == false) //If the Dialog Box is open/Active. This means a some dialog is currently running 
            {
                StartDialog();
            }
            else
            {
                ProgressDialog();
            }
        }
    }

    void StartDialog()
    {
        currentDialog = dialog;
        dialogBox.SetActive(true);
        dialogText.text = currentDialog.lines[index].text;
        namePlateText.text = currentDialog.lines[index].character.characterName;
        portraitImage.sprite = currentDialog.lines[index].character.characterPortrait;
    }

    void ProgressDialog()
    {
        if(index < currentDialog.lines.Length - 1) //While the index (current count) is less then the total number of elements in the currentDialog.lines array. Do the stuff inside the if statement
        {
            index++; //increase the currentDialog.lines array by 1 to select the next sentence
            dialogText.text = ""; //Clear out the old dialog
            namePlateText.text = currentDialog.lines[index].character.characterName;
            portraitImage.sprite = currentDialog.lines[index].character.characterPortrait;
            dialogText.text = currentDialog.lines[index].text; //Display the next sentence in the dialog
        }
        else
        {
            if(currentDialog.nextDialog != null)
            {
                GetNextDialog();
            }
            else
            {
                dialogBox.SetActive(false); //Disable the dialog box
                index = 0; //reset the index to restart the dialog
            }

        }     
    }

    void GetNextDialog()
    {
        index = 0;
        currentDialog = currentDialog.nextDialog;
        dialogText.text = currentDialog.lines[index].text; //Display the next sentence in the dialog
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
            dialogBox.SetActive(false); //This set the dialogbox to inctive (hidden) if not in range
            //Debug.Log("Player not in range");
        }
    }
}
