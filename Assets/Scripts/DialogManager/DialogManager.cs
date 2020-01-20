using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{

    public Dialog currentDialog;        //The Dialog Currently running
    public GameObject dialogBox;        //This is a link to the DialogUIManager/DialogUICanvas to turn the DialogUI on and Off as needed
    public TextMeshProUGUI dialogText;  //This is the text that is that is displayed in the DialogUI. It will be updated during the ProgressDialog() method
    [SerializeField] int index = 0;              //This is the current count/posisiton in the currentDialog array (the scriptable object attached to this)
    public bool playerInRange;          //A check against the Box Collider 2D to check that the player is in range. Could be replace with Raycast at a later date



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) == true && playerInRange == true)
        {
            Debug.Log("In Range");

            if (dialogBox.activeInHierarchy == false) //if the dialogbox is not active
            {
                Debug.Log("Spacebar inside update pressed");
                dialogBox.SetActive(true);
                dialogText.text = currentDialog.lines[index].text;
                ProgressDialog();
            }
        }
    }


    void ProgressDialog()
    {
        if(index < currentDialog.lines.Length - 1) //While the index (current count) is less then the total number of elements in the currentDialog.lines array. Do the stuff inside the if statement
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Spacebar inside ProgressDialog pressed");
                index++; //increase the currentDialog.lines array by 1 to select the next sentence
                dialogText.text = "";
                dialogText.text = currentDialog.lines[index].text;
            }

        }
        else
        {
            dialogBox.SetActive(false);
            index = 0;
        }
        
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
