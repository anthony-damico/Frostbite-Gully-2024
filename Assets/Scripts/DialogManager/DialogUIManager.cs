using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogUIManager : MonoBehaviour
{
    public GameObject dialogUICanvas;            //This is a link to the DialogUIManager/DialogUICanvas to turn the DialogUI on and Off as needed
    public TextMeshProUGUI dialogText;      //This is the text that is that is displayed in the DialogUI. It will be updated during the ProgressDialog() method
    public TextMeshProUGUI namePlateText;   //This is the text that is that is displayed in the NamePlate UI Element. It will be updated during the ProgressDialog() and StartDialog() methods
    public Image portraitImage;             //The image displayed as part of the DialogUI



    public void OpenDialogUI()
    {
        dialogUICanvas.SetActive(true);
    }

    public void CloseDialogUI()
    {
        dialogUICanvas.SetActive(false);
    }

    public void WriteText(string text)
    {
        dialogText.text = text;     
    }

    public IEnumerator WriteTextv2(string text)
     {
        while(true)
        {
            while (!Input.GetKeyDown(KeyCode.Space))
            {
                yield return null;
                dialogText.text = text;
                Debug.Log("SpaceBar?");
            }
        }
     }
}
