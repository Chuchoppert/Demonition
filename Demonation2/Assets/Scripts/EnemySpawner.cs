using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public GameObject[] prefabs;
	public float[] EnemiesForSeconds = { 2f, 4f };

	// Use this for initialization
	void Start()
	{
		Invoke("SpawnEnemies", Random.Range(EnemiesForSeconds[0], EnemiesForSeconds[1]));
	}

	// Update is called once per frame
	void Update()
	{
		//solo para hardcore mode
		//Invoke("SpawnEnemies", Random.Range(2f, 4f));
	}

	void SpawnEnemies()
	{
		// instantiate a random enemy past the right egde of the screen, facing left
		Instantiate(prefabs[Random.Range(0, prefabs.Length)], new Vector3(57.7f, Random.Range(-12f, 14.5f), 27.7f), Quaternion.Euler(-90f, -90f, 0f));

		Invoke("SpawnEnemies", Random.Range(EnemiesForSeconds[0], EnemiesForSeconds[1]));
	}
}
