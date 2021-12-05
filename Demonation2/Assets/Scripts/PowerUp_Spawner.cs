using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_Spawner : MonoBehaviour
{

    public GameObject[] PowerUps;
    public Transform[] SpawnLocations;
    public float SpawnTimer = 30f;
    private float counter;
    private GameObject PowerUp;

    // Start is called before the first frame update
    void Start()
    {
        counter = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if(counter >= SpawnTimer)
        {
            PowerUp = Instantiate(PowerUps[Random.Range(0, PowerUps.Length)], SpawnLocations[Random.Range(0, SpawnLocations.Length)]);
            counter = 0f;
        }
        Destroy(PowerUp, 10f);
    }
}
