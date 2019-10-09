using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    [SerializeField]
    float _maxHealth; //The Players max health

    [SerializeField]
    float _currentHealth; //The Players current health


    //Property to get players current health. Other scripts can access the players current health without modifying it by using a GET
    public float PlayerMaxHealth
    {
        get
        {
            return _maxHealth;
        }
    }

    //Property to get players current health. Other scripts can access the players current health without modifying it by using a GET
    public float PlayerCurrentHealth
    {
        get
        {
            return _currentHealth;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _maxHealth; //Set the players current health equal to the max health on gamestart.
    }

    // Update is called once per frame
    void Update()
    {
        HealthCheck(); //Always keep an eye on the players health
    }


    void HealthCheck()
    {
        if(_currentHealth <= 0)
        {
            PlayerFaints();
        }
    }

    void PlayerFaints()
    {
        //Do Something
        Debug.Log("The Players Health is 0");
        _currentHealth = _maxHealth; //Reset to max health for debug purposes
    }


    public void ReducePlayersHealth(int reduceHeatlh)
    {
        _currentHealth -= reduceHeatlh; //Reduce the players current health by the reduceHealth variable which will be passed in from other scripts
    }

    

}
