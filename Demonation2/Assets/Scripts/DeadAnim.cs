using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadAnim : MonoBehaviour
{
    public ParticleSystem explosion;
    public AudioSource[] explosionSound;

    public bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        explosionSound.Play();
        explosion.transform.position = transform.position;
        Instantiate<ParticleSystem>(explosion);
    }
}
