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
	float CurrentYVelocity;
	float LastYVelocity;
	float njumps=0; 
	public float PlayerTargetSpeed = 1f;
	bool positivemove;
	bool lastpositivemove;
	int UpdateSpeedChecker = 1; 
	void FixedUpdate()
	{

		CurrentYVelocity = rb.velocity.y;	
		Debug.Log(rb.velocity.x);

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
		move = 0;

		if (Input.GetKey("a"))
		{
			move = -PlayerTargetSpeed;
		}

		if (Input.GetKey("d"))
		{
			move = PlayerTargetSpeed;
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
	
				if(rb.velocity.x < 1.3f & (rb.velocity.x > -0) | (rb.velocity.x > -1.3f & (rb.velocity.x < -0) | (rb.velocity.x == 0)))
				{
					foreach (GameObject r in rail)
						r.GetComponent<BoxCollider2D>().isTrigger = true; 
				}
				


				if(PlayerTargetSpeed < 2 & (UpdateSpeedChecker > 0))
				{
					PlayerTargetSpeed = PlayerTargetSpeed + .1f;
					UpdateSpeedChecker--; 
				}

			}
		} 

		if(!coll.IsTouchingLayers(LayerMask.GetMask("Rail")) & (UpdateSpeedChecker < 1))
		{
			UpdateSpeedChecker++; 
		}


		if (!Input.GetKey("left shift"))
		{
			GameObject[] rail = GameObject.FindGameObjectsWithTag("Rail");
			foreach (GameObject r in rail)
				r.GetComponent<BoxCollider2D>().isTrigger = true;
		}

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


