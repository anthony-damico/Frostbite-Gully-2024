using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolHighlight : MonoBehaviour
{
    public PlayerMovement player;
    Vector3 offset; //Offset is used to determine the direction of the crophitbox. Eg if the x axis is negative, then the player is facing left so the offset will move the crophitobx to the left of the player.

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    // LateUpdate is called last
    void LateUpdate()
    {
        if (player.currentPlayerDirection == PlayerDirection.Up)
        {
            offset.x = 0.0f;
            offset.y = 1.5f;
        }

        if (player.currentPlayerDirection == PlayerDirection.Down)
        {
            offset.x = 0.0f;
            offset.y = -1.5f;
        }

        if (player.currentPlayerDirection == PlayerDirection.Left)
        {
            offset.x = -1.5f;
            offset.y = 0.0f;
        }

        if (player.currentPlayerDirection == PlayerDirection.Right)
        {
            offset.x = 1.5f; //Make this number smaller to get the hitbox closer to player (confrimed. Tweek this to optimize how close the hitbox is to the player
            offset.y = 0.0f;
        }

        //transform.position = new Vector3(Mathf.Round(player.transform.position.x) + offset.x, Mathf.Round(player.transform.position.y) + offset.y);
        transform.position = new Vector3((Mathf.Round(player.transform.position.x * 2f) * 0.5f) + offset.x, (Mathf.Round(player.transform.position.y * 2f) * 0.5f) + offset.y);
    }
}
