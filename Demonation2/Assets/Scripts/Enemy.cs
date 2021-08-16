using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject Player;
    //float HealthEnemy = 1;

    public float MovingSpeed = 2f;

    //GameObject Controller;
    public GameObject Chunk_Prefab;

    public GameObject bullet_Prefab;
    public float DistanceToShoot = 80f;
    public float shootInterval =1.5f;
    float distanceFromTarget;
    float shootTime;

    

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        Debug.DrawRay(gameObject.transform.position, gameObject.transform.forward * 100, Color.red);
        transform.LookAt(Player.transform);

        distanceFromTarget = Vector3.Distance(transform.position, Player.transform.position);
        shootControl();


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
        if (other.gameObject.CompareTag("Player")) //si toca al demon
        {
            Player.GetComponent<Controller>().HealthDemon -= 1.0f;

            Debug.Log("Hit");
        }
        else if (other.gameObject.CompareTag("Grab")) //para que no se destruya con el collider de la mano del demonio :v
        {
            
        }
        else if (other.gameObject.CompareTag("ObjectDestroy")) //Si se destruye con un chunk
        {
            
            GameObject Chunk = Instantiate<GameObject>(Chunk_Prefab);

            //para dos chunks
            //GameObject Chunk1 = Instantiate<GameObject>(Chunk_Prefab); 
            //Chunk1.transform.position = new Vector3 (transform.position.x + 3.5f, transform.position.y + 3.5f, transform.position.z); 

            Chunk.transform.position = transform.position;
            //para que no aparezcan los chunks mas alla de donde el personaje no puede ir 
            if (Chunk.transform.position.x < -19f || Chunk.transform.position.x > 5.8f) 
            {
                Destroy(Chunk);
            }
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void shootControl()
    {
        shootTime -= Time.deltaTime;
        if(shootTime < 0)
        {
            if(distanceFromTarget < DistanceToShoot)
            {
                shootTime = shootInterval;
                GameObject bullet = Instantiate<GameObject>(bullet_Prefab);
                bullet.transform.position = transform.position;
                bullet.transform.LookAt(Player.transform.position);
            }
        }
    }
}
