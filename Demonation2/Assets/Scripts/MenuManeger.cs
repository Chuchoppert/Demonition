using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuManeger : MonoBehaviour
{
    public float score;
     float Hcore;
    public TextMeshProUGUI Text_Score; //pantallaGame
    public TextMeshProUGUI Text_SC; //PantallaGO
    public TextMeshProUGUI Text_HC; //PantallaGO

     float TimerSC;
     float TimerHC;
    public TextMeshProUGUI Text_TSC;
    public TextMeshProUGUI Text_THC;

    public GameObject Demon;
    public GameObject GameOverMenu;

    public float timer = 1.6f;

    public bool isActivateHM;
    void Start()
    {
        Text_HC.text = PlayerPrefs.GetFloat("HighScore").ToString("F0");
        Text_THC.text = PlayerPrefs.GetFloat("HighScoreTime").ToString("F2");
    }
    void Update()
    {
        ScoresGH();
        TimeScoreHC();
        if (Demon != null && Demon.activeSelf == true)
        {
            GameOverMenu.SetActive(false);
        }
        else if (Demon != null && Demon.activeSelf == false)
        {
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
       if(Text_Score != null)
        {
            Text_Score.text = "Ships: " + score.ToString("F0");
        }

        if (Text_SC != null)
        {
            Text_SC.text = score.ToString("F0");
        }
        
        if (Hcore < score)
        {
            Hcore = score;
            PlayerPrefs.SetFloat("HighScore", Hcore);
            Text_HC.text = Hcore.ToString("F0");
        }              
    }
    public void TimeScoreHC()
    {      
        
        if (Text_TSC != null)
        {
            Mathf.Round(TimerSC += Time.deltaTime);
            Text_TSC.text = TimerSC.ToString("F2");
        }
            

        if (TimerHC < TimerSC)
        {
            TimerHC = TimerSC;
            PlayerPrefs.SetFloat("HighScoreTime", TimerHC);
            Text_THC.text = TimerHC.ToString("F2");
        }
    }
}
