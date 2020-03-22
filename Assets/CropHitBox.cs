using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropHitBox : MonoBehaviour
{

    public GameObject player;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //Vector3 vec = new Vector3(1.26, 2.2, 3);
        //vec *= 2.0;
        //vec = Vector3(Mathf.Round(vec.x), Mathf.Round(vec.y), Mathf.Round(vec.z));
        //vec /= 2.0;

        //transform.position = player.transform.position + _offset; // 4 Mathf.Round( * 2f) * 0.5f;  (float)Math.Round(, MidpointRounding.AwayFromZero) / 2;   //;
        //transform.position = RoundToNearestHalf(player.transform.position.x);
    }

    public static float RoundToNearestHalf(float a)
    {
        return a = Mathf.Round(a * 2f) * 0.5f;
    }
}