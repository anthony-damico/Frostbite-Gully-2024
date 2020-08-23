using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public enum TransferType    //Used to tell the scene transfer where to use the initalValue (AKA New Scene Coordiants) or use previousValue to return to the previous coordiants)
    {
        initalValue,
        previousValue
    }

    public TransferType transferType; //Used to tell the scene transfer where to use the initalValue (AKA New Scene Coordiants) or use previousValue to return to the previous coordiants)

    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerGlobalPosition;
    


    public void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player") && !collider.isTrigger)
        {
            if(transferType == TransferType.initalValue)
            {
                Debug.Log("initalValue");
                playerGlobalPosition.initialValue = playerPosition;
                playerGlobalPosition.previousValue = GameObject.Find("Player").transform.position; //If we are transferring to a new scene or location, capture the previous coordinantes
                playerGlobalPosition.usePreviousValue = false;
            }
            else if (transferType == TransferType.previousValue)
            {
                Debug.Log("previousValue");
                playerPosition = playerGlobalPosition.previousValue;
                playerGlobalPosition.usePreviousValue = true;
                
            }
                        
            SceneManager.LoadScene(sceneToLoad);
            
        }
    }
}
