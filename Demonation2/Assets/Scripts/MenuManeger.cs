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
        if(ActivateReset == true)
        {
            PlayerPrefs.DeleteAll();
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
   /*public void ScoresGH()
    {
        Hcore = PlayerPrefs.GetFloat("HighScore");

        if (Text_Score != null) //Game
        {
            Text_Score.text = "Score: " + score.ToString("F0");
        }

        if (Text_SC != null) //Game over
        {
            Text_SC.text = score.ToString("F0");
        }

        if (Text_TSC != null)
        {
            Text_TSC.text = ExacTime.ToString("F2");
        }                     
    }*/

    public void ResetScore()
    {
        ActivateReset = true;
    }
}
