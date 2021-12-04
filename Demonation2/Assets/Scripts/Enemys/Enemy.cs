using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Actions Enemys")]
    public float[] EnemyLimitsBorder = { 8, 28 };   
    public float DistanceToShoot = 80f;
    public float ShootInterval = 1.5f;
    public float MovingSpeedEnemy = 2f;


    [Header("Sets for Dead and Shoot")]
    public GameObject bullet_Prefab;
    public GameObject Chunk_Prefab;
    public AudioClip[] SoundsEnemy; //bullet   |  dead
    public int NumberDeadSound;
    public AudioSource SoundFromEnemy;
    public GameObject Explosion;

    private GameObject Player;
    private float DistanceToStop;
    private float distanceFromTarget;
    private bool onetime = false;
    private bool onetimeDeadAnim = false;
    private bool onetimeChunkDestroy = false;


    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        this.DistanceToStop = Random.Range(EnemyLimitsBorder[0], EnemyLimitsBorder[1]);

        SoundFromEnemy = GameObject.FindGameObjectWithTag("SdEnemies").GetComponent<AudioSource>();      
    }

    void Update()
    {
        LookPlayer(); 
        if(distanceFromTarget < DistanceToShoot && Player != null && onetime == false)
        {
            Invoke("ShootPlayer", ShootInterval);
            onetime = true;
        }

        if (gameObject.transform.position.x > this.DistanceToStop) 
        {
            this.gameObject.transform.Translate(-MovingSpeedEnemy * 2 * Time.deltaTime, 0, 0, Space.World);         
        }
        else if(gameObject.transform.position.x < this.DistanceToStop)
        {
            this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        }
    }

    void LookPlayer()
    {
        Debug.DrawRay(gameObject.transform.position, gameObject.transform.forward * 100, Color.red);
        if (Player != null)
        {
            distanceFromTarget = Vector3.Distance(this.transform.position, Player.transform.position);

            transform.LookAt(Player.transform);
        }
    }

    void ShootPlayer()
    {
        if(distanceFromTarget < DistanceToShoot && Player != null)
        {
            GameObject bullet = Instantiate<GameObject>(bullet_Prefab);
            bullet.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z); //NOTA: modificar origin de los enemies
            bullet.transform.LookAt(Player.transform.position);

            SoundFromEnemy.clip = SoundsEnemy[0];
            SoundFromEnemy.PlayOneShot(SoundFromEnemy.clip);

            Invoke("ShootPlayer", ShootInterval);
        }
    }
    void InstantiateOneTime()
    {      
        if (this.onetimeChunkDestroy == false)
        {
            this.onetimeChunkDestroy = true;
            GameObject Chunk = Instantiate<GameObject>(Chunk_Prefab);
            Chunk.transform.position = transform.position;     
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 12 || collision.gameObject.layer == 9) //si toca asteroide y chunk produce animacion de muerte
        {
            if (collision.gameObject.layer == 12 && collision.gameObject.GetComponent<ChunksController>().ThisWasPicked == true) //Si es el chunk, lanzalo
            {
                InstantiateOneTime();
            }

            if (collision.gameObject.layer == 9 && collision.gameObject.GetComponent<Asteroids>().ThisWasPicked == true)  //Si es el asteroide, lanzalo
            {
                InstantiateOneTime();
            }

            if(onetimeDeadAnim == false)
            {
                gameObject.GetComponent<DeadAnim>().DeadAnimSound(SoundsEnemy, Explosion, SoundFromEnemy, NumberDeadSound);
                onetimeDeadAnim = true;
            }
        }
    }
}
