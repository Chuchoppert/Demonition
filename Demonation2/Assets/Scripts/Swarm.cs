using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swarm : MonoBehaviour
{
    public float MovingSpeed= 5f;
    public AudioClip effect;
    bool reached = false;
    public float standbyTime;


    private void Start()
    {
        standbyTime = 0;
    }

    private void Update()
    {
        if (transform.position.x <= -28f && !reached)
        {
            transform.Translate(MovingSpeed * 2 * Time.deltaTime, 0, 0, Space.World);
        }

        if (transform.position.x >= -28f)
        {
            //Debug.Log("Toggle reached");
            reached = true;
        }

        if (reached)
        {
            standbyTime += Time.deltaTime;
        }

        if (standbyTime >= 1.2f)
        {
            MovingSpeed = 25f;
            transform.Translate(MovingSpeed * 2 * Time.deltaTime, 0, 0, Space.World);
        }

        if (transform.position.x > 36f)
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
