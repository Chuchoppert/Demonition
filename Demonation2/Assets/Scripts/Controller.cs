using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Controller : MonoBehaviour
{

	public float speed = 10.0f;
	private Rigidbody rb;
	private float vertical, horizontal;

	public float HealthDemon = 2.0f;
	public ParticleSystem explosion;
	public AudioSource explosionSound;

	public Animator CH_Demon_Anim;
	//GrabChunk | isReady | Launched

	//GameObject Forward_Script;
	float AnimChunk_Time = 0;
	public float IntervalChunk_Time = 0.49f;

	// Use this for initialization
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		CH_Demon_Anim = GetComponent<Animator>();
		//Forward_Script = GameObject.FindWithTag("ObjectDestroy"); 
	}

	// Update is called once per frame
	void Update()
	{
		transform.position = new Vector3(transform.position.x, transform.position.y, 27.7f);
		transform.rotation = Quaternion.Euler(-90, 0, 90);

		// vertical axis is either up or down or w and s on the keyboard, among others
		if (Input.GetAxisRaw("Vertical") != 0)
		{
			vertical = Input.GetAxis("Vertical") * speed;

			// constrain movement within the bounds of the camera
			if (transform.position.y < -17f)
			{
				transform.position = new Vector3(transform.position.x, -17f, transform.position.z);
			}
			if (transform.position.y > 17f)
			{
				transform.position = new Vector3(transform.position.x, 17f, transform.position.z);
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
			if (transform.position.x < -25f)
			{
				transform.position = new Vector3(-25f, transform.position.y, transform.position.z);
			}
			if (transform.position.x > -1f)
			{
				transform.position = new Vector3(-1f, transform.position.y, transform.position.z);
			}
		}
		else
		{
			horizontal = 0f;
		}

		// set rigidbody's velocity to our input
		rb.velocity = new Vector3(horizontal, vertical, 0);

		if (HealthDemon == 0)
		{

			Invoke("DeadAnim", 0f);

			gameObject.SetActive(false);
		}

		//AnimChunk_Time += Time.deltaTime;
		//AnimChunk_Time = IntervalChunk_Time;
		if (FowardMovement.ReadyToLaunch == true) //Forward_Script.GetComponent<FowardMovement>().ReadyToLaunch == true

		{
			AnimChunk_Time += Time.deltaTime;
			if (AnimChunk_Time < 0.49f)
            {				
				CH_Demon_Anim.SetBool("GrabChunk", true);
			}
            else
            {				
				CH_Demon_Anim.SetBool("GrabChunk", false);
				CH_Demon_Anim.SetBool("isReady", true);
			}
			
        }
        else
        {
			CH_Demon_Anim.SetBool("isReady", false);
			CH_Demon_Anim.SetTrigger("Launched");	
			CH_Demon_Anim.ResetTrigger("Launched");
			AnimChunk_Time = 0;
		}
       
	}

	void DeadAnim()
    {
		explosionSound.Play();
		explosion.transform.position = transform.position;
		Instantiate<ParticleSystem>(explosion);
		Debug.Log("Shiiiit");
    }
}
