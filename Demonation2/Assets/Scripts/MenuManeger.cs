using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class MenuManeger : MonoBehaviour
{
    public float score;
    float Hcore = 0;
    public TextMeshProUGUI Text_Score; //pantallaGame
    public TextMeshProUGUI Text_SC; //PantallaGO
    public TextMeshProUGUI Text_HC; //PantallaGO

    float TimerSC;
    float TimerHC;
    public float ExacTime;
    public TextMeshProUGUI Text_TSC;
    public TextMeshProUGUI Text_THC;

    public GameObject Demon;
    public GameObject GameOverMenu;

    public float timer = 1.6f;

    public bool isActivateHM;
    void Start()
    {
       // if (Application.isEditor == false)
       //{
            Text_HC.text = PlayerPrefs.GetFloat("HighScore").ToString("F0");
            Text_THC.text = PlayerPrefs.GetFloat("HighScoreTime").ToString("F2");
       // }
       
    }
    void Update()
    {
        
        ScoresGH();
        if (Demon != null && Demon.activeSelf == true)
        {
            TimerSC += Time.deltaTime;
            GameOverMenu.SetActive(false);           
        }
        else if (Demon != null && Demon.activeSelf == false)
        {
            ExacTime = TimerSC;
            if (Hcore < score)
            {            
                Text_THC.text = ExacTime.ToString("F2");
                //Text_THC.text = Text_TSC.text;

                PlayerPrefs.SetFloat("HighScoreTime", Single.Parse(Text_THC.text));

                Hcore = score;
                PlayerPrefs.SetFloat("HighScore", Hcore);
                Text_HC.text = Hcore.ToString("F0");
            }

            timer -= Time.deltaTime;
            if ( timer <= 0)
            {
                
                GameOverMenu.SetActive(true);
            }          
        }

        isActivateHM = HardcoreMODE.isHardcore;
    }
    public void MN_BotonStart()
    {
        SceneManager.LoadScene(1);
    }
    public void BotonQuit()
    {
        Debug.Log(0);
        Application.Quit();
    }
    public void BotonMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void BotonRestart(string escena)
    {
        SceneManager.LoadScene(escena);   
    }
   public void ScoresGH()
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
    }

    public void ResetScore()
    {
        PlayerPrefs.DeleteAll();
    }
}
