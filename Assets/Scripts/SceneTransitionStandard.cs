using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionStandard : SceneTransition
{

    public void OnTriggerEnter2D(Collider2D collider)
    {
        SceneTransfer(collider);
    }

}
