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
	public bool israil; 
	public Collider2D coll;
	float CurrentYVelocity;
	float LastYVelocity;
	float njumps=0; 
	public float PlayerTargetSpeed = 1f;
	bool positivemove;
	bool lastpositivemove;
	int UpdateSpeedChecker = 1; 
	private float raildirection; 
	void FixedUpdate()
	{

		CurrentYVelocity = rb.velocity.y;	
		Debug.Log(CurrentYVelocity);

		if(LastYVelocity == CurrentYVelocity)
		{
			isgrounded=true;
		}

		if(LastYVelocity != CurrentYVelocity)
		{
			isgrounded=false;
		}

	}
	
	void Update()
    {
		// resets movement to 0 if no key is pressed
		// makes MovementSmoothing default to 0.5f 
		move = 0;

		if (Input.GetKey("a"))
		{
			move = -PlayerTargetSpeed;
			raildirection = -PlayerTargetSpeed;
		}

		if (Input.GetKey("d"))
		{
			move = PlayerTargetSpeed;
			raildirection = PlayerTargetSpeed;
		}

		// allows player to jump if jump is greater than 1
		if (isgrounded == true)
		{
			njumps = 1;   
			
			if (Input.GetKeyDown("space") & (njumps > 0))
			{	
				rb.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
				njumps = 0;
			}
		}

		if (isgrounded == false)
		{
			njumps = 0;
		}
  
	// this whole section is an absolute mess, if you have any idea how to fix this, please do.
   // It works completely fine but like, wow!
   
		if (coll.IsTouchingLayers(LayerMask.GetMask("Rail")))
			{
				if(Input.GetKey("left shift"))
					{
					if(CurrentYVelocity < 0)
						{
							GameObject[] rail = GameObject.FindGameObjectsWithTag("Rail");
							foreach (GameObject r in rail)
								r.GetComponent<BoxCollider2D>().isTrigger = false;
								israil = true; 
								
								// all this does is let you pass through the rail when your jumping up but land on it when your falling down
						}
				
					if(PlayerTargetSpeed < 2 & (UpdateSpeedChecker > 0 & (isgrounded = true)))
						{
							PlayerTargetSpeed = PlayerTargetSpeed + .1f;
							UpdateSpeedChecker--; 
							// this makes sure that speed only increases once per rail
							// the UpdateSpeedChecker thing is really janky
						}
					}
		    }

		if (israil == false)
		{
			MovementSmoothing = 0.5f;
		}
		
		if (israil == true)
		{
			MovementSmoothing = 0.3f; 
		}

		if(rb.velocity.x < 1.3f & (rb.velocity.x > -0) | (rb.velocity.x > -1.3f & (rb.velocity.x < -0) | (rb.velocity.x == 0)))
			{
								
		     	GameObject[] rail = GameObject.FindGameObjectsWithTag("Rail");
				foreach (GameObject r in rail)
					r.GetComponent<BoxCollider2D>().isTrigger = true;
					PlayerTargetSpeed = 1f; 
			}
				
			

        // insures UpdateSpeedChecker is only increased once per rail. 
		if(!coll.IsTouchingLayers(LayerMask.GetMask("Rail")) & (UpdateSpeedChecker < 1))
		{
			UpdateSpeedChecker++; 
		}


		if (!Input.GetKey("left shift"))
		{
			GameObject[] rail = GameObject.FindGameObjectsWithTag("Rail");
			foreach (GameObject r in rail)
				r.GetComponent<BoxCollider2D>().isTrigger = true;
			israil = false; 
		}

        // resests movement speed if you change directoins
		positivemove = move > 0; 
			
		if (lastpositivemove != positivemove)
		{
			PlayerTargetSpeed = 1f; 
		}

		targetVelocity = new Vector2(move * 10f, rb.velocity.y);
		rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref Velocity, MovementSmoothing);
		LastYVelocity = CurrentYVelocity;
		lastpositivemove = positivemove; 
	}
	
}


