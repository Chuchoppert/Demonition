using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuManeger : MonoBehaviour
{
    public float score;
    public float Hcore;
    public TextMeshProUGUI Text_Score;
    public TextMeshProUGUI Text_SC;
    public TextMeshProUGUI Text_HC;

    public GameObject Demon;
    public GameObject GameOverMenu;

    public float timer = 1.6f;


    void Start()
    {
        Text_HC.text = PlayerPrefs.GetFloat("HighScore").ToString("F2");
    }
    void Update()
    {
        //Reemplazar el Delta.Time por el puntaje real

        ScoresGH();

        if (Demon.activeSelf == true)
        {
            GameOverMenu.SetActive(false);
        }
        else
        {
            timer -= Time.deltaTime;
            if ( timer <= 0)
            {
                GameOverMenu.SetActive(true);
            }
            
        }
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
        //score += Time.deltaTime;        
        //Mathf.Round(score += Time.deltaTime); LUEGO SE AGREGA LO DEL TIEMPO EN PANTALLA GAMEOVER

        Text_Score.text = "Ships: " + score.ToString("F2");
        Text_SC.text = score.ToString("F2");
        
        if (Hcore < score)
        {
            Hcore = score;
            PlayerPrefs.SetFloat("HighScore", Hcore);
            Text_HC.text = Hcore.ToString("F2");
        }
       
        
    }

}
