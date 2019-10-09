using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// © 2017 TheFlyingKeyboard and released under MIT License
// theflyingkeyboard.net
public class SpawnOnClick : MonoBehaviour
{

    public float minX; //MinX Position on a map
    public float minY; //MinY Position on a map
    public float maxX; //MaxX Position on a map
    public float maxY; //MaxY Position on a map
    public float prefabWidth; //width of the prefab
    public float prefabLength; //length of the prefab
    public Transform PrefabToSpawn; //The Prefab being spawned
    public int number = 1;
    public int orderInLayer = 1;

    void Start()
    {
        for (float i = minX; i < maxX; i += prefabWidth)
        {
            for (float j = minY; j < maxY; j += prefabLength)
            {
                PrefabToSpawn.name = "plot";
                string tmpName = PrefabToSpawn.name;
                PrefabToSpawn.name = PrefabToSpawn.name + number;
                //PrefabToSpawn.GetComponent<SpriteRenderer>.orderInLayer = orderInLayer;
                Instantiate(PrefabToSpawn, new Vector3(i, j, 0), Quaternion.identity);
                number = number + 1;
                PrefabToSpawn.name = tmpName;


            }
        }
    }

    //public GameObject objectToSpawn;
    // Use this for initialization
    //// Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        spawnPosition.z = 0.0f;
    //        GameObject objectInstance = (GameObject)Instantiate(Resources.Load("Pot"), spawnPosition, Quaternion.Euler(new Vector3(0, 0, 0)));
    //    }
    //}
}