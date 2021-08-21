using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab_Throw : MonoBehaviour
{
    private GameObject Player; 

    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Player.transform.position.x + 5, Player.transform.position.y, Player.transform.position.z);
    }
}
