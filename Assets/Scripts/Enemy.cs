using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger
}

public class Enemy : MonoBehaviour
{

    public EnemyState currentState; //reference to the EnemyState state machine
    public int health; //This will be the Enemies hit points
    public string enemyName; //This is the enemy name
    public int baseAttack; //this is the enemies base attack
    public float moveSpeed;  //this is the enemies move speed

    public void Knock(Rigidbody2D myRigidbody, float knockBackTime)
    {
        StartCoroutine(KnockBackCo(myRigidbody, knockBackTime));
    }

    private IEnumerator KnockBackCo(Rigidbody2D myRigidbody, float knockBackTime)
    {

        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(knockBackTime); //How long to wait
            myRigidbody.velocity = Vector2.zero;
            currentState = EnemyState.idle; //sets the enemy state machine to back to idle
            myRigidbody.velocity = Vector2.zero;
        }

    }


}
