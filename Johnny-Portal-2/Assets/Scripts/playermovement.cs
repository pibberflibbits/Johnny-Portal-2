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

	void Update()
    {
		move = 0;

		if (Input.GetKey("a"))
		{
			move = -1;
		}

		if (Input.GetKey("d"))
		{
			move = 1;
		}

		if (isgrounded == true)
		{
			if (Input.GetKeyDown("space"))
			{
				rb.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
			}
		}

		if(coll.IsTouchingLayers(LayerMask.GetMask("Rail")))
        {
			israiled = true;

            if(Input.GetKey("x"))
            {
				GameObject[] rail = GameObject.FindGameObjectsWithTag("Rail");
				foreach (GameObject r in rail)
					r.GetComponent<BoxCollider2D>().isTrigger = false;
			}
			if (!Input.GetKey("x"))
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
		if (theCollision.gameObject.name == "Ground")
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