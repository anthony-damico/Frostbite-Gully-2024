using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SeasonManager : MonoBehaviour
{
    //https://docs.unity3d.com/ScriptReference/Tilemaps.Tilemap.SwapTile.html

    [SerializeField] private Tilemap tilemapGround;
    [SerializeField] private Tilemap tilemapCollision;
    [SerializeField] private TileBase[] tileBaseSpring;
    [SerializeField] private TileBase[] tileBaseSummer;
    [SerializeField] private TileBase[] tileBaseAutumn;
    [SerializeField] private TileBase[] tileBaseWinter;

    [SerializeField] private TileBase[] tileBaseFromSeason;
    [SerializeField] private TileBase[] tileBaseTargetSeason;

    [SerializeField] private Seasons toSeason;
    [SerializeField] private Seasons fromSeason;
    [SerializeField] private Seasons compareSeason;
    [SerializeField] private Seasons currentSeason;


    private void Start()
    {
        toSeason = TimeManagerController.instance.MySeason; //Get the current Season and store it in the ToSeason variable when the scene loads
        fromSeason = TimeManagerController.instance.MySeason; //Get the current Season and store it in the ToSeason variable when the scene loads
        compareSeason = TimeManagerController.instance.MySeason; //Get the current Season and store it in the compareSeason variable when the scene loads
        currentSeason = TimeManagerController.instance.MySeason; //Get the current Season and store it in the compareSeason variable when the scene loads
        tileBaseFromSeason = tileBaseSpring; //The game starts in spring so set the tileset to spring
        tileBaseTargetSeason = tileBaseSummer; //The game starts in spring so set the tileset to spring
    }

    private void Update()
    {
        ChangeSeason();
        CheckSeason();
        
    }

    private void CheckSeason()
    {
        if(toSeason == TimeManagerController.instance.MySeason)
        {
            toSeason++; //Add 1 to the current season enum to select the next season
            
            if((int)toSeason > 3) //If the season greater then 3 (Or winter) set to 0 (Spring)
            {
                toSeason = Seasons.spring; //Set to spring
            }

            Debug.Log("Next season is: " + toSeason);

        }

        if(fromSeason == TimeManagerController.instance.MySeason)
        {
            fromSeason--; //Add 1 to the current season enum to select the next season

            if ((int)fromSeason < 0) //If the season less then 0 (Or spring) set to 3 (Winter)
            {
                fromSeason = Seasons.winter; //Set to winter
            }

            Debug.Log("Previous season is: " + fromSeason);
        }

    }

    private void ChangeSeason()
    {
        if (TimeManagerController.instance.MySeason != compareSeason) //If the currentSeason and the Compare season match, then do nothing, if they do match, change the tileset
        {
            //Update The season tileset
            GetSeasonTileset(compareSeason, toSeason); //Get the CurrenSeason (compareSeason) and the targetSeason (toSeason) tilesets populated
            ChangeTileset(tileBaseFromSeason, tileBaseTargetSeason);

            //Update the CompareSeason to match the current Season
            compareSeason = TimeManagerController.instance.MySeason;
            Debug.Log("Current season is: " + compareSeason);
        }

        
    }

    private void GetSeasonTileset(Seasons fromSeason, Seasons targetSeason)
    {
        //Get the FromSeason Tileset into the temp array
        if(fromSeason == Seasons.spring)
        {
            tileBaseFromSeason = tileBaseSpring; //Set the FromSeason tilebase array to the spring tileset
        }
        else if (fromSeason == Seasons.summer)
        {
            tileBaseFromSeason = tileBaseSummer; //Set the FromSeason tilebase array to the spring tileset
        }
        else if (fromSeason == Seasons.autumn)
        {
            tileBaseFromSeason = tileBaseAutumn; //Set the FromSeason tilebase array to the spring tileset
        }
        else if (fromSeason == Seasons.winter)
        {
            tileBaseFromSeason = tileBaseWinter; //Set the FromSeason tilebase array to the spring tileset
        }

        //Get the TargetSeason Tileset into the temp array
        if (targetSeason == Seasons.spring)
        {
            tileBaseTargetSeason = tileBaseSpring; //Set the FromSeason tilebase array to the spring tileset
        }
        else if (targetSeason == Seasons.summer)
        {
            tileBaseTargetSeason = tileBaseSummer; //Set the FromSeason tilebase array to the spring tileset
        }
        else if (targetSeason == Seasons.autumn)
        {
            tileBaseTargetSeason = tileBaseAutumn; //Set the FromSeason tilebase array to the spring tileset
        }
        else if (targetSeason == Seasons.winter)
        {
            tileBaseTargetSeason = tileBaseWinter; //Set the FromSeason tilebase array to the spring tileset
        }
    }

    private void ChangeTileset(TileBase[] fromSeason, TileBase[] targetSeason)
    {
        for (int i = 0; i < fromSeason.Length; i++)
        {
            tilemapGround.SwapTile(fromSeason[i], targetSeason[i]);
            tilemapCollision.SwapTile(fromSeason[i], targetSeason[i]);
        }
    }

    ////Spawn tilemap when scene loads 
    //GameObject tilemap = (GameObject)Instantiate(tilemapArray[seasonVariable], transform.position, Quaternion.identity);

    //
    //void Start()
    //{
    //    Tilemap tilemap = GetComponent<Tilemap>();
    //    tilemap.SwapTile(tileA, tileB);
    //}

}
