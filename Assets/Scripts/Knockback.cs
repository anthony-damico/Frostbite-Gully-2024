using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{

    public float thrust; //this is the force that a player can knock the enemy back
    public float knockBackTime; //Time the Knockback lasts

    //Has an enemy enter the trigger area
    private void OnTriggerEnter2D(Collider2D collision)
    {

        //THis was being preformed in the PlayerHit.cs script. Probably doesn't need its own script
        if (collision.gameObject.CompareTag("breakable") && this.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Pot>().Smash();
        }
        
        if (collision.gameObject.CompareTag("enemy") || collision.gameObject.CompareTag("Player"))
        {
            //variable hit will be used for objects that are "Hit" by another object
            Rigidbody2D hit = collision.GetComponent<Rigidbody2D>(); 
            if (hit != null) //this means the enemy has a ridgidbody
            {

                //These actions apply to both the player and enemy
                Vector2 difference = hit.transform.position - transform.position; //get the difference between the player and the enemy??
                difference = difference.normalized * thrust; //Normalize the knockback speed
                hit.AddForce(difference, ForceMode2D.Impulse); //Apply the force to the enemy

                if (collision.gameObject.CompareTag("enemy")) //Only complete actions that are applicable to the enemy in this IF statement
                {
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger; //sets the enemy state machine to stagger
                    collision.GetComponent<Enemy>().Knock(hit, knockBackTime); //This allows the object (enemy) to monitor its own knockback instead of a global task monitoring the object 
                }

                if (collision.gameObject.CompareTag("Player")) //Only complete actions that are applicable to the player in this IF statement
                {
                    hit.GetComponent<PlayerMovement>().currentState = PlayerState.stagger; //sets the player state machine to stagger
                    //collision.GetComponent<PlayerMovement>().Knock(knockBackTime); //This allows the object (player) to monitor its own knockback instead of a global task monitoring the object (Knock has been disabled in the playermovement script
                }
            }
        }
    }
}
