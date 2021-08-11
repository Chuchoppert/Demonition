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

    void Start()
    {
        Text_HC.text = PlayerPrefs.GetFloat("HighScore").ToString("1");
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
            GameOverMenu.SetActive(true);
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
        Mathf.Round(score += Time.deltaTime);

        Text_Score.text = "Ships: " + score.ToString("1");
        Text_SC.text = score.ToString("1");
        
        if (Hcore < score)
        {
            Hcore = score;
            PlayerPrefs.SetFloat("HighScore", Hcore);
            Text_HC.text = Hcore.ToString("1");
        }
       
        
    }

}
