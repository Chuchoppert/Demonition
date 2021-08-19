using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject Player;
  
    public GameObject Chunk_Prefab;

    public GameObject bullet_Prefab;
    public float DistanceToShoot = 80f;
    public float shootInterval = 1.5f;
    float distanceFromTarget;
    float shootTime;

    public float[] DistanceToStop = { 7f, 11f };
    public float MovingSpeedEnemy = 2f;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        Debug.DrawRay(gameObject.transform.position, gameObject.transform.forward * 100, Color.red);
        distanceFromTarget = Vector3.Distance(transform.position, Player.transform.position);
        shootControl();
        if (Player.gameObject.activeSelf == true)
        {
            transform.LookAt(Player.transform);
        }

        //foreach (GameObject enemy in EnemySpawner.InstancesEnemies)
        //{   
            if (transform.position.x > Random.Range(DistanceToStop[0], DistanceToStop[1]))//ARREGLAR: QUE SE PAREN ALEATORIAMENTE Y NO TODOS EN EL MISMO PUNTO
            {
                transform.Translate(-MovingSpeedEnemy * 2 * Time.deltaTime, 0, 0, Space.World);
            }
        //}
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ObjectDestroy"))
        {
            GameObject Chunk = Instantiate<GameObject>(Chunk_Prefab);
            Chunk.transform.position = transform.position;
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
                bullet.transform.position = new Vector3(transform.position.x + 7.5f, transform.position.y, transform.position.z);
                bullet.transform.LookAt(Player.transform.position);
            }
        }
    }
}
