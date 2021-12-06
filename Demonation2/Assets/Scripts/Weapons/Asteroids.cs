using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    [Header("Actions")]
    public bool ThisWasPicked = false;
    public bool ThisWasLaunched = false;
    public float ForceLaunch = 25.0f;
    public float MovingSpeedAsteroid = 4f;

    GameObject MenuManager;


    void Start()
    {
        MenuManager = GameObject.FindWithTag("MenuManag");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ThisWasPicked == false)
        {

            this.transform.Translate((new Vector3(0.5f, 1, 0) * -MovingSpeedAsteroid * 5 * Time.deltaTime) * Player_DemonLord.slowMotionFactor, Space.Self); 
            if (transform.position.x < -20f)
            {
                Destroy(this.gameObject);
            }
        }

        if (ThisWasLaunched == true && ThisWasPicked == true)
        {
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            this.gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

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

    private void OnCollisionEnter(Collision collision)
    {
        //Destroy enemy and add 1 point to score
                //Enemi_Small                 O             Enemy_Big
        if ((collision.gameObject.layer == 10 || collision.gameObject.layer == 11) && this.ThisWasPicked == true) //si el asteroide fue lanzado y toco enemy, sumar punto
        {
            MenuManager.GetComponent<HUDManager>().ScoreInGame += 1;
            Destroy(this.gameObject);
            Destroy(collision.gameObject);          
        }

        if ((collision.gameObject.layer == 10 || collision.gameObject.layer == 11) && this.ThisWasPicked == true) //si el asteroide no fue lanzado y toco enemy, no sumar puntos
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);                    
        }       
    }
}
