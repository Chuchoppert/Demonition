using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunksController : MonoBehaviour
{
    [Header("Actions")]
    public bool ThisWasPicked = false;
    public bool ThisWasLaunched = false;
    public float ForceLaunch = 25.0f;
    public float MovingSpeedChunk = 5f;

    GameObject MenuManager;

    void Start()
    {
        MenuManager = GameObject.FindWithTag("MenuManag");
        Physics.IgnoreLayerCollision(12, 10, true);
        Physics.IgnoreLayerCollision(12, 11, true);
    }

    void Update()
    {
        if (ThisWasPicked == false)
        {
            Physics.IgnoreLayerCollision(12, 10, false);
            Physics.IgnoreLayerCollision(12, 11, false);
           
            this.transform.Translate(-MovingSpeedChunk * 2 * Time.deltaTime, 0, 0, Space.World);

            if (transform.position.x < -50f)
            {
                Destroy(this.gameObject);
            }
        }

        if (ThisWasLaunched == true && ThisWasPicked == true)
        {
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            
            this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(ForceLaunch, 0, 0), ForceMode.Impulse);
            if (transform.position.x > 50f)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void isReadyToLaunch()
    {
        this.ThisWasPicked = true;
    }

    public void isLaunched()
    {
        this.ThisWasLaunched = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        //Destroy enemy and add 1 point to score
        //Enemi_Small                 O             Enemy_Big
        if ((collision.gameObject.layer == 10 || collision.gameObject.layer == 11) && this.ThisWasPicked == true)
        {
            MenuManager.GetComponent<HUDManager>().ScoreInGame += 1;
            Destroy(this.gameObject);
            Destroy(collision.gameObject, 0.05f);
        }
    }
}