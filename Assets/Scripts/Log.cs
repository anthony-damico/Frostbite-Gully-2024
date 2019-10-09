using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{
    private Rigidbody2D myRigidbody;
    public Transform target;
    public float chaseRadius; //this is te radius that the ememy will chase the player
    public float attackRadius; //this is the radius that the enemy will attack the player at
    public Transform homePosition; //If the player moves outside the chase radius, this will move the enemy back to there home position
    public Animator animator; //Used to create a reference to the animator
    

    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.idle; //enemy will start in the idle state
        myRigidbody = GetComponent<Rigidbody2D>(); //Completes the reference to the ridgidbody
        target = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>(); //completes the reference to the animator allow me to call objects from the animator to use on the log
       
    }

    // FixedUpdate only moves on physics calls
    void FixedUpdate()
    {
        CheckDistance();
    }

    //This checks to see if the player is in the enemy raduis and gives chase / attacks if true
    void CheckDistance()
    {
        if(Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if(currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                changeAnim(temp - transform.position);
                myRigidbody.MovePosition(temp);
                ChangeState(EnemyState.walk); //uses the ChangeState method which will also be used to change animations
                animator.SetBool("wakeUp", true); //Changes the animation to logWakeUp
            }
        }
        else if(Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            animator.SetBool("wakeUp", false); //Changes the animation to logWidle? logsleep? whatever its called
        }
    }

    private void SetAnimFloat(Vector2 setVector)
    {
        animator.SetFloat("moveX", setVector.x);
        animator.SetFloat("moveY", setVector.y);
    }


    //This is used to allow the enemy (log) to look in different directions Such as facing up when X & Y = UP
    private void changeAnim(Vector2 direction)
    {
        if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if(direction.x > 0)
            {
                SetAnimFloat(Vector2.right); //Vector2.right is shorthand for writing: Vector2(1, 0)
            }
            else if(direction.x < 0)
            {
                SetAnimFloat(Vector2.left); //Shorthand for writing Vector2(-1, 0).
            }
        }
        else if(Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if(direction.y > 0)
            {
                SetAnimFloat(Vector2.up); //Shorthand for writing Vector2(0, 1).
            }
            else if(direction.y < 0)
            {
                SetAnimFloat(Vector2.down); //Shorthand for writing Vector2(0, -1).
            }
        }
    }

    // see video 19 to explain in detail?
    private void ChangeState(EnemyState newState)
    {
        currentState = newState;
    }

}
