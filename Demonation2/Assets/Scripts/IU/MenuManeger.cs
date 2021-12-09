using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using TMPro;

public class MenuManeger : MonoBehaviour
{
    [Header("Look")]
    public bool ActivateReset = false;
    public TextMeshProUGUI Text_HScoreInMenu;
    public TextMeshProUGUI Text_HTimeInMenu;

    public TextMeshProUGUI TextMoney;

    void Start()
    {
        if(Text_HScoreInMenu != null && Text_HTimeInMenu != null)
        {
            Text_HScoreInMenu.text = PlayerPrefs.GetFloat("HighScore").ToString("F0");
            Text_HTimeInMenu.text = PlayerPrefs.GetFloat("TimeHighScore").ToString("F2");
        } 
    }

    void Update()
    {
        if(TextMoney != null)
        {
            TextMoney.text = "Souls: " + PlayerPrefs.GetFloat("Money").ToString("F2");
        }

        if (ActivateReset == true)
        {
            PlayerPrefs.SetFloat("HighScore", 0);
            PlayerPrefs.SetFloat("TimeHighScore", 0);

            SceneManager.LoadScene("_MenuPrincipal");
            ActivateReset = false;
        }
    }
    public void BotonQuit()
    {
        Debug.Log(0);
        Application.Quit();
    }
    public void SceneAdministrator(string escena)
    {
        SceneManager.LoadScene(escena);   
    }
    public void ResetScore()
    {
        ActivateReset = true;
    }
}
