using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    // references the movement manager script, making this script
    // significantly easier to write
    public MovementManager controller;

    public float runspeed = 40f;
    float Horizontalmove = 0f;

    bool jump = false;
    
    // Update is called once per frame
    void Update()
    {
        // left + right / a + d will move the player horizontal
        Horizontalmove = Input.GetAxisRaw("Horizontal") * runspeed;

        // Jump is false by default, but turns on when space is pressed
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    void FixedUpdate ()
    {
        // Move the Thing, false says crouching is a no go, sets just to false after the player jumps 
        // so that a loop is not created.
        controller.Move(Horizontalmove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
