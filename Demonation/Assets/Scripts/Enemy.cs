using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject Player;

    public float MovingSpeed = 2f;
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
        Player.SetActive(false);
        Debug.Log("Hit");
    }
}
