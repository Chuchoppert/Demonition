using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PropsComprados : MonoBehaviour
{
    public GameObject TextCost;
    public float CostSkin;
    public float WasBought;
    public string WhatProp;

    private float MoneyPlayer;

    private void Start()
    {
       this.WasBought = PlayerPrefs.GetFloat("Prop" + WhatProp); // 0 = false , 1 = true
       // PlayerPrefs.SetFloat("Money", 5452154);
    }

    // Update is called once per frame
    void Update()
    {      
        if (this.WasBought == 0)
        {
            TextCost.SetActive(true);
            TextCost.GetComponent<TextMeshProUGUI>().text = "Price: " + CostSkin.ToString();
        }
        else if (this.WasBought == 1)
        {
            TextCost.SetActive(false);
        }


        if (Input.GetKey(KeyCode.X) && Input.GetKey(KeyCode.LeftShift))
        {
            MoneyPlayer = 100000f;
            PlayerPrefs.SetFloat("Money", MoneyPlayer);
        }
    }

    public void CheckBought()
    {
        MoneyPlayer = PlayerPrefs.GetFloat("Money");
        if (MoneyPlayer > CostSkin)
        {
            if (this.WasBought == 0)
            {
                MoneyPlayer -= CostSkin;
                TextCost.SetActive(false);
                PlayerPrefs.SetFloat("Money", MoneyPlayer);

                this.WasBought = 1;
                PlayerPrefs.SetFloat("Prop" + WhatProp, WasBought);
            }
        }        
    }
}
