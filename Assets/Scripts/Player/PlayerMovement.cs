using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
using UnityEngine.Animations;



//Create a State Machine here
//A State Machine is like a bool that allows more then true/false (Or maybe think of it as a TAG or a Variable that support TEXT instead of Numbers)
//In the state machine, you define any state you need, then you can use that state as a filter when doing IF statements. 
//For Example, I can make a state called Attack. When i want to do an attack, I need to set the player state machine equal to "attack" when a button is pressed and only if the player is not already
//in the attack state. Once the script moves to the attack state, the animation is called to preform an attack.
public enum PlayerState
{
    walk,
    attack,
    interact,
    stagger,
    idle
}


public enum PlayerDirection
{
    Left,
    Right,
    Up,
    Down
}

public class PlayerMovement : MonoBehaviour
{
    //Declare Variables
    public PlayerState currentState;                    //This makes a reference to the state Machine above  
    public float speed;                                 //By declare "speed" as a public variable, it allows me to control the player speed via the unity interface
    private Rigidbody2D myRigidbody;                    //This makes a reference to the player object Rigidbody2D
    private Vector3 change;                             //Vector3 is used to pass position and direction for an object (x, y, z axis)
    private Animator animator;                          //This variable makes a reference to the Animator
    private PlayerDirection playerDirection;            //This creates a reference to the PlayerDirection state machine
    public CropManagerController cropManagerController; //This creates a refernce to the CropManagerController script
    public EquipmentManager equipmentManager;           //This creates a refernce to the equipmentManager script
    private PlayerStats playerStats;                    //This creates a areferce to the PlayerStats.cs script

    //Player Animation Variables
    public AnimationClip clipDown;                      //The Animation for a tool/items down position
    public AnimationClip clipLeft;                      //The Animation for a tool/items Left position
    public AnimationClip clipRight;                     //The Animation for a tool/items Right position
    public AnimationClip clipUp;                        //The Animation for a tool/items Up position
    private PlayableGraph playableGraph;                //A playableGraph is used with the Playable API to play a animation without having to create an animation tree
    private bool playerMovementDisabled;                //Is used to stop the player moving when an animation is playing (such as watering seeds)
    public float playerAnimSeconds;                     //This is the length a animation clip will play for. For now, all animations will play 0.350f

    //Raycast and LayerMask Variables
    private int layermask1 = 1 << 9;                    //Plot Layer
    private int layermask2 = 1 << 10;                   //BigResource Layer
    private int layermask3 = 1 << 11;                   //SmallResource Layer
    private LayerMask requiredLayersMask;               //Not sure what this is yet?

    //Controller Variables
    public string btnHorizontal = "Horizontal";         //Input Managers input for left and right
    public string btnVertical = "Vertical";             //Input Managers input for Up and Down
    public string btnAction2 = "Action2";               //Input Managers input for using something. EG tool (X button on an Xbox Controller)            


    //Start Methods

    // Start is called before the first frame update
    void Start()
    {
        playerMovementDisabled = false;
        currentState = PlayerState.walk;                                                                                //When the player script starts, the player state will be set to walk      
        myRigidbody = GetComponent<Rigidbody2D>();                                                                      //This completes the reference to the Player Rigidbody2D       
        animator = GetComponent<Animator>();                                                                            //This complete the reference to the Animator
        animator.SetFloat("moveX", 0);                                                                                  //This line of code and the next line of code define the player sprite as facing "DOWN"
        animator.SetFloat("moveY", -1);
        cropManagerController = GameObject.FindObjectOfType(typeof(CropManagerController)) as CropManagerController;    //This complete the reference to the EquipmentManager Script
        equipmentManager = GameObject.FindObjectOfType(typeof(EquipmentManager)) as EquipmentManager;                   //This complete the reference to the EquipmentManager Script
        playerStats = GameObject.FindObjectOfType(typeof(PlayerStats)) as PlayerStats;                                  //This complete the reference to the PlayerStats.cs Script
        playerDirection = PlayerDirection.Down;                                                                         //Sets the player Direction to down when the game start
    }

    // Update is called once per frame
    void Update()
    {

        checkPlayerDirection(); //Check the player direction and set the player direction to left, right, update, down according

        if (EventSystem.current.IsPointerOverGameObject()) //This is a built in unity function that checks if the mouse pointer is over a gameObject. If it is True, no other script will run
        {
            return;
        }

        change = Vector3.zero;  //By setting change (Vector3 Function) to zero, it means that the player is reset every frame

        //The below determines weather the user/player is pressing any keys (up, down, left, right)
        change.x = Input.GetAxisRaw(btnHorizontal); //Horizontal is defined by default in Unity 
        change.y = Input.GetAxisRaw(btnVertical); //Vertical is defined by default in Unity 

        if (Input.GetButtonDown(btnAction2) && currentState != PlayerState.attack) //Check to see if the attack input is true (spacebar being pressed) and checks to make sure the player state is not already set to attack to prevent unintential double attack
        {
            playerStats.ReducePlayersHealth(1); //Reduce the players health by 1 (I will work out a damage formula at a later date)
            playerRaycast(); //This might need to occur at the same time as the attackCoroutine meaning a second check might need to occur
            StartCoroutine(attackCoroutine());
        }
        else if (currentState == PlayerState.walk || currentState == PlayerState.idle) //The player sprite will begin to walk if currentState is equal to walk
        {
            UpdateAnimationAndMove();
        }
    }


    void playerRaycast()
    {
        // See: https://docs.unity3d.com/ScriptReference/Physics2D.Raycast.html & https://answers.unity.com/questions/1499447/how-to-distinguish-which-layer-has-been-hit-by-ray.html
        requiredLayersMask = layermask1 | layermask2 | layermask3; //Logic copied from here: https://answers.unity.com/questions/1499447/how-to-distinguish-which-layer-has-been-hit-by-ray.html
        //int layerMask = LayerMask.GetMask("Plot"); // Use whatever mask you assigned to your ground tiles (plots) (Not used, but could be useful code)

        float distance = 1.0f; //Changes this to make the raycast go further?
        Vector2 direction = this.playerDirection == PlayerDirection.Left ? Vector2.left :
                            this.playerDirection == PlayerDirection.Right ? Vector2.right :
                            this.playerDirection == PlayerDirection.Up ? Vector2.up : Vector2.down;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, requiredLayersMask);

        Debug.Log("Player is facing: " + playerDirection);

        Debug.DrawRay(transform.position, direction, Color.red, 10, false);
        Debug.Log("Raycast: " + hit.transform);

        //If the raycast collider hits another collider, Do something such as farm work or talk to a NPC
        if (hit.collider != null)
        {

            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("BigResource"))
            {
                Debug.Log("Hit a Big Resource");

                if(hit.collider.tag == "rock")
                {
                    hit.transform.gameObject.GetComponent<BigRockScript>().DamageObject(hit);
                }
                else if(hit.collider.tag == "log")
                {
                    hit.transform.gameObject.GetComponent<BigLogScript>().DamageObject(hit);
                }
            }
            else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("SmallResource"))
            {
                Debug.Log("Hit a Small Resource");

                if (hit.collider.tag == "rock")
                {
                    hit.transform.gameObject.GetComponent<SmallRockScript>().DamageObject(hit);
                }
                else if (hit.collider.tag == "log")
                {
                    hit.transform.gameObject.GetComponent<SmallLogScript>().DamageObject(hit);
                }
            }
            else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Plot"))
            {
                cropManagerController = hit.transform.gameObject.GetComponent<CropManagerController>(); //This completes the reference to the CropManagerController when the raycast hits a gameObject that is CropManagerController
                cropManagerController.DoFarmWork(hit); //Depending on the state of the plot, this could be watering, planting a seed, cleaning rubbish etc }
            }
        }
    }


    //This method is called during the update() and will be responsible for updating the players direction to be up, down, left or right. The players direction can be access via playerDirection
    void checkPlayerDirection()
    {
        if (Mathf.Abs(change.x) > Mathf.Abs(change.y))
        {
            if (change.x > 0)
            {
                playerDirection = PlayerDirection.Right;
            }
            else if (change.x < 0)
            {
                playerDirection = PlayerDirection.Left;

            }
        }
        else if (Mathf.Abs(change.x) < Mathf.Abs(change.y))
        {
            if (change.y > 0)
            {
                playerDirection = PlayerDirection.Up;
            }
            else if (change.y < 0)
            {
                playerDirection = PlayerDirection.Down;
            }
        }
    }

    //This might not need to be an IEnumerator anymore since the attacking blendtree is no longer uses
    private IEnumerator attackCoroutine()
    {
        currentState = PlayerState.attack; //Sets the PlayerState to attack so the user can't trigger this coroutine a second time until it is finished, See "Attack Input" above for logic
        yield return null; //wait one frame
        StartCoroutine(playAnimationSimple(playerAnimSeconds));
        currentState = PlayerState.walk; //Sets the PlayerState to walk as the attack is now finished and we want to player to return to walking state so that the walking/Idle animation are used
    }

    void UpdateAnimationAndMove()
    {
        //If there is a change happening. This means the Vector3 does not equal zero (vector2.zero)
        if (change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", change.x); //This sets the moveX variable defined in the animatior equal to the player x position defined in the change variable by the player input
            animator.SetFloat("moveY", change.y); //This sets the moveY variable defined in the animatior equal to the player y position defined in the change variable by the player input
            animator.SetBool("moving", true); //When moving is true, the blend tree will transistion from the Idle state to the moving state. What i do not understand is how the code understands that
            //the sprite is moving therefore transition to moving. My guess is the animator.setbool function can detect this
        }
        else
        {
            animator.SetBool("moving", false); //When moving is false, the blend tree will transistion from the moving state to the idle state
        }
    }


    //The below method has been defined to allow me to call the player movement from other places as needed (Such as using onscreen buttons)
    void MoveCharacter()
    {
        if (playerMovementDisabled == false)
        {
            change.Normalize();     //This Normalize the player sprite speed when walking diagonal. (The sprite will move at the same speed as walking Up, down, left, right)

            //MovePosition is used to move the Rigidbody(player) to a new position
            //Transform.position is the Rigidbody current position
            //Change is the players x and y
            //time.delta time is the time that has passed since previous frame
            myRigidbody.MovePosition(transform.position + change * speed * Time.deltaTime);
        }
    }



    IEnumerator playAnimationSimple(float playerAnimSeconds)
    {
        if (equipmentManager.currentEquipment != null) //We only want the animations to play if an item is equip, otherwise an uneeded wait occurs
        {
            //If forget what this is. Refer to Playable API: https://docs.unity3d.com/Manual/Playables-Examples.html

            playerMovementDisabled = true; //Prevents the player from moving whilst an animation is occuring

            if (playerDirection == PlayerDirection.Down)
            {
                AnimationPlayableUtilities.PlayClip(GetComponent<Animator>(), clipDown, out playableGraph);
                Debug.Log("Down Animation has been played");
            }

            if (playerDirection == PlayerDirection.Left)
            {
                AnimationPlayableUtilities.PlayClip(GetComponent<Animator>(), clipLeft, out playableGraph);
                Debug.Log("Left Animation has been played");
            }

            if (playerDirection == PlayerDirection.Up)
            {
                AnimationPlayableUtilities.PlayClip(GetComponent<Animator>(), clipUp, out playableGraph);
                Debug.Log("Animation has been played");
            }

            if (playerDirection == PlayerDirection.Right)
            {
                AnimationPlayableUtilities.PlayClip(GetComponent<Animator>(), clipRight, out playableGraph);
                Debug.Log("Animation has been played");
            }

            yield return new WaitForSeconds(playerAnimSeconds);

            playableGraph.Destroy();
            Debug.Log("Animation has been stopped");

            playerMovementDisabled = false; //Allows the player to move again now that the animation has finished
        }
    }


    //This is a untitity function, it ia used to wait a certain amount of seconds
    public IEnumerator waitForTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }


    //The below was my attempt at a custom animator. The custom animator works, i have just decided to go along with the playable API
    /*
    public Sprite down0; //The first down animation
    public Sprite down1; //The second down animation
    public Sprite down2; //The third down animation
    public Sprite down3; //The final down animation

    IEnumerator customAnimator(float playerAnimSeconds)
    {
        animator.enabled = false;
    
        if (playerDirection == PlayerDirection.Down)
        {
            Sprite temp = this.transform.GetComponent<SpriteRenderer>().sprite; //The current animation on the player
    
    
            transform.GetComponent<SpriteRenderer>().sprite = down0;
            yield return new WaitForSeconds(playerAnimSeconds);
            transform.GetComponent<SpriteRenderer>().sprite = down1;
            yield return new WaitForSeconds(playerAnimSeconds);
            transform.GetComponent<SpriteRenderer>().sprite = down2;
            yield return new WaitForSeconds(playerAnimSeconds);
            transform.GetComponent<SpriteRenderer>().sprite = down3;
            yield return new WaitForSeconds(playerAnimSeconds);
            transform.GetComponent<SpriteRenderer>().sprite = down3;
            yield return new WaitForSeconds(playerAnimSeconds);
            transform.GetComponent<SpriteRenderer>().sprite = temp;
    
            Debug.Log(playerDirection);
        }
    
        //if (this.playerDirection == PlayerDirection1.Up)
        //{
        //Debug.Log(playerDirection);
        //}
    
        animator.enabled = true;
    
    }
    */


    //This is another way to play animations but the playAnimationSimple() does the trick.
    /*
    public void playAnimation()
    {
        playerUsingItem = true;

        playableGraph = PlayableGraph.Create();

        playableGraph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);

        var playableOutput = AnimationPlayableOutput.Create(playableGraph, "Animation", GetComponent<Animator>());

        // Wrap the clip in a playable

        var clipPlayable = AnimationClipPlayable.Create(playableGraph, clip);

        // Connect the Playable to an output

        playableOutput.SetSourcePlayable(clipPlayable);

        // Plays the Graph.

        playableGraph.Play();

        //yield return new WaitForSeconds(5.0f);

        Debug.Log("Animation has been played");

    }

    IEnumerator stopAnimation()
    {
        // Destroys all Playables and PlayableOutputs created by the graph.
        //playableGraph.Destroy();

        //StartCoroutine(waitForTime(5.0f));
        yield return new WaitForSeconds(0.33f);

        playableGraph.Stop();
        Debug.Log("Animation has been stopped");

        playerUsingItem = false;

    }
    */

    //Knockback is old code used from the original idea of a zelda style game. It is probably not needed anymore unless i add combat into the game.
    /*
    public void Knock(float knockBackTime)
    {
        StartCoroutine(KnockBackCo(knockBackTime)); //By Creating the knockback as a coroutine in the player, it allows the player object to monitor its own knockback
    }

    private IEnumerator KnockBackCo(float knockBackTime)
    {

        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(knockBackTime); //How long to wait
            myRigidbody.velocity = Vector2.zero;
            currentState = PlayerState.idle; //sets the player state machine to back to idle
            myRigidbody.velocity = Vector2.zero;
        }

    }
    */

}
