using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FowardMovement : MonoBehaviour
{
    public float ForceLaunch = 2.0f;
    private GameObject PickedObject = null;
    public bool IsReadyLauch;
    public static bool ReadyToLaunch; 

    GameObject MenuManager;

    public float MovingSpeedChunk = 1f;

    public AudioSource S_grab;
    public AudioSource S_throw;

    void Start()
    {
        MenuManager = GameObject.FindWithTag("MenuManag");
    }


    void Update()
    {
        ReadyToLaunch = IsReadyLauch;

        if (IsReadyLauch == false)
        {
            transform.Translate(-MovingSpeedChunk * 2 * Time.deltaTime, 0, 0, Space.World);
            if (transform.position.x < -50f)
            {
                Destroy(gameObject);
            }
        }


        if (IsReadyLauch == true)
        {
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(ForceLaunch, 0.0f, 0.0f);
        }

        if (PickedObject != null)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                IsReadyLauch = false;
                S_throw.Play();

                PickedObject.GetComponent<Rigidbody>().isKinematic = false;

                PickedObject.gameObject.transform.SetParent(null);

                PickedObject = null;
                this.gameObject.layer = 6;
            }
        }

    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
                MenuManager.GetComponent<MenuManeger>().score += 1;
               
                Destroy(gameObject);        
        }

        if (other.gameObject.CompareTag("Grab"))
        {
            if (Input.GetKey(KeyCode.Space) && PickedObject == null)
            {
                S_grab.Play();
                IsReadyLauch = true;

                gameObject.transform.position = other.transform.position;
                gameObject.transform.SetParent(other.transform);

                PickedObject = gameObject;
                //this.gameObject.layer = 0;
            }
        }
    }
}
