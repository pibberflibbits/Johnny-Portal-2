using UnityEngine;
using System.Collections;

public class playermovement : MonoBehaviour
{
	private Vector3 targetVelocity;
	private float move;
	public Rigidbody2D rb;
	public float MovementSmoothing;
	private Vector3 Velocity = Vector3.zero;
	public bool isgrounded;
	public bool israiled;
	public Collider2D coll;
	public float currentdirection;

	public float jumps;

	public float jdefault;

	void Update()
    {
	
		// resets movement to 0 if no key is pressed
		move = 0;

		// sets default # of jumps incase we want more than 1 jump later.
		jumps = jdefault;
		jdefault = 1;

		if (Input.GetKey("a"))
		{
			move = -1;
			currentdirection = -1;
		}

		if (Input.GetKey("d"))
		{
			move = 1;
			currentdirection = 1;
		}


		// allows player to jump if jump is greater than 1
		if (isgrounded == true)
		{
			if (Input.GetKeyDown("space"))
			{
				if (jumps > 0)
				{
					rb.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
					jumps -= jumps;
				}
			}
		}

		if (isgrounded == false)
		{
			jumps = 0;
		}
   
		if(coll.IsTouchingLayers(LayerMask.GetMask("Rail")))
        {
			israiled = true;

            if(Input.GetKey("left shift")) 
            {
				GameObject[] rail = GameObject.FindGameObjectsWithTag("Rail");
				foreach (GameObject r in rail)
					r.GetComponent<BoxCollider2D>().isTrigger = false;
					// moves player automatically when holding x on a rail.
				move = currentdirection;
				jumps = 1;				
			}

			if (!Input.GetKey("left shift"))
			{
				GameObject[] rail = GameObject.FindGameObjectsWithTag("Rail");
				foreach (GameObject r in rail)
					r.GetComponent<BoxCollider2D>().isTrigger = true;
					
			}

		}

		if(!coll.IsTouchingLayers(LayerMask.GetMask("Rail")))
        {
			israiled = false;
        }

		targetVelocity = new Vector2(move * 10f, rb.velocity.y);
		rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref Velocity, MovementSmoothing);
	}

	void OnCollisionEnter2D(Collision2D theCollision)
	{
		if (theCollision.gameObject.name == "Ground" || theCollision.gameObject.name == "Rail")
		{
			isgrounded = true;
		}
	}

    void OnCollisionExit2D(Collision2D theCollision)
	{
		if (theCollision.gameObject.name == "Ground")
		{
			isgrounded = false;
		}
	}
}