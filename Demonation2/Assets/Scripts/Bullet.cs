using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speedBullet = 2;
    GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 40 || transform.position.x < -60)
        {
            Destroy(gameObject);
        }
        if (transform.position.y > 20 || transform.position.x < -20)
        {
            Destroy(gameObject);
        }
        if(Player != null && HardcoreMODE.isHardcore == true)
        {
            transform.LookAt(Player.transform.position);
        }

    }
    private void FixedUpdate()
    {
        transform.position += transform.forward * speedBullet * Time.fixedDeltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {        
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("ObjectDestroy"))
        {
            Destroy(gameObject);
        }
    }
}
