using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject Player;
    //float HealthEnemy = 1;

    public float MovingSpeed = 2f;

    GameObject Controller;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    void Update()
    {

        // if enemy goes past the left edge, destroy it
        if (transform.position.x < -40)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.Rotate(Random.Range(10, 30) * Time.deltaTime, 0, 0);
            transform.Translate(-MovingSpeed * 2 * Time.deltaTime, 0, 0, Space.World);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Player.SetActive(false);
            Player.GetComponent<Controller>().HealthDemon -= 1.0f;
            Destroy(gameObject);
            Debug.Log("Hit");
        }
        else if (other.gameObject.CompareTag("Grab"))
        {
            Debug.Log("Ups");
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
