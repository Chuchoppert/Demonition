using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public GameObject[] EnemyPrefabs;
	public float[] EnemiesForSeconds = { 2f, 4f };

	private GameObject prefabInstance;
	public int AmountEnemies = 7;
	public int Cuantostenemos;


	void Start()
	{
			Invoke("PreSpawn", Random.Range(EnemiesForSeconds[0], EnemiesForSeconds[1]));
	}

	// Update is called once per frame
	void Update()
	{
		Cuantostenemos = transform.childCount;
	}
	void PreSpawn()
    {
		if(transform.childCount < AmountEnemies)
        {
			Invoke("SpawnEnemies", Random.Range(EnemiesForSeconds[0], EnemiesForSeconds[1])); //ARRGLAR: EL QUE NO PUEDAN GENERARSE MAS TRAS MORIR Y EL QUE SE GENEREN 7 AL MISMO TIEMPO
		}	
	}
	void SpawnEnemies()
	{
		for (int i = 0; i < AmountEnemies; i++)
		{
			// instantiate a random enemy past the right egde of the screen, facing left
			prefabInstance = Instantiate(EnemyPrefabs[Random.Range(0, EnemyPrefabs.Length)], new Vector3(57.7f, Random.Range(-12f, 14.5f), 27.7f), Quaternion.Euler(-90f, 0f, 0f));
			prefabInstance.transform.SetParent(transform);		
		}
		Invoke("PreSpawn", Random.Range(EnemiesForSeconds[0], EnemiesForSeconds[1]));
	}

}
