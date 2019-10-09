using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScriptControl : MonoBehaviour
{

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
