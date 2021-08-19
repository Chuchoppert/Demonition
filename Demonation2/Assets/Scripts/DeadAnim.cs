using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadAnim : MonoBehaviour
{
    public ParticleSystem explosion;
    public AudioSource SoundSource;
    public AudioClip[] Sounds;

    public bool isDead;

    // Start is called before the first frame update
    void Start()
    {       

    }

    // Update is called once per frame
    void Update()
    {
        if (isDead == true) //SI SE ACTIVA EL TRUE MANUALMENTE FUNCIONA, PERO NO CUANDO LO HACE LA MUERTE
        {
            Invoke("DeadAnimSound", 0f);
        }
    }
     void DeadAnimSound()
    {        
            SoundSource.clip = Sounds[Random.Range(0, Sounds.Length)];
            SoundSource.PlayOneShot(SoundSource.clip);

            explosion.transform.position = transform.position;
            Instantiate<ParticleSystem>(explosion);                  
    }
}
