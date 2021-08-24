using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HardcoreMODE : MonoBehaviour
{
    public static bool isHardcore;

    public void ActivateHM(bool isActivate)
    {
        isHardcore = isActivate;
    }
}
