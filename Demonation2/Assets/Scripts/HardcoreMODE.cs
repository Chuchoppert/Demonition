using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HardcoreMODE : MonoBehaviour
{
    public static bool isHardcore;
    bool isHardcoreActivate;

    

    private void Start()
    {
        
    }

    public void ActivateHM(bool isActivate)
    {
        isHardcore = isActivate;
    }
}
