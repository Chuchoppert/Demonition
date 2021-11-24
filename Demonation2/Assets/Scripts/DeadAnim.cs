using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadAnim : MonoBehaviour
{
    bool oncetime = false;

    public void DeadAnimSound(AudioClip[] DeadSound, ParticleSystem DeadExplosion, AudioSource SoundSource, int NumberDead)
    {
        if (oncetime == false)
        {
            SoundSource.clip = DeadSound[Random.Range(NumberDead, DeadSound.Length)];
            SoundSource.PlayOneShot(SoundSource.clip);

            DeadExplosion.transform.position = this.transform.position;
            Instantiate<ParticleSystem>(DeadExplosion);

            oncetime = true;
        }
    }
}
