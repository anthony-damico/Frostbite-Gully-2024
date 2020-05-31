using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScriptControl : MonoBehaviour
{
    [SerializeField] private CanvasGroup menuBackground;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void Start()
    {
        HideMenuOnGameStart();
    }

    public void HideMenuOnGameStart()
    {
        menuBackground.alpha = 0; //Make the menu Visable
        menuBackground.blocksRaycasts = false;
        menuBackground.interactable = false; //Enable the interactable compoent of the canvas group so the event system can interact
    }

}
