using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab_Throw : MonoBehaviour
{
    public GameObject ColocadorChunkDemon;
    public GameObject Hand;
    private GameObject PickedObject = null;
    public bool IsReadyLauch;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3( ColocadorChunkDemon.transform.position.x, ColocadorChunkDemon.transform.position.y, ColocadorChunkDemon.transform.position.z + 5.0f);
        transform.position = ColocadorChunkDemon.transform.position;
        transform.rotation = ColocadorChunkDemon.transform.rotation;

        if(PickedObject != null)
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

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("ObjectDestroy"))
        {
            if (Input.GetKey(KeyCode.Space) && PickedObject == null)
            {                
                other.GetComponent<Rigidbody>().isKinematic = true;

                IsReadyLauch = true;

                other.transform.position = Hand.transform.position;
                //other.transform.position = gameObject.transform.position;
                other.gameObject.transform.SetParent(Hand.gameObject.transform);
                //other.gameObject.transform.SetParent(gameObject.transform);

                PickedObject = other.gameObject;            
            }
        }
    }
}
