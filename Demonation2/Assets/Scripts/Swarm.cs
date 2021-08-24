using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swarm : MonoBehaviour
{
    public float MovingSpeed= 2f;
    public AudioClip effect;
    bool reached = false;
    public float standbyTime;


    private void Start()
    {
        standbyTime = 0;
    }

    private void Update()
    {
        if (transform.position.x <= -23f && !reached)
        {
            transform.Translate(MovingSpeed * 2 * Time.deltaTime, 0, 0, Space.World);          
        }

        if (transform.position.x >= -24.4f)
        {
            //Debug.Log("Toggle reached");
            reached = true;
        }

        if (reached)
        {
            standbyTime += Time.deltaTime;
        }

        if (standbyTime >= 2)
        {
            MovingSpeed = 4f;
            transform.Translate(MovingSpeed * 2 * Time.deltaTime, 0, 0, Space.World);
        }

        if (transform.position.x > 33f)
        {
            Destroy(gameObject);
        }
        //Debug.Log(standbyTime);
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(effect, new Vector3(0, 0, 0));
            Destroy(gameObject);
        }
    }
}
