using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FowardMovement : MonoBehaviour
{
    GameObject Enemies;

    public float ForceLaunch = 2.0f;

    GameObject Grab_Throw; //ref1
    

    public bool ReadyToLaunch;
    // Start is called before the first frame update
    void Start()
    {      
        ReadyToLaunch = false;
        Enemies = GameObject.FindWithTag("Enemy");
        Grab_Throw = GameObject.FindWithTag("Grab"); //ref2
    }

    // Update is called once per frame
    void Update()
    {
        if (Grab_Throw.GetComponent<Grab_Throw>().IsReadyLauch == true) //ref final
        {
            ReadyToLaunch = true;
        }
        else
        {
            ReadyToLaunch = false;
        }

        if (ReadyToLaunch == true)
        {
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(ForceLaunch, 0.0f, 0.0f);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}