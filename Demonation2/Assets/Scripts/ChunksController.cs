using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunksController : MonoBehaviour
{
    GameObject Player;

    public float ForceLaunch = 2.0f;
    private GameObject PickedObject = null;
    public bool IsReadyLauch;
    public static bool ReadyToLaunch; 

    GameObject MenuManager;

    public float MovingSpeedChunk = 1f;


    void Start()
    {
        MenuManager = GameObject.FindWithTag("MenuManag");
        Invoke("AddCollider", 1f);
        Player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (Player == null)
        {
            Destroy(gameObject, 0.5f);
        }
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

                PickedObject.GetComponent<Rigidbody>().isKinematic = false;

                PickedObject.gameObject.transform.SetParent(null);

                PickedObject = null;
            }
        }
    }

    void AddCollider()
    {
        this.gameObject.AddComponent<BoxCollider>();
        this.gameObject.GetComponent<BoxCollider>().size = new Vector3(3.6f, 4, 4);
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            MenuManager.GetComponent<MenuManeger>().score += 1;
            if(this.gameObject.layer == 6)
            {
                Destroy(this.gameObject);
                Destroy(other.gameObject, 0.1f);               
            }          
        }

        if (other.gameObject.CompareTag("Grab"))
        {
            if (Input.GetKey(KeyCode.Space) && PickedObject == null)
            {
                IsReadyLauch = true;

                gameObject.transform.position = other.transform.position;
                gameObject.transform.SetParent(other.transform);

                this.gameObject.layer = 6;
                PickedObject = gameObject;
            }
        }
    }
}
