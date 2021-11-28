using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skins_Player : MonoBehaviour
{
    public float PlayerSkin;

    [Header("Set GO Skins for player")]
    public GameObject prop1;
    public GameObject prop2;
    public GameObject prop3;
    public GameObject prop4;

    private void Start()
    {
        PlayerSkin = PlayerPrefs.GetFloat("SkinSelected");
        if (PlayerSkin == 1)
        {
            prop1.SetActive(true);
        }
        if (PlayerSkin == 2)
        {
            prop2.SetActive(true);
        }
        if (PlayerSkin == 3)
        {
            prop3.SetActive(true);
        }
        if (PlayerSkin == 4)
        {
            prop4.SetActive(true);
        }
    }

}
