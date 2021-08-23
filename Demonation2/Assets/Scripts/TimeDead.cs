using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDead : MonoBehaviour
{
    float Timer;
    public float TimerDead;

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer > TimerDead)
        {
            Destroy(gameObject);
        }
    }
}
