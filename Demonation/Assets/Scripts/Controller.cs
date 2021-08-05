using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{

	public float speed = 10.0f;
	private Rigidbody rb;
	private float vertical, horizontal;


	// Use this for initialization
	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{

		// vertical axis is either up or down or w and s on the keyboard, among others
		if (Input.GetAxisRaw("Vertical") != 0)
		{
			vertical = Input.GetAxis("Vertical") * speed;

			// constrain movement within the bounds of the camera
			if (transform.position.y < -5.7f)
			{
				transform.position = new Vector3(transform.position.x, -5.7f, transform.position.z);
			}
			if (transform.position.y > 8.2)
			{
				transform.position = new Vector3(transform.position.x, 8.2f, transform.position.z);
			}
		}
		else
		{
			vertical = 0f;
		}

		// horizontal axis is either left or right or a and d on the keyboard, among others
		if (Input.GetAxisRaw("Horizontal") != 0)
		{
			horizontal = Input.GetAxis("Horizontal") * speed;

			// constrain movement within the bounds of the camera
			if (transform.position.x < -12.5f)
			{
				transform.position = new Vector3(-12.5f, transform.position.y, transform.position.z);
			}
			if (transform.position.x > -2f)
			{
				transform.position = new Vector3(-2f, transform.position.y, transform.position.z);
			}
		}
		else
		{
			horizontal = 0f;
		}

		// set rigidbody's velocity to our input
		rb.velocity = new Vector3(horizontal, vertical, 0);
	}


}
