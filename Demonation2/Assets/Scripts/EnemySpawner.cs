using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public GameObject[] EnemyPrefabs;
	public GameObject[] Enemies;
	public float[] EnemiesForSeconds = { 2f, 4f };

	private GameObject prefabInstance;

	//public static int AmountEnemies = 7;
	public int AmountEnemies = 7;
	public int Cuantostenemos;
	GameObject[] EachEnemy;
	bool IsFull;

	private void Awake()
    {
		Invoke("PreSpawn", Random.Range(EnemiesForSeconds[0], EnemiesForSeconds[1]));
	}
    void Start()
	{
	
	}

	// Update is called once per frame
	void Update()
	{
		EachEnemy = GameObject.FindGameObjectsWithTag("Enemy");
		Cuantostenemos = EachEnemy.Length;
		Enemies = EnemyPrefabs;

		if (Cuantostenemos < AmountEnemies)
		{
			IsFull = false;			
		}
		else if (Cuantostenemos >= AmountEnemies)
		{
			IsFull = true;
		}
	}
	void PreSpawn()
    {
		Invoke("SpawnEnemies", Random.Range(EnemiesForSeconds[0], EnemiesForSeconds[1])); 

	}
	void SpawnEnemies()
	{
		// instantiate a random enemy past the right egde of the screen, facing left
		if(IsFull == false)
		{ 
			prefabInstance = Instantiate(Enemies[Random.Range(0, Enemies.Length)], new Vector3(57.7f, Random.Range(-12f, 14.5f), 27.7f), Quaternion.Euler(-90f, 0f, 0f));
		}
		else if (true)
        {
			Debug.Log("Are you OK?");
        }
		Invoke("PreSpawn", Random.Range(EnemiesForSeconds[0], EnemiesForSeconds[1]));
	}

}
