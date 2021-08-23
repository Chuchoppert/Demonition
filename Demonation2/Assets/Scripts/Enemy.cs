using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(AudioSource))]
public class Enemy : MonoBehaviour
{
    GameObject Player;
   // public Transform Player;
  
    public GameObject Chunk_Prefab;

    public GameObject bullet_Prefab;
    public float DistanceToShoot = 80f;
    public float shootInterval = 1.5f;
    float distanceFromTarget;
    float shootTime;

    public float DistanceToStop = 12f;
    public float MovingSpeedEnemy = 2f;
    //public AudioClip effect1;

    GameObject soundSource;

    /*GameObject[] EnemiesTotal;
    GameObject[] EnemiesCheck;
    int iFor;
    int CountEnemies;*/

    void Start()
    {
        soundSource = GameObject.FindGameObjectWithTag("SdEnemies");
        gameObject.GetComponent<DeadAnim>().SoundSource = soundSource.GetComponent<AudioSource>();
        Player = GameObject.FindWithTag("Player");

        //EnemiesCheck = GameObject.FindGameObjectsWithTag("Enemy");
    }

    void Update()
    {
        Debug.DrawRay(gameObject.transform.position, gameObject.transform.forward * 100, Color.red);

        distanceFromTarget = Vector3.Distance(this.transform.position, Player.transform.position);
        transform.LookAt(Player.transform);     
        
        if (Player.gameObject.activeSelf == false)
        {
            Destroy(gameObject, 0.5f);
        }
        shootControl();

        //Debug.Log(DistanceToStop);

        /*CountEnemies = EnemySpawner.AmountEnemies;

            for (iFor = 0; iFor <= CountEnemies; iFor++)
            {
                if (EnemiesCheck[iFor].gameObject.transform.position.x > Random.Range(DistanceToStop[0], DistanceToStop[1]))//TODO: QUE SE PAREN ALEATORIAMENTE Y NO TODOS EN EL MISMO PUNTO
                {
                    EnemiesCheck[iFor].gameObject.transform.Translate(-MovingSpeedEnemy * 2 * Time.deltaTime, 0, 0, Space.World);
                }
            }
        if(iFor >= CountEnemies)
        {
            iFor = 0;
        }*/

        if (gameObject.transform.position.x > DistanceToStop)//TODO: QUE SE PAREN ALEATORIAMENTE Y NO TODOS EN EL MISMO PUNTO
        {
            gameObject.transform.Translate(-MovingSpeedEnemy * 2 * Time.deltaTime, 0, 0, Space.World);
        }
        else
        {
            DistanceToStop = Random.Range(12f, 22f);
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ObjectDestroy"))
        {


            //if(other.gameObject.layer == 6)
            //{
            gameObject.GetComponent<DeadAnim>().isDead = true;
            Destroy(gameObject, 0.01f);
            GameObject Chunk = Instantiate<GameObject>(Chunk_Prefab);
            Chunk.transform.position = transform.position;
            //Debug.Log("Sound");


            // gameObject.GetComponent<DeadAnim>().isDead = false;
            // }
            // else if(other.gameObject.layer == 0)
            // {

            //  }            
            //AudioSource.PlayClipAtPoint(effect1, new Vector3(0, 0, 0));           
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
