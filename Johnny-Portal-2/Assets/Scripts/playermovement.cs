using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    public MovementManager controller;

    public float runspeed = 40f;
    float Horizontalmove = 0f;

    bool jump = false;
    
    // Update is called once per frame
    void Update()
    {
        Horizontalmove = Input.GetAxisRaw("Horizontal") * runspeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }


    }

    void FixedUpdate ()
    {
        // Move the Thing, false says crouching is a no go
        controller.Move(Horizontalmove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
