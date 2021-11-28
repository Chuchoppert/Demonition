using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [Header("Sets for Score")]
    public TextMeshProUGUI Text_ScoreIngame;
    public TextMeshProUGUI Text_ScoreInGameover;
    public float ScoreInGame;
    public float MaxScore;

    [Header("Sets for HighScore")]
    public TextMeshProUGUI Text_HScoreInMenu;
    public TextMeshProUGUI Text_HTimeInMenu;
    public bool isHighCore = false;

    [Header("Sets for time")]
    public TextMeshProUGUI Text_TimeIngame;
    public TextMeshProUGUI Text_TimeInGameover;

    private float TimeIngame;

    [Header("Sets for stuffs")]
    public GameObject Player;
    public GameObject GameoverMenu;
    public float TimeToAppearGameoverMenu = 1.5f;
    public bool isActivateHM;
    public TextMeshProUGUI TextMoney;
    private float money;
    public float earnMoneyperScore;

    private bool OncetimeMoney;

    // Start is called before the first frame update
    void Start()
    {
        Text_HScoreInMenu.text = PlayerPrefs.GetFloat("HighScore").ToString("F0");
        Text_HTimeInMenu.text = PlayerPrefs.GetFloat("TimeHighScore").ToString("F2");

        money = PlayerPrefs.GetFloat("Money");
    }

    // Update is called once per frame
    void Update()
    {
        TextTime();
        Scores();
        HighScores();

        isActivateHM = HardcoreMODE.isHardcore;

        if (isHighCore == true)
        {
            MaxScore = ScoreInGame;
            PlayerPrefs.SetFloat("HighScore", MaxScore);


            PlayerPrefs.SetFloat("TimeHighScore", TimeIngame);
        }

        if (Player.gameObject.activeSelf == false)
        {
            if(OncetimeMoney == false)
            {
                float plusmoney = ScoreInGame * earnMoneyperScore;
                money += plusmoney;
                TextMoney.text = money.ToString();
                PlayerPrefs.SetFloat("Money", money);

                OncetimeMoney = true;
            }           
            Invoke("ActivateGameoverMenu", TimeToAppearGameoverMenu);
        }
    }

    void TextTime()
    {
        if (Player != null && Player.activeSelf == true && Text_TimeIngame != null) //si esta activo el Player, correr tiempo
        {
            TimeIngame += Time.deltaTime;
            Text_TimeIngame.text = "Time: " + TimeIngame.ToString("F2");
        }
        else if (Player != null && Player.activeSelf == false && Text_TimeInGameover != null) //Si hay Gameover, parar tiempo y hacer Check de HighScore de tiempo
        {
            Text_TimeInGameover.text = TimeIngame.ToString("F2");
        }
    }

    void Scores()
    {
        if (Text_ScoreIngame != null)
        {
            Text_ScoreIngame.text = "Score: " + ScoreInGame.ToString("F0");
        }
        if (Text_ScoreInGameover != null)
        {
            Text_ScoreInGameover.text = ScoreInGame.ToString("F0");
        }

        if (Player != null && Player.activeSelf == false) //Si hay Gameover, hacer check de HighScore
        {
            if (MaxScore < ScoreInGame)
            {
                isHighCore = true;
            }
        }
    }

    void HighScores()
    {
        if (GameoverMenu.gameObject.activeSelf == true)
        {
            Text_HScoreInMenu.text = PlayerPrefs.GetFloat("HighScore").ToString("F0");
            Text_HTimeInMenu.text = PlayerPrefs.GetFloat("TimeHighScore").ToString("F2");
        }
    }

    void ActivateGameoverMenu()
    {
        GameoverMenu.gameObject.SetActive(true);
    }
}
