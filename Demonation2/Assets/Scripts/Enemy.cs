using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(AudioSource))]
public class Enemy : MonoBehaviour
{
    GameObject Player;
 
    public GameObject Chunk_Prefab;

    public GameObject bullet_Prefab;
    public float DistanceToShoot = 80f;
    public float shootInterval = 1.5f;
    float distanceFromTarget;
    float shootTime;

    public float enemyLeftBorder = 8;
    public float enemyRightBorder = 28;
    private float DistanceToStop;
    public float MovingSpeedEnemy = 2f;

    GameObject soundSource;
    bool isChunkCollision;

    void Start()
    {
        soundSource = GameObject.FindGameObjectWithTag("SdEnemies");
        gameObject.GetComponent<DeadAnim>().SoundSource = soundSource.GetComponent<AudioSource>();
        Player = GameObject.FindWithTag("Player");

        DistanceToStop = Random.Range(enemyLeftBorder, enemyRightBorder);
        isChunkCollision = false;
    }

    void Update()
    {
        Debug.DrawRay(gameObject.transform.position, gameObject.transform.forward * 100, Color.red);
        if (Player != null)
        {
            distanceFromTarget = Vector3.Distance(this.transform.position, Player.transform.position);

            transform.LookAt(Player.transform);
        }       
        if (Player == null)
        {
            Destroy(gameObject, 0.5f);
        }
        shootControl();

        if (gameObject.transform.position.x > DistanceToStop)
        {
            gameObject.transform.Translate(-MovingSpeedEnemy * 2 * Time.deltaTime, 0, 0, Space.World);         
        }
    }

    public void OnDestroy()
    {
        if(isChunkCollision == true)
        {
            GameObject Chunk = Instantiate<GameObject>(Chunk_Prefab);
            Chunk.transform.position = transform.position;
        }      
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ObjectDestroy") && other.gameObject.layer == 6)
        {
            gameObject.GetComponent<DeadAnim>().isDead = true;
            isChunkCollision = true;
            Destroy(gameObject, 0.01f);   
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
                if (Player != null)
                {
                    bullet.transform.LookAt(Player.transform.position);
                } 
            }
        }
    }
}
