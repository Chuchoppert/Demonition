using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadAnim : MonoBehaviour
{
    bool oncetime = false;
    GameObject GO_PartSys;

    public void DeadAnimSound(AudioClip[] DeadSound, GameObject DeadExplosion, AudioSource SoundSource, int NumberDead)
    {
        if (oncetime == false)
        {
            SoundSource.clip = DeadSound[Random.Range(NumberDead, DeadSound.Length)];
            SoundSource.PlayOneShot(SoundSource.clip);

            //DeadExplosion.transform.position = this.transform.position;
            GO_PartSys = Instantiate<GameObject>(DeadExplosion);
            GO_PartSys.transform.position = this.transform.position;
            Destroy(GO_PartSys, 5f);

            oncetime = true;
        }
    }
}
