using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDemonLords : MonoBehaviour
{
    [Header("Set life and Damage")]
    public float Healt = 2;
    public float DamageForBullets = 1;
    public float DamageForSmallEnemy = 0.5f;
    public float DamageForBigEnemy = 1f;
    public float DamageForNave = 1f;
    public Slider VidasImages;
    public Image Fill;
    public GameObject HeartBeat;
 
    [Header("Set changing color Lerp")]
    public RawImage RawImage_Camera;
    public Color[] ColorChanging;
    [Range(0f, 1f)]
    public float LerpTime;

    private void Update()
    {
        DemonLordWasHurt();
        CheckNoLives();
    }
    void DemonLordWasHurt()
    {
        if (Healt > 1)
        {
            RawImage_Camera.color = Color.white;
        }
        else if (Healt <= 1)
        {
            RawImage_Camera.color = Color.Lerp(ColorChanging[0], ColorChanging[1], Mathf.PingPong(Time.time * LerpTime, 1));
        }
    }

    void CheckNoLives()
    {
        VidasImages.value = Healt;
        if (Healt >= 3)
        {
            Healt = 3;
        }
        if(VidasImages.value == 0)
        {
            HeartBeat.SetActive(false);
            Fill.color = new Color(0, 0, 0, 1);
        }
        if(VidasImages.value == 1)
        {
            HeartBeat.SetActive(true);
            Fill.color = new Color(0.25f, 0.25f, 0.25f, 1);
        }
        if(VidasImages.value == 2)
        {
            HeartBeat.SetActive(false);
            Fill.color = new Color (0.5f, 0.5f, 0.5f, 1);
        }
        if (VidasImages.value == 3)
        {
            HeartBeat.SetActive(false);
            Fill.color = new Color(1, 1, 1, 1);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Collision with danger objects
        if (other.gameObject.layer == 14)//Daño por bala
        {
            Healt -= DamageForBullets;
        }
        else if (other.gameObject.layer == 10) //Daño por tocar enemigo pequeño
        {
            Healt -= DamageForSmallEnemy;

        }
        else if (other.gameObject.layer == 11) //Daño por tocar enemigo grande
        {
            Healt -= DamageForBigEnemy;
        }
        else if (other.gameObject.layer == 15) //Daño por tocar nave
        {
            Healt -= DamageForBigEnemy;
        }

        //Collision with PowerUps
        if (other.gameObject.tag == "Nuke_PW")
        {           
            Destroy(other.gameObject);
            GameObject.FindGameObjectWithTag("Grab").GetComponent<Player_DemonLord>().Nuke();
        }
        if (other.gameObject.tag == "Vida_PW")
        {
            Destroy(other.gameObject);
            GameObject.FindGameObjectWithTag("Grab").GetComponent<Player_DemonLord>().GetExtraLife();
        }
        if (other.gameObject.tag == "Speed_PW")
        {
            Destroy(other.gameObject);
            GameObject.FindGameObjectWithTag("Grab").GetComponent<Player_DemonLord>().Speed_PowerUp();
        }
        if (other.gameObject.tag == "SlowMotion_PW")
        {
            Destroy(other.gameObject);
            GameObject.FindGameObjectWithTag("Grab").GetComponent<Player_DemonLord>().SlowMotionPW();
        }
    }
}
