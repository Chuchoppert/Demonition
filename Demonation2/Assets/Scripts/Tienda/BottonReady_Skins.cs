using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottonReady_Skins : MonoBehaviour
{
    public GameObject TextoReady;
    public float NumberProp;

    public GameObject[] otherText;

    private float WasBought;
    public void ChangeReady()
    {
        this.WasBought = PlayerPrefs.GetFloat("Prop" + NumberProp); // 0 = false , 1 = true

        if (TextoReady != null && this.WasBought == 1)
        {
            if (TextoReady.activeSelf == true)
            {
                TextoReady.SetActive(false);
            }
            else
            {
                TextoReady.SetActive(true);

                foreach (GameObject GO_otherText in otherText)
                {
                    GO_otherText.SetActive(false);
                }
                PlayerPrefs.SetFloat("SkinSelected", NumberProp);
            }
        }
    }
}
