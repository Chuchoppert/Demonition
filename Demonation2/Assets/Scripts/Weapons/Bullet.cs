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
        Physics.IgnoreLayerCollision(14, 13, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 45 || transform.position.x < -60)
        {
            Destroy(gameObject);
        }
        if (transform.position.y > 20 || transform.position.y < -20)
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
        //transform.position += transform.forward * speedBullet * Time.fixedDeltaTime;
        transform.position += transform.forward * speedBullet * Time.fixedDeltaTime; //Modifica esta linea para que sea mas lento.
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8 || other.gameObject.layer == 12 || other.gameObject.layer == 9)
        {
            Destroy(gameObject);
        }


    }
}
