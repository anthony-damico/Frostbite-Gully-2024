using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PurchaseManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text inputValue;    //This is the input field text object mapped via the editor

    [SerializeField]
    private GameObject proceedButton;   //Used to proceed to the the next step, mapped in the editor

    [SerializeField]
    private TMP_Text confrimName;   //Text to show the name being confrimed, mapped in editor

    [SerializeField]
    private GameObject confrimButton;   //Used to proceed to the the next step, mapped in the editor

    [SerializeField]
    private GameObject cancelButton;   //Used to return to the the previous step, mapped in the editor

    [SerializeField]
    private CanvasGroup MenuCanvas;

    [SerializeField]
    private CanvasGroup inputCanvas;

    [SerializeField]
    private CanvasGroup confrimCanvas;

    [SerializeField]
    private CanvasGroup ChooseBuildingCanvas;

    [SerializeField]
    private CanvasGroup ReturnToMenuCanvas;

    [SerializeField]
    private AnimalBuildingSerialization animalBuildingSerialization; //Reference to the SO

    [SerializeField]
    private AnimalStatsSerialization animalStatsSerialization;

    [SerializeField]
    private BuildingButtonScript buildingButtonPrefab;

    [SerializeField]
    private Transform parentgameObjectGroupingPanel;

    [SerializeField]
    private VectorValue vectorValue;

    [SerializeField]
    public TMP_Text animalAddedText;

    [SerializeField]
    private List<BuildingButtonScript> ListOfBuildings = new List<BuildingButtonScript>();


    public void Start()
    {

        //Hide all canvas except the Menu canvas
        ShowCanvas(MenuCanvas);
        HideCanvas(inputCanvas);
        HideCanvas(confrimCanvas);
        HideCanvas(ChooseBuildingCanvas);
        HideCanvas(ReturnToMenuCanvas);
    }

    public void BuyButton()
    {
        HideCanvas(MenuCanvas);
        ShowCanvas(inputCanvas);
        HideCanvas(confrimCanvas);
        HideCanvas(ChooseBuildingCanvas);
        HideCanvas(ReturnToMenuCanvas);
    }

    public void SellButton()
    {

    }

    public void Proceed()
    {
        Debug.Log("Button was pressed on " + this.name);
        Debug.Log("Input Box Length:" + (inputValue.text).Length);

        int inputFieldLength = (inputValue.text).Length;

        if (inputFieldLength > 1) //Check that the length of the text is greater then 1
        {
            confrimName.text = "Is this Name Correct: " + inputValue.text;
            HideCanvas(MenuCanvas);
            HideCanvas(inputCanvas);
            ShowCanvas(confrimCanvas);
            HideCanvas(ChooseBuildingCanvas);
            HideCanvas(ReturnToMenuCanvas);
        }
    }

    public void Confrim()
    {
        InstantiateBuildingButtons();
        HideCanvas(MenuCanvas);
        HideCanvas(inputCanvas);
        HideCanvas(confrimCanvas);
        ShowCanvas(ChooseBuildingCanvas);
        HideCanvas(ReturnToMenuCanvas);
    }

    public void Cancel()
    {
        HideCanvas(MenuCanvas);
        ShowCanvas(inputCanvas);
        HideCanvas(confrimCanvas);
        HideCanvas(ChooseBuildingCanvas);
        HideCanvas(ReturnToMenuCanvas);
    }

    public void ReturnToGame()
    {
        SceneManager.LoadScene(vectorValue.previousScene);
    }

    public void ContinueShopping()
    {
        HideCanvas(MenuCanvas);
        HideCanvas(inputCanvas);
        HideCanvas(confrimCanvas);
        HideCanvas(ChooseBuildingCanvas);
        ShowCanvas(ReturnToMenuCanvas);
    }

    public void ReturnToMenu()
    {
        ShowCanvas(MenuCanvas);
        HideCanvas(inputCanvas);
        HideCanvas(confrimCanvas);
        HideCanvas(ChooseBuildingCanvas);
        HideCanvas(ReturnToMenuCanvas);
        DestroyBuildingButtons();
    }



    public void InstantiateBuildingButtons()
    {

        foreach (var building in animalBuildingSerialization.animalBuildingList)
        {
            Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            BuildingButtonScript button = Instantiate(buildingButtonPrefab, position, Quaternion.identity);
            button.transform.SetParent(parentgameObjectGroupingPanel, false); //button.transform.parent = parentgameObjectGroupingPanel.transform;
            button.buildingUuid = building.buildingUuid;
            button.animalName = inputValue.text;
            button.currentBuildingCapacity = CurrentBuildingCapacity(button.buildingUuid, animalStatsSerialization);
            button.maxBuildingCapacity = building.buildingSize;
            button.buildingNameText.text = building.buildingName;
            if(button.currentBuildingCapacity < button.maxBuildingCapacity)
            {
                button.buildingNameText.text = "\r\n" + button.buildingNameText.text + "\r\n" + button.currentBuildingCapacity + " / " + button.maxBuildingCapacity + " Spaces Free";
            }
            else
            {
                button.buildingNameText.text = "\r\n" + button.buildingNameText.text + "\r\n" + "Full";
            }
            ListOfBuildings.Add(button);

        }
    }

    public void DestroyBuildingButtons()
    {
        for (int i = 0; i < parentgameObjectGroupingPanel.transform.childCount; i++)
        {
            Destroy(parentgameObjectGroupingPanel.transform.GetChild(i).gameObject);
        }
    }

    public void ShowCanvas(CanvasGroup canvasGroup)
    {
        //Display the game menu
        canvasGroup.alpha = 1; //Make the menu Visable
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true; //Enable the interactable compoent of the canvas group so the event system can interact
    }

    public void HideCanvas(CanvasGroup canvasGroup)
    {
        //Display the game menu
        canvasGroup.alpha = 0; //Make the menu Visable
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false; //Enable the interactable compoent of the canvas group so the event system can interact
    }

    public int CurrentBuildingCapacity(string uuid, AnimalStatsSerialization animalStatsSerialization)
    {
        int CurrentBuildingCapacity = 0;

        foreach (var animal in animalStatsSerialization.animalList)
        {
            if(animal.builidngUuid == uuid)
            {
                CurrentBuildingCapacity++;
            }
        }

        return CurrentBuildingCapacity;

    }

}
