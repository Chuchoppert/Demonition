using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject Player;
    public float speedBullet = 2;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 40 || transform.position.x < -40)
        {
            Destroy(gameObject);
        }
        if (transform.position.y > 20 || transform.position.x < -20)
        {
            Destroy(gameObject);
        }
         //transform.LookAt(Player.transform.position); modo ultra hardcore
    }

    private void FixedUpdate()
    {
        transform.position += transform.forward * speedBullet * Time.fixedDeltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player.GetComponent<Controller>().HealthDemon -= 1.0f;
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Grab") || other.gameObject.CompareTag("Enemy")) //para que no se destruya con el collider de la mano del demonio :v
        {
            
        }
        else if (other.gameObject.CompareTag("ObjectDestroy"))
        {
            Destroy(gameObject);
        }
    }
}
