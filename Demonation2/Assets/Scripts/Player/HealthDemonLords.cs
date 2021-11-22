using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDemonLords : MonoBehaviour
{
    [Header("Set life and Damage")]
    public float Healt = 2;
    public float DamageForBullets = 1;
    public float DamageForSmallEnemy = 0.5f;
    public float DamageForBigEnemy = 1f;
    public float DamageForNave = 1f;


    private void Update()
    {
        DemonLordWasHurt();
    }
    void DemonLordWasHurt() //Cambia "material" al demonio y si llega a 0, se desactiva  (PASAR A SCRIPT DE DEMONLORD)
    {
        if (Healt >= 1)
        {
            gameObject.layer = 8;
        }
        else if (Healt == 1)
        {
            gameObject.layer = 3;
        }
        else if (Healt <= 0)
        {
            gameObject.layer = 8;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 14)//Da�o por bala
        {
            Healt -= DamageForBullets;
        }
        else if (other.gameObject.layer == 10) //Da�o por tocar enemigo peque�o
        {
            Healt -= DamageForSmallEnemy;

        }
        else if (other.gameObject.layer == 11) //Da�o por tocar enemigo grande
        {
            Healt -= DamageForBigEnemy;
        }
        else if (other.gameObject.layer == 15) //Da�o por tocar nave
        {
            Healt -= DamageForBigEnemy;
        }
    }
}
