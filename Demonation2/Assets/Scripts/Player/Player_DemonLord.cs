using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DemonLord : MonoBehaviour
{
    //NOTA: POR ALGUNA RAZON si agarro el power up de Nuke y un chunk al mismo tiempo, COSAS pasan.
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
    public AudioClip[] SoundsDemonLords; //Throw | Grab | Dash | PickUpPowerUp | Dead 
    public int NumberDeadSound = 2;
    public AudioSource SoundFromDemonLords;
    public GameObject Explosion;
    public Animator DemonLord_Anim;
    //GrabChunk | isReady | Launched

    [Header("Set for Dash")]
    public float DashSpeed;
    public float DashTime;
    public float DashCooldown = 3f;
    public GameObject DashEffect;

    [Header("Look Actions")]
    public bool isPickedUp = false;
    public bool isLaunched = false;

    [Header("Set for PowerUp")]
    public float SpeedPowerUp = 2;
    public float Time_SpeedPW = 5;

    private Rigidbody rb;
    private float InputX;  //Horizontal
    private float InputY;  //Vertical

    private GameObject AmmoPickedUp = null;

    private MeshCollider MeshColl_DL;
    private bool DashActivate = false;
    private GameObject GO_DashEffect;

    private float RealSpeedPowerUp = 1;
    private float WhatPowerReset = 0;

    public static float SpeedSlowMotion; //UTILIZA ESTA LINEA PARA MODIFICAR A TODOS LOS SCRIPT (SOLO BULLET, SWARM Y ASTEROID) REVISA LOS COMENTARIOS Y TE DEJE ABAJO LA BASE (LINEA 244 y 261)

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();

        SoundFromDemonLords = GameObject.FindGameObjectWithTag("SoundDemonLords").GetComponent<AudioSource>();
        MeshColl_DL = DemonLord.GetComponent<MeshCollider>();
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
        Dash();
        DashCooldown -= Time.deltaTime;
    }
    private void FixedUpdate()
    {
        //Sets Movements for X and Y
        InputY = Input.GetAxis("Vertical");
        InputX = Input.GetAxis("Horizontal");
        
        if ( (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0) && DashActivate == false)
        {         
            rb.velocity = new Vector3(InputX, InputY, 0) * (SpeedMovements * RealSpeedPowerUp);
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
        else
        {
            AnimationDemonLord(2);
            Invoke("ResetBools", 0.2f);
            AmmoPickedUp = null;
        }
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
        else if(WhatHappen == 2) //Resetear animacion si chocas con el chunk agarrado
        {
            DemonLord_Anim.SetBool("GrabChunk", false);
            DemonLord_Anim.SetBool("Launched", false);
        }

    }

    private void Dash()
    {
        if (DashCooldown <= 0)
        {

            if (Input.GetKey(KeyCode.LeftShift))
            {

                SoundFromDemonLords.clip = SoundsDemonLords[2];
                SoundFromDemonLords.PlayOneShot(SoundFromDemonLords.clip);

                GO_DashEffect = Instantiate<GameObject>(DashEffect);
                GO_DashEffect.transform.position = this.transform.position;
                Destroy(GO_DashEffect, 2f);

                rb.AddForce(new Vector3(InputX, InputY, 0) * DashSpeed, ForceMode.Impulse);
                MeshColl_DL.enabled = false;
                DashCooldown = 5f;
            }
        }
        if (DashCooldown < 4.5f)
        {
            MeshColl_DL.enabled = true;
        }
    }
    void Nuke()
    {
        GameObject.FindGameObjectWithTag("SpawnerEnemy").GetComponent<EnemySpawner>().EliminateAllEnemy();
    }
    void GetExtraLife()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<HealthDemonLords>().Healt += 1;
    }
    void SlowMotionPW()
    {
        //Haz cambios de la static aqui

        WhatPowerReset = 2;
        Invoke("ResetPowerUp", 5);
    }
    void Speed_PowerUp()
    {
        RealSpeedPowerUp = SpeedPowerUp;

        WhatPowerReset = 1;
        Invoke("ResetPowerUp", Time_SpeedPW);
    }

    void ResetPowerUp()
    { 
        if(WhatPowerReset == 1)
        {
            RealSpeedPowerUp = 1;
            WhatPowerReset = 0;
        }
        if(WhatPowerReset == 2)
        {
            //Variable static aqui reiniciada
            WhatPowerReset = 0;
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
        if (collision.gameObject.tag == "Nuke_PW")
        {            
            Destroy(collision.gameObject);
            Nuke();
        }
        if (collision.gameObject.tag == "Vida_PW")
        {
            Destroy(collision.gameObject);
            GetExtraLife();
        }
        if (collision.gameObject.tag == "Speed_PW")
        {
            Destroy(collision.gameObject);
            Speed_PowerUp();
        }
        if (collision.gameObject.tag == "SlowMotion_PW")
        {
            Destroy(collision.gameObject);
            SlowMotionPW();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Nuke_PW")
        {
            Destroy(other.gameObject);
            Nuke();
        }
        if (other.gameObject.tag == "Vida_PW")
        {
            Destroy(other.gameObject);
            GetExtraLife();
        }
        if (other.gameObject.tag == "Speed_PW")
        {
            Destroy(other.gameObject);
            Speed_PowerUp();
        }
        if (other.gameObject.tag == "SlowMotion_PW")
        {
            Destroy(other.gameObject);
            SlowMotionPW();
        }
    }
}
