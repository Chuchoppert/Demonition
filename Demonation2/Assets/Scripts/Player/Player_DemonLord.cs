using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DemonLord : MonoBehaviour
{
    [Header("Set Basics")]
    public Vector3 OffsetChunkPickedToPlayer;

    [Header("Set for Movements")]
    public float SpeedMovements;
    public float[] LimitsX = new float[] {-32, 32};
    public float[] LimitsY = new float[] {-17, 17};

    [Header("Variable for DemonLords")]
    public float Health_DL;
    public GameObject DemonLord;

    [Header("Sets for Dead, Sounds and Animations")]
    public AudioClip[] SoundsDemonLords; //Throw | Grab | Dead
    public int NumberDeadSound = 2;
    public AudioSource SoundFromDemonLords;
    public ParticleSystem Explosion;
    public Animator DemonLord_Anim;
    //GrabChunk | isReady | Launched

    [Header("Look Actions")]
    public bool isPickedUp = false;
    public bool isLaunched = false;

    private Rigidbody rb;
    private float InputX;  //Horizontal
    private float InputY;  //Vertical
    private GameObject AmmoPickedUp;
    //private MonoBehaviour HealtDemonLord_Script;
    //private GameObject Player

    //Para PowerUps
    private float SpeedPowerUp = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        AmmoPickedUp = null;
        //Player = GameObject.FindGameObjectWithTag("Player");
        //HealtDemonLord_Script = Player.GetComponent<MonoBehaviour>();

        SoundFromDemonLords = GameObject.FindGameObjectWithTag("SoundDemonLords").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {     
        //Limit Movements
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, LimitsX[0], LimitsX[1]),
            Mathf.Clamp(transform.position.y, LimitsY[0], LimitsY[1]), transform.position.z);

        GrabChunk();
        ThrowChunk();
        CheckHealt();       
    }
    private void FixedUpdate()
    {
        //Sets Movements for X and Y
        if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            InputY = Input.GetAxis("Vertical");
            InputX = Input.GetAxis("Horizontal");

            rb.velocity = new Vector3(InputX, InputY, 0) * SpeedMovements * SpeedPowerUp;
        }
    }

    void GrabChunk()
    {
        if(AmmoPickedUp != null) //Setea la pos del objeto agarrado con la pos del Player
        {
            //Animacion de agarrar chunk y mantener una vez
            if(isPickedUp == true)
            {
                AnimationDemonLord(0);
                isPickedUp = false;
            }

            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            if (AmmoPickedUp.gameObject.layer == 12) 
            {
                AmmoPickedUp.GetComponent<ChunksController>().isReadyToLaunch();
            }

            if (AmmoPickedUp.gameObject.layer == 9) 
            {
                AmmoPickedUp.GetComponent<Asteroids>().isReadyToLaunch();
            }

            AmmoPickedUp.gameObject.transform.position = transform.position + OffsetChunkPickedToPlayer;       

        }
        //SOLUCIONAR: SI COHCAMOS EL CHUNK EN MANOS CON UN ENEMIGO, ESTE SE BORRA PERO NO SE REINICIA PARA EL SIGUIENTE CHUNK
    }

    void ThrowChunk()
    {
        if (AmmoPickedUp != null && Input.GetKeyDown(KeyCode.Space) && isLaunched == false)
        {
            //Animacion de lanzar
            AnimationDemonLord(1);

            if (AmmoPickedUp.gameObject.layer == 12) //Si es el chunk, lanzalo
            {
                AmmoPickedUp.GetComponent<ChunksController>().isLaunched();
            }

            if (AmmoPickedUp.gameObject.layer == 9) //Si es el asteroide, lanzalo
            {
                AmmoPickedUp.GetComponent<Asteroids>().isLaunched();
            }

            AmmoPickedUp = null;   
            isLaunched = true;

            Invoke("ResetBools", 0.2f); //Preparar todo para el nuevo intento
        }
    }

    void ResetBools()
    {
        gameObject.GetComponent<BoxCollider>().isTrigger = false;
        isPickedUp = false;
        isLaunched = false;

        DemonLord_Anim.SetBool("Launched", false);
    }

    void CheckHealt()
    {
        Health_DL = DemonLord.GetComponent<HealthDemonLords>().Healt;
        if (Health_DL <= 0)
        {
            gameObject.GetComponent<DeadAnim>().DeadAnimSound(SoundsDemonLords, Explosion, SoundFromDemonLords, NumberDeadSound);
            gameObject.SetActive(false);
        }
    }
    
    void AnimationDemonLord(float WhatHappen)
    {
        if(WhatHappen == 0) //Agarra Chunk
        {
            SoundFromDemonLords.clip = SoundsDemonLords[1];
            SoundFromDemonLords.PlayOneShot(SoundFromDemonLords.clip);

            DemonLord_Anim.SetBool("GrabChunk", true);
        }
        else if(WhatHappen == 1) //Lanza chunk
        {
            SoundFromDemonLords.clip = SoundsDemonLords[0];
            SoundFromDemonLords.PlayOneShot(SoundFromDemonLords.clip);

            DemonLord_Anim.SetBool("GrabChunk", false);
            DemonLord_Anim.SetBool("Launched", true);
        }

    }

    private void OnCollisionStay(Collision collision)
    {      
        if(collision.gameObject.layer == 12 || collision.gameObject.layer == 9) //Chunks o Asteroids
        {
            //Agrega el chunk o asteroide a un gameobject especifico
            if (Input.GetKeyUp(KeyCode.Space) && AmmoPickedUp == null)
            {
                isPickedUp = true;
                AmmoPickedUp = collision.gameObject;               
            }
        }
    }
}
