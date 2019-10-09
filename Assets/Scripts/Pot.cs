using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{

    private Animator animator; //Reference to the animator

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); //Complete the refernce to the animator
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Smash()
    {
        animator.SetBool("smash", true); //sets the smash animation for the pot object
        StartCoroutine(breakCo()); 
    }

    IEnumerator breakCo()
    {
        yield return new WaitForSeconds(.33f);
        this.gameObject.SetActive(false); //This sets the pot to inactive / hidden
    }

}
