using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    GameObject Player;

    public float ForceLaunch = 2.0f;
    private GameObject PickedObject = null;
    public bool IsReadyLauch;
    public bool isLaunched;

    GameObject MenuManager;

    public float MovingSpeedAsteroid = 1f;

    void Start()
    {
        this.isLaunched = false;
        MenuManager = GameObject.FindWithTag("MenuManag");
        Invoke("AddCollider", 1f);
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Player == null)
        {
            Destroy(gameObject, 0.5f);
        }
        ChunksController.ReadyToLaunch = IsReadyLauch;

        if (IsReadyLauch == false && this.isLaunched == false)
        {
            transform.Translate(new Vector3(0.5f,1,0) * -MovingSpeedAsteroid * 5 * Time.deltaTime, Space.Self);
            if (transform.position.y < -20f)
            {
                Destroy(gameObject);
            }
        }
        else if (IsReadyLauch == true)
        {
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(ForceLaunch, 0.0f, 0.0f);
        }
        if (PickedObject != null)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                IsReadyLauch = false;
                this.isLaunched = true;
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
            if (this.gameObject.layer == 7)
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

                this.gameObject.layer = 7;
                PickedObject = gameObject;
                PickedObject.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }
}
