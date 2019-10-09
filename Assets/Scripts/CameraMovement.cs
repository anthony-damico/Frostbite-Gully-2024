using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    //This public variable is a reference to what game object i want the camera to follow. In this case "Player"
    public Transform target;

    //This public variable is the camera speed moving to the target
    public float smoothing;

    //This is the maxiumum co-ordiance that the camera is allowed move
    public Vector2 maxPosition;

    //This is the minimum co-ordiance that the camera is allowed move
    public Vector2 minPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // LateUpdate() always comes last
    void LateUpdate()
    {
        
        //This checks to see if the camera posistion matches the target position "Player". If they do not match, begin code in the if statement which will be to move the camera to the player
        if(transform.position != target.position)
        {

            //Create a new vector3 variable known as targetPosition, this will have new x, y, z parametres passed into it for moving the camera with the player
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z); //by setting z the transform (gameobject camera), it allows the object to be its own z axis

            //Clamp has 3 inputs. A = what value you want to clamp. b = what the min clamp will be. c = what the max clamp will be 
            //Clamp the X axis
            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);

            //Clamp the X axis
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);

            //this will find the distance between it and the target and move a % of the distance each frame
            //Lerp has 3 functions: a = Position object is currently at. B = Position of object we want to go to. C = the amount of space we want to cover
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);


        }

    }
}
