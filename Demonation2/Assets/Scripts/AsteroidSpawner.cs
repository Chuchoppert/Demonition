using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
	public GameObject[] prefabs;
	public float[] EnemiesForSeconds = { 2f, 4f };
	public float[] EnemiesForSecondsHM = { 5f, 8f };

	void Start()
	{
		if (HardcoreMODE.isHardcore == true)
		{
			Invoke("SpawnHM", Random.Range(EnemiesForSecondsHM[0], EnemiesForSecondsHM[1]));
		}
		else if (HardcoreMODE.isHardcore == false)
		{
			Invoke("Spawn", Random.Range(EnemiesForSeconds[0], EnemiesForSeconds[1]));
		}
		
	}
	void Spawn()
	{
		// instantiate a random enemy past the right egde of the screen, facing left
		Instantiate(prefabs[Random.Range(0, prefabs.Length)], new Vector3(15f, Random.Range(24, 30f), 27.7f), Quaternion.Euler(0f, 0f, 0f));

		Invoke("Spawn", Random.Range(EnemiesForSeconds[0], EnemiesForSeconds[1]));
	}

	void SpawnHM()
	{
		// instantiate a random enemy past the right egde of the screen, facing left
		Instantiate(prefabs[Random.Range(0, prefabs.Length)], new Vector3(15f, Random.Range(24, 30f), 27.7f), Quaternion.Euler(0f, 0f, 0f));

		Invoke("SpawnHM", Random.Range(EnemiesForSecondsHM[0], EnemiesForSecondsHM[1]));
	}
}

