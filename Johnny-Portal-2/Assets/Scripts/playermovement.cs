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
	public Collider2D coll;
	float CurrentVelocity;
	float LastVelocity;
	float njumps=0; 
	float TargetSpeed = 1f;
	void FixedUpdate()
	{

		CurrentVelocity = rb.velocity.y;
		Debug.Log(rb.velocity.y);

		if(LastVelocity == CurrentVelocity)
		{
			isgrounded=true;
		}

		if(LastVelocity != CurrentVelocity)
		{
			isgrounded=false; 
		}

	}
	
	void Update()
    {
		// resets movement to 0 if no key is pressed
		move = 0;

		if (Input.GetKey("a"))
		{
			move = -TargetSpeed;
		}

		if (Input.GetKey("d"))
		{
			move = TargetSpeed;
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
   
		if(coll.IsTouchingLayers(LayerMask.GetMask("Rail")))
        {
            if(Input.GetKey("left shift")) 
            {
				GameObject[] rail = GameObject.FindGameObjectsWithTag("Rail");
				foreach (GameObject r in rail)
					r.GetComponent<BoxCollider2D>().isTrigger = false;
			if(TargetSpeed < 2f)
				TargetSpeed = TargetSpeed + .1f;  
			}
		}


			if (!Input.GetKey("left shift"))
			{
				GameObject[] rail = GameObject.FindGameObjectsWithTag("Rail");
				foreach (GameObject r in rail)
					r.GetComponent<BoxCollider2D>().isTrigger = true;
					
			}

		targetVelocity = new Vector2(move * 10f, rb.velocity.y);
		rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref Velocity, MovementSmoothing);
		LastVelocity = CurrentVelocity;
	}
}

